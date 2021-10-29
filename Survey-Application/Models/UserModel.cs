using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Survey_Application.Models
{
    public class UserModel
    {
        [Required(ErrorMessage = "Please enter Username")]
        public string Name { get; set; }
    }
}