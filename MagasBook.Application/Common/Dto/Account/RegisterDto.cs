using AutoMapper;
using MagasBook.Application.Common.Mappings;
using MagasBook.Domain.Entities.Account;

namespace MagasBook.Application.Common.Dto.Account
{
    public class RegisterDto : IMapFrom<ApplicationUser>
    {
        public string UserName { get; set; }
        
        public string Email { get; set; }
        
        public string Password { get; set; }
        
        public string PasswordConfirm { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<RegisterDto, ApplicationUser>();
            profile.CreateMap<RegisterDto, ApplicationUser>().ReverseMap();
        }
    }
}