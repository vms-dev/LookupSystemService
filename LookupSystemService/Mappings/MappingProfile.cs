using AutoMapper;
using LookupSystem.DataAccess.Models;
using LookupSystemService.Models;

namespace LookupSystemService.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserHired>();
            
            CreateMap<User, UserFired>()
                .ForMember("Name", opt => opt.MapFrom(m => $"{m.UserContact.FirstName} {m.UserContact.LastName}"))
                .ForMember("CreatedDate", opt => opt.MapFrom(m => m.CreatedDate.ToShortDateString()))
                .ForMember("DeleteDate", opt => opt.MapFrom(m => m.DeleteDate.HasValue ? m.DeleteDate.Value.ToShortDateString() : null))
                .ForMember("Phone", opt => opt.MapFrom(m => m.UserContact.Phone))
                .ForMember("MobilePhone", opt => opt.MapFrom(m => m.UserContact.MobilePhone))
                .ForMember("Email", opt => opt.MapFrom(m => m.UserContact.Email));

            CreateMap<User, UserByPhone>()
                .ForMember("Name", opt => opt.MapFrom(m => $"{m.UserContact.FirstName} {m.UserContact.LastName}"))
                .ForMember("Phone", opt => opt.MapFrom(m => m.UserContact.Phone))
                .ForMember("MobilePhone", opt => opt.MapFrom(m => m.UserContact.MobilePhone));

            CreateMap<User, UserByEmail>()
                .ForMember("FirstName", opt => opt.MapFrom(m => m.UserContact.FirstName))
                .ForMember("LastName", opt => opt.MapFrom(m => m.UserContact.LastName))
                .ForMember("Email", opt => opt.MapFrom(m => m.UserContact.Email));
            
            
        }
    }
}