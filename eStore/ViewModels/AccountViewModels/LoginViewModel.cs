using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace eStore.ViewModels.AccountViewModels
{
    public class LoginViewModel
    {
        [Required]

        [DataType(DataType.EmailAddress)]

        public string Email { get; set;}

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool isPresistant { get; set; }

    }
}
