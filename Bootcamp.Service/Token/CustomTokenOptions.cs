using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp.Service.Token
{
    public record CustomTokenOptions
    {
        public string Signature { get; set; } = default!;
        public int ExpireByHour { get; set; }
        public string  Issuer { get; set; } = default!;
        public string[] Audience { get; set; } = default!;
    }

}
