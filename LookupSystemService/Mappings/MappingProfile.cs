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
            CreateMap<User, FullUserInfo>()
                .ForMember(u => u.Id, opt => opt.MapFrom(m => m.Id))
                .ForMember(u => u.CreatedDate, opt => opt.MapFrom(m => m.CreatedDate))
                .ForMember(u => u.DeleteDate, opt => opt.MapFrom(m => m.DeleteDate))
                .ForMember(u => u.Fired, opt => opt.MapFrom(m => m.Fired))
                .ForMember(u => u.ManagerId, opt => opt.MapFrom(m => m.ManagerId))
                .ForMember(u => u.FirstName, opt => opt.MapFrom(m => m.UserContact.FirstName))
                .ForMember(u => u.LastName, opt => opt.MapFrom(m => m.UserContact.LastName))
                .ForMember(u => u.Phone, opt => opt.MapFrom(m => m.UserContact.Phone))
                .ForMember(u => u.MobilePhone, opt => opt.MapFrom(m => m.UserContact.MobilePhone))
                .ForMember(u => u.Country, opt => opt.MapFrom(m => m.UserContact.Country))
                .ForMember(u => u.City, opt => opt.MapFrom(m => m.UserContact.City))
                .ForMember(u => u.Address, opt => opt.MapFrom(m => m.UserContact.Address))
                .ForMember(u => u.SSN, opt => opt.MapFrom(m => m.UserContact.SSN))
                .ForMember(u => u.DriverLicense, opt => opt.MapFrom(m => m.UserContact.DriverLicense))
                .ForMember(u => u.Email, opt => opt.MapFrom(m => m.UserContact.Email));


            CreateMap<User, ShortUserInfo>()
                .ForMember(u => u.Fired, opt => opt.MapFrom(m => m.Fired))
                .ForMember(u => u.ManagerId, opt => opt.MapFrom(m => m.ManagerId))
                .ForMember(u => u.Name, opt => opt.MapFrom(m => $"{m.UserContact.FirstName} {m.UserContact.LastName}"))
                .ForMember(u => u.Phone, opt => opt.MapFrom(m => m.UserContact.Phone))
                .ForMember(u => u.MobilePhone, opt => opt.MapFrom(m => m.UserContact.MobilePhone))
                .ForMember(u => u.Address, opt => opt.MapFrom(m => $"{m.UserContact.Country}, {m.UserContact.City}, {m.UserContact.Address}"))
                .ForMember(u => u.Email, opt => opt.MapFrom(m => m.UserContact.Email));

        }
    }
}