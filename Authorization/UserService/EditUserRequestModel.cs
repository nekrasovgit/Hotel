﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Authorization.UserService
{
    public class EditUserRequestModel
    {
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^(([^<>()\\[\]\\.,;:\s@""]+(\.[^<>()\\[\]\\.,;:\s@""]+)*)|("".+""))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$", ErrorMessage = "Email is not valid")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Date of Birth is required")]
        [DataType(DataType.DateTime)]
        public DateTime DateOfBirth { get; set; }
    }
}
