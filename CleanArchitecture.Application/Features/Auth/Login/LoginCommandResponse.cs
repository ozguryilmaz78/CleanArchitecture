using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Auth.Login
{
    public class LoginCommandResponse
    {
        public LoginCommandResponse(string token, string refreshToken, DateTime refreshTokenExpires)
        {
            Token = token;
            RefreshToken = refreshToken;
            RefreshTokenExpires = refreshTokenExpires;
        }

        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpires { get; set; }
    }
}
