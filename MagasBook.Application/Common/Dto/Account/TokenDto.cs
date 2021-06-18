using System;

namespace MagasBook.Application.Common.Dto.Account
{
    public class TokenDto
    {
        public string Token { get; set; }
        public DateTime Expires { get; set; }
    }
}