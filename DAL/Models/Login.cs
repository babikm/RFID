using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class Login
    {
        [Display(Name ="Nazwa użytkownika")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "To pole jest wymagane")]
        public string UserName { get; set; }

        [Display(Name = "Hasło")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "To pole jest wymagane")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name="Zapamiętaj mnie")]
        public bool RememberMe { get; set; }
    }
}
