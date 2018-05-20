using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaGestaoVigilanciaGP2018.Models.ManageViewModels
{
    public class IndexViewModel
    {
        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Nº Telefone")]
        public string PhoneNumber { get; set; }

        public string StatusMessage { get; set; }

        public ApplicationUser User { get; set; }
    }
}
