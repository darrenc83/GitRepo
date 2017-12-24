using AutoMapper;
using Shared.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YPSWebApp.Models.Merchant;
using YPSWebApp.Models.AcquiringBank;
using YPSWebApp.Models.Seller;
using YPSWebApp.Models.Terminal;

namespace YPSWebApp.App_Start
{
    public static class MappingConfig
    {
        public static void RegisterMaps()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<MerchantModel, MerchantViewModel>();
                config.CreateMap<MerchantViewModel, MerchantModel>();
                config.CreateMap<AcquiringBankModel, AcquiringBankViewModel>();
                config.CreateMap<AcquiringBankViewModel, AcquiringBankModel>();
                config.CreateMap<SellerModel, SellerViewModel>();
                config.CreateMap<SellerViewModel, SellerModel>();

                config.CreateMap<TerminalModel, TerminalViewModel>();
                config.CreateMap<TerminalViewModel, TerminalModel>();

                config.CreateMap<MerchantTerminalModel, TerminalViewModel>();
                config.CreateMap<TerminalViewModel, MerchantTerminalModel>();


            });
        }
    }
}