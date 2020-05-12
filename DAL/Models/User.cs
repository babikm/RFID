using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{

    public partial class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Display(Name = "Nazwa użytkownika")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "To pole jest wymagane!")]
        public string UserName { get; set; }

        [Display(Name = "Imię")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "To pole jest wymagane!")]
        public string FirstName { get; set; }

        [Display(Name = "Nazwisko")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "To pole jest wymagane!")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "To pole jest wymagane!")]
        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }

        [Display(Name = "Hasło")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "To pole jest wymagane!")]
        [DataType(DataType.Password)]
        [MinLength(6,ErrorMessage = "Hasło powinno zawierać minimum 6 znaków!")]
        public string Password { get; set; }

        [Display(Name = "Potwierdź hasło")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Hasła nie są identyczne!")]
        [BsonIgnore]
        public string ConfirmPassword { get; set; }

        public bool EmailConfirmed { get; set; }
        public Guid ActivationCode { get; set; }
        public Role role { get; set; }
        
        public enum Role
        {
            SuperAdmin = 2,
            Admin = 1,
            User = 0
        }
    }
}
