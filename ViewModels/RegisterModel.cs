using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingStore.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Email not specified")]
        [Remote(action: "CheckEmail", controller: "Account", ErrorMessage = "Email is already used")]
        [RegularExpression(@"^[\w]*$", ErrorMessage ="Incorrect Email")]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Length must be between 8 and 30 characters")]
        public string Email { get; set; }

        [StringLength(20, MinimumLength = 8, ErrorMessage = "Length must be between 8 and 20 characters")]
        [Required(ErrorMessage = "Password not specified")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords don't match")]
        public string ConfirmPassword { get; set; }
    }
}
