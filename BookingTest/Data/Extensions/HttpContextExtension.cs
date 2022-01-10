﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookingTest.Data.Extensions
{
    public static class HttpContextExtension
    {
        public static int GetCurrentUserID(this HttpContext context)
        {
            try
            {
                return int.Parse((context.User.Identity as ClaimsIdentity).FindFirst(ClaimTypes.Name).Value);
            }
            catch
            {
                return 0;
            }

        }
        public static string GetCurrentUserRole(this HttpContext context)
        {
            return (context.User.Identity as ClaimsIdentity).FindFirst(ClaimTypes.Role).Value;
        }
        public static string GetCurrentUserEmail(this HttpContext context)
        {
            return (context.User.Identity as ClaimsIdentity).FindFirst(ClaimTypes.Email).Value;
        } 
    }
}
