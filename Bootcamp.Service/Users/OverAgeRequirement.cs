using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp.Service.Users
{ 
    public class OverAgeRequirement: IAuthorizationRequirement // Policy based Authorization
    {
        public int Age { get; set; }
    }


    public class OverAgeRequirementHandler : AuthorizationHandler<OverAgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OverAgeRequirement requirement)
        {
           if(!context.User.HasClaim(c=> c.Type == ClaimTypes.DateOfBirth))
            {
                return Task.CompletedTask;
            }

            var dateOfBirth = Convert.ToDateTime(context.User.FindFirst(c => c.Type == ClaimTypes.DateOfBirth).Value);

            var age = DateTime.Today.Year - dateOfBirth.Year;

            if(dateOfBirth.Date > DateTime.Today.AddYears(-age))
            {
                age--;
            }

            if(age >= requirement.Age)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
