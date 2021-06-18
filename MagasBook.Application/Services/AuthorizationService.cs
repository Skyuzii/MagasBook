using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration;
using MagasBook.Application.Common.Constants;
using MagasBook.Application.Common.Dto;
using MagasBook.Application.Common.Dto.Account;
using MagasBook.Application.Common.Exceptions;
using MagasBook.Application.Common.Interfaces;
using MagasBook.Domain.Entities.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ClaimTypes = MagasBook.Application.Common.Constants.ClaimTypes;

namespace MagasBook.Application.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IMapper _mapper;
        private readonly JwtSettings _jwtSettings;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthorizationService(
            IMapper mapper,
            IOptions<JwtSettings> jwtSettings,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task RegisterAsync(RegisterDto registerDto)
        {
            var userExist = await _userManager.FindByNameAsync(registerDto.UserName) != null;
            if (userExist)
            {
                throw new RestException("Пользователь уже существует", StatusCodes.Status500InternalServerError);
            }

            userExist = await _userManager.FindByEmailAsync(registerDto.Email) != null;
            if (userExist)
            {
                throw new RestException("Пользователь уже существует", StatusCodes.Status500InternalServerError);
            }
            
            var applicationUser = _mapper.Map<ApplicationUser>(registerDto);
            var result = await _userManager.CreateAsync(applicationUser, registerDto.Password);
            if (!result.Succeeded)
            {
                throw new RestException(result.Errors.First().Description, StatusCodes.Status500InternalServerError);
            }

            await _userManager.AddToRoleAsync(applicationUser, UserRoles.User);
        }

        public async Task<TokenDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            if (user == null)
            {
                throw new RestException("Неверный логин или пароль", StatusCodes.Status401Unauthorized);
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded)
            {
                throw new RestException("Неверный логин или пароль", StatusCodes.Status401Unauthorized);
            }
            
            var expires = DateTime.Now.AddMonths(1);
            var token = await GenerateJwtTokenAsync(user, expires);
            
            return new TokenDto {Token = token, Expires = expires};
        }

        private async Task<string> GenerateJwtTokenAsync(ApplicationUser user, DateTime expires)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Id, user.Id),
                new Claim(ClaimTypes.UserName, user.UserName)
            };

            claims.AddRange((await _userManager.GetRolesAsync(user)).Select(userRole => new Claim(ClaimTypes.Role, userRole)));
            
            var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                expires: expires,
                claims: claims,
                signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256Signature));
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}