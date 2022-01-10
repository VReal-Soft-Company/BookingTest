using System;
using System.Collections.Generic;
using System.Text;

namespace BookingTest.BLL.Data
{
    public class JWTAuthentication
    {
        public string JwtIssuer { get; set; }
        public string JwtAudience { get; set; }
        public string JwtKey { get; set; }
        public string JwtHours { get; set; }
    }
}
 