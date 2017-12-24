using Microsoft.AspNet.Identity;
using Shared.Core.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Mvc;
using YPSWebApp.Domain.Service;
using YPSWebApp.Models.Merchant;
using YPSWebApp.Models.AcquiringBank;
using YPSWebApp.Models.Seller;
using AutoMapper;
using YPSWebApp.Models.Terminal;

namespace YPSWebApp.Controllers
{
    public class MerchantController : AuthBaseController
    {
        [HttpGet]
        public  ActionResult Create()
        {          
            MerchantViewModel mvm = PopulateDropdowns();

            return View(mvm);
        }

        
        public ActionResult Details(int id)
        {
            var merchantService = new MerchantDataService(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            var model = merchantService.GetMerchantToEdit(id);

            var mappedModel = Mapper.Map<MerchantModel, MerchantViewModel>(model);

            return View(mappedModel);
        }

        public MerchantViewModel PopulateDropdowns()
        {
            List<AcquiringBankModel> abm = new List<AcquiringBankModel>();
            List<SellerModel> sm = new List<SellerModel>();
            List<TerminalModel> tm = new List<TerminalModel>();

            var merchantService = new MerchantDataService(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            var acuiringBankService = new AcquiringBankDataService(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            var sellersService = new SellerDataService(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            var terminalService = new TerminalDataService(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

            MerchantViewModel mvm = new MerchantViewModel();
         
           

            abm = acuiringBankService.GetAcquiringBank();
            sm = sellersService.GetSellers();
           

            
           

            var mappedBankModel = Mapper.Map<List<AcquiringBankModel>, List<AcquiringBankViewModel>>(abm);
            var mappedSellerModel = Mapper.Map<List<SellerModel>, List<SellerViewModel>>(sm);
            var mappedTerminalModel = Mapper.Map <List<TerminalModel>, List<TerminalViewModel>>(tm);


            //foreach (var item in mappedTerminalModel)
            //{
            //    item.Terminaltype = item.Terminaltype + " - £" + decimal.Round(item.TerminalAmount, 2);

            //}

            //mvm.MerchantTerminalViewModel.MerchantTerminalViewModelList = new List<TerminalViewModel>();



            mvm.AcquiringBankViewModelList = mappedBankModel;
            mvm.SellerViewModelList = mappedSellerModel;
           // mvm.MerchantTerminalViewModel.MerchantTerminalViewModelList = mappedTerminalModel;
            
            return mvm;

        }

        [HttpPost]
        public async Task<ActionResult> Create(MerchantViewModel model)
        {
            if (model.MerchantId <= 0)
            {
                ModelState.AddModelError("MerchantId", "Merchant Id must be greater than 0");
               
                return View();
            }
            var user = await UserManager.FindByEmailAsync(User.Identity.Name);

            model.CreatedBy = user.Firstname + " " + user.Surname;
            model.CreatedDate = DateTime.Now;
            
            var merchantService = new MerchantDataService (ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            AcquiringBankModel acb = new AcquiringBankModel();
            SellerModel sm = new SellerModel();
            acb.Id = model.AcquiringBankViewModel.Id;
            sm.Id = model.SellerViewModel.Id;
            //var mappedBankModel = Mapper.Map<AcquiringBankViewModel, AcquiringBankModel>(model.AcquiringBankViewModel);
            //var mappedSellerModel = Mapper.Map<SellerViewModel, SellerModel>(model.SellerViewModel);



            var mappedModel = Mapper.Map<MerchantViewModel, MerchantModel>(model);

            mappedModel.AcquiringBankId = acb.Id;
            mappedModel.SellerId = sm.Id;
            var NewMerchant =  merchantService.CreateMerchant(mappedModel);

            if (NewMerchant > 0)
            {
                //a record has been created.
                return RedirectToAction("Overview","Merchant");
            }


            else if (NewMerchant == -1)
            {
                //an error occured.
                ModelState.AddModelError("MerchantId", "Merchant " +model.MerchantId + " already exists!");

                MerchantViewModel mvm = PopulateDropdowns();
                model.AcquiringBankViewModelList = mvm.AcquiringBankViewModelList;
                model.SellerViewModelList = mvm.SellerViewModelList;

                return View(model);
            }

            else {
                return View();
            }

          
        }

        public ActionResult Edit(int id)
        {
            var merchantService = new MerchantDataService(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            var model =  merchantService.GetMerchantToEdit(id);

            var mappedModel = Mapper.Map<MerchantModel, MerchantViewModel>(model);

            return View(mappedModel);
        }

        [HttpPost]
        public  ActionResult Edit(MerchantViewModel model)
        {            
            var user =  UserManager.FindByEmail(User.Identity.Name);
            model.LastUpdatedDate = DateTime.Now;
            model.LastUpdatedBy = user.Firstname + " " + user.Surname;
            var merchantService = new MerchantDataService(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

            var mappedModel = Mapper.Map<MerchantViewModel, MerchantModel>(model);

            var result =  merchantService.EditMerchant(mappedModel);

            if (result == 1)
            {
                return RedirectToAction("Overview", "Merchant");
            }

            else
            {
                ModelState.AddModelError("MerchantName", "An error occured whilst updating merchant " + model.MerchantName +"!");
                return View();
            }
        
        }


        [HttpGet]
        public  ActionResult Manage()
        {
           
            var merchantService = new MerchantDataService(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            var merchants =  merchantService.GetMerchantList(); 

            var mappedModel = Mapper.Map<List<MerchantModel>, List<MerchantViewModel>>(merchants);

            return View(mappedModel);
        }

        [HttpGet]
        public ActionResult Overview()
        {
            var merchantService = new MerchantDataService(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            var merchants = merchantService.GetMerchantList();

            var mappedModel = Mapper.Map<List<MerchantModel>, List<MerchantViewModel>>(merchants);

            return View(mappedModel);

        }
    }
}