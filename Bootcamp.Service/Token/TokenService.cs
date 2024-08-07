using Bootcamp.Service.SharedDTOs;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp.Service.Token
{
    public interface ITokenService
    {
        Task<ResponseModelDto<TokenResponseDto>> CreateClientAccessToken(GetAccessTokenRequestDto request);
    }
    public class TokenService(IOptions<CustomTokenOptions> tokenOptions, IOptions<Clients> clients): ITokenService
    {

        public Task<ResponseModelDto<TokenResponseDto>> CreateClientAccessToken(GetAccessTokenRequestDto request)
        {
          if(!clients.Value.Items.Any(x=> x.Id == request.ClientId && x.Secret == request.ClientSecret))
          {
                return Task.FromResult(ResponseModelDto<TokenResponseDto>.Fail("Client Not Found"));    
          }


          var claims = new List<Claim>()
          {
                new Claim("clientId",request.ClientId)
          };

            tokenOptions.Value.Audience.ToList().ForEach(x =>
            {
                claims.Add(new Claim(JwtRegisteredClaimNames.Aud, x));
            });
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.Value.Signature));

            var tokenExpire = DateTime.Now.AddHours(tokenOptions.Value.ExpireByHour);

            var jwtToken = new JwtSecurityToken(claims: claims,
                expires: tokenExpire,
                issuer:tokenOptions.Value.Issuer,
                signingCredentials: new SigningCredentials(key,SecurityAlgorithms.HmacSha256Signature));

            var handler = new JwtSecurityTokenHandler();
            var token = handler.WriteToken(jwtToken);

            return Task.FromResult(ResponseModelDto<TokenResponseDto>.Success(new TokenResponseDto(token)));

        }
    }
}
