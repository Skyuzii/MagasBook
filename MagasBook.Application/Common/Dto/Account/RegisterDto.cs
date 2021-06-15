using AutoMapper;
using FluentValidation.Attributes;
using MagasBook.Application.Common.Mappings;
using MagasBook.Application.Common.Validators.Account;
using MagasBook.Domain.Entities.Account;

namespace MagasBook.Application.Common.Dto.Account
{
    [Validator(typeof(RegisterDtoValidator))]
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