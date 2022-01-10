using System;
using System.Collections.Generic;
using System.Text;

namespace BookingTest.DTO.User
{
    public class UserSuccessLoginDTO
    {
        public long UserId { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
