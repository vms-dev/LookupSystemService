using AutoMapper;
using LookupSystem.DataAccess.Models;
using LookupSystemService.Models;
using System.Linq;

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
                .ForMember("Email", opt => opt.MapFrom(m => m.UserContact.Email))
                .ForMember(d => d.Activities, opt => opt.MapFrom(m => m.Tags.Select(t => t.Name).ToArray()));
;

            CreateMap<User, UserByPhone>()
                .ForMember("Name", opt => opt.MapFrom(m => $"{m.UserContact.FirstName} {m.UserContact.LastName}"))
                .ForMember("Phone", opt => opt.MapFrom(m => m.UserContact.Phone))
                .ForMember("MobilePhone", opt => opt.MapFrom(m => m.UserContact.MobilePhone));

            CreateMap<User, UserByEmail>()
                .ForMember("FirstName", opt => opt.MapFrom(m => m.UserContact.FirstName))
                .ForMember("LastName", opt => opt.MapFrom(m => m.UserContact.LastName))
                .ForMember("Email", opt => opt.MapFrom(m => m.UserContact.Email));

            CreateMap<User, UserByName>()
                .ForMember("Id", opt => opt.MapFrom(m => m.Id))
                .ForMember("Name", opt => opt.MapFrom(m => $"{m.UserContact.FirstName} {m.UserContact.LastName}"))
                .ForMember("ManagerId", opt => opt.MapFrom(m => m.ManagerId));

        }
    }
}