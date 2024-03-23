using AutoMapper;
using DataAccess.Models;
using SocialNetwork.Views.ViewModels;

namespace SocialNetwork.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<RegisterViewModel, User>()
                    .ForMember(x => x.BirthDate, opt => opt.MapFrom(c => new DateTime((int)c.Year, (int)c.Month, (int)c.Day)))
                    .ForMember(x => x.Email, opt => opt.MapFrom(c => c.Email))
                    .ForMember(x => x.UserName, opt => opt.MapFrom(c => c.Email));
            CreateMap<LoginViewModel, User>();
        }
    }
}
