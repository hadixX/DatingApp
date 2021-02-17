using System.Linq;
using AutoMapper;
using DatingApp.API.DTO;
using DatingApp.API.models;

namespace DatingApp.API.handlers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User,UserForDetailedList>()
                .ForMember(dest =>dest.PhotoUrl, opt=>
                    opt.MapFrom(src =>src.Photos.FirstOrDefault(p=>p.IsMain).Url))
                .ForMember(dest =>dest.Age, opt => 
                    opt.MapFrom(src =>src.DateOfBirth.CalculateAge()));
            CreateMap<User,UserForList>()
            .ForMember(dest =>dest.PhotoUrl, opt=>
                    opt.MapFrom(src =>src.Photos.FirstOrDefault(p=>p.IsMain).Url))
                    .ForMember(dest =>dest.Age, opt => 
                    opt.MapFrom(src =>src.DateOfBirth.CalculateAge()));
            CreateMap<Photo,PhotoForDetailedList>();
            CreateMap<PhotoForCreation,Photo>();
            CreateMap<UserForUpdate,User>();
            CreateMap<Photo,PhotoFromReturn>();
            CreateMap<UserForRegister, User>();
            
        }
    }
}