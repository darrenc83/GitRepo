using AutoMapper;
using YPMMS.Data.Repository.Models;
using YPMMS.Shared.Core.Enums;
using YPMMS.Shared.Core.Models;

namespace YPMMS.Data.Repository.AutoMapper
{
    /// <summary>
    /// AutoMapper profile for the repository layer, defining required model mappings
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Ensure a null SystemStatus (i.e. the MachineStatus property in SystemEvent)
            // converts to a null string, not string.Empty
            CreateMap<SystemStatus?, string>().ConvertUsing(src => src?.ToString());

            CreateMap<Merchant, MerchantInsertModel>();
            CreateMap<SystemEvent, SystemEventInsertModel>();
        }
    }
}