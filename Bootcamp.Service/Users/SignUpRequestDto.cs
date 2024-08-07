using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp.Service.Users
{
    public record SignUpRequestDto(string UserName, string Email, string Password, string Name, string Lastname, DateTime? BirthDate);

}
