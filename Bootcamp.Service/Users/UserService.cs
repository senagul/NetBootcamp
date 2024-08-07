﻿using Bootcamp.Repository.Identities;
using Bootcamp.Service.SharedDTOs;
using Bootcamp.Service.Token;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

namespace Bootcamp.Service.Users
{
    public class UserService(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager,IOptions<CustomTokenOptions> tokenOptions,IOptions<Clients> clients)
    {
        public async Task<ResponseModelDto<Guid>> SignUp(SignUpRequestDto request)
        {
            var user = new AppUser
            {
                UserName = request.UserName,
                Email = request.Email,
                Name = request.Name,
                Surname = request.Lastname,
                BirthDate = request.BirthDate
            };

            var result = await userManager.CreateAsync(user,request.Password);

            if(!result.Succeeded)
            {
                return ResponseModelDto<Guid>.Fail(result.Errors.Select(x=> x.Description).ToList()); 
            }

            if(request.BirthDate.HasValue)
            {
                await userManager.AddClaimAsync(user,new Claim(ClaimTypes.DateOfBirth,user.BirthDate!.Value.ToShortDateString()));
            }



            return ResponseModelDto<Guid>.Success(user.Id, HttpStatusCode.Created);
        }

        public async Task<ResponseModelDto<TokenResponseDto>> SignIn(SignInRequestDto request)
        {
            var user = await userManager.FindByEmailAsync(request.Email);

            if(user is null)
            {
                return ResponseModelDto<TokenResponseDto>.Fail("Email or Password is wrong",HttpStatusCode.NotFound);
            }

            var result = await userManager.CheckPasswordAsync(user,request.Password);

            if(!result)
            {
                return ResponseModelDto<TokenResponseDto>.Fail("Email or Password is wrong");
            }

            var userClaimList = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name!)

            };

            tokenOptions.Value.Audience.ToList().ForEach(x =>
            {
                userClaimList.Add(new Claim(JwtRegisteredClaimNames.Aud, x));
            });
            var userRoles = await userManager.GetRolesAsync(user);

            foreach (var userRole in userRoles)
            {
                userClaimList.Add(new Claim(ClaimTypes.Role, userRole));
            }
            var userClaims = await userManager.GetClaimsAsync(user);

            foreach (var userClaim in userClaims)
            {
                userClaimList.Add(new Claim(userClaim.Type, userClaim.Value));
            }

            foreach (var roleName in userRoles)
            {
                var role = await roleManager.FindByNameAsync(roleName);

                if (role is null)
                {
                    continue;
                }

                var roleClaim = await roleManager.GetClaimsAsync(role);

                foreach (var roleAsClaim in roleClaim)
                {
                    userClaimList.Add(roleAsClaim);
                }

            }
     

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.Value.Signature));
            var tokenExpire = DateTime.Now.AddHours(tokenOptions.Value.ExpireByHour);

            var jwtToken = new JwtSecurityToken(claims: userClaimList,
            expires: tokenExpire,
                issuer: tokenOptions.Value.Issuer,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature));

            var handler = new JwtSecurityTokenHandler();
            var token = handler.WriteToken(jwtToken);

            return ResponseModelDto<TokenResponseDto>.Success(new TokenResponseDto(token));

        }
    }
}
