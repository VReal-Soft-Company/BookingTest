using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookingTest.DTO.User
{
    public class RegisterDTO:Basic.BasicAuthDTO
    { 
        [Compare(nameof(Password), ErrorMessage ="Passwords are not Equal.")]
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }  
        public string PhoneNumber { get; set; }
    }
}
