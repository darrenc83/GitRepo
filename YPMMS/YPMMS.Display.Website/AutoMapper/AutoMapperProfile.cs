using AutoMapper;
using YPMMS.Display.Website.Models.Account;
using YPMMS.Display.Website.Models.Admin;
using YPMMS.Display.Website.Models.Identity;
using YPMMS.Shared.Core.Enums;
using YPMMS.Shared.Core.Models;

namespace YPMMS.Display.Website.AutoMapper
{
    /// <summary>
    /// AutoMapper profile for the website layer, defining required model mappings
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ApplicationUser, CurrentUserDetailsModel>();
           // CreateMap<Machine, AdminMachineJsonModel>();
            //CreateMap<AdminMachineJsonModel, Machine>().AfterMap((src, dest) =>
            //{
            //    dest.SiteId = src.Site?.Id ?? 0;
            //    dest.Status = SystemStatus.Offline;
            //    dest.SystemType = SystemType.NotSet;
            //});
        }
    }
}