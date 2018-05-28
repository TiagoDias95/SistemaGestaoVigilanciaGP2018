using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaGestaoVigilanciaGP2018.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Primeiro Nome")]
        public string PrimeiroNome { get; set; }

        [Required]
        [Display(Name = "Ultimo Nome")]
        public string UltimoNome { get; set; }

        [Required]
        [Display(Name = "Numero de Docente")]
        public string NumeroDocente { get; set; }

        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Nascimento")]
        public DateTime DataNascimento { get; set; }

        [Required]
        [Display(Name = "Nacionalidade")]
        public string Nacionalidade { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
