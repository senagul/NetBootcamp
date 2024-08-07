using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp.Repository.Identities
{
    public class AppUser:IdentityUser<Guid>
    {
        public string Name { get; set; } = default!;
        public string Surname { get; set; } = default!;
        public DateTime? BirthDate { get; set; }
    }
}
