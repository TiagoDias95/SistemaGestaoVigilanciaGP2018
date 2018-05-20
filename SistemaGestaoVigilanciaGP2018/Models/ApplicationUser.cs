using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;


namespace SistemaGestaoVigilanciaGP2018.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        // Nome Utilizador
        [Display(Name = "Primeiro Nome")]
        public string PrimeiroNome { get; set; }

        [Display(Name = "Ultimo Nome")]
        public string UltimoNome { get; set; }

        // Data Nascimento Utilizador
        [DataType(DataType.Date)]
        [Display(Name = "Data de Nascimento")]
        public DateTime DataNascimento { get; set; }

        // Nacionalidade Utilizador
        public string Nacionalidade { get; set; }

        // Dados necessarios para candidatura
        public string Genero { get; set; }
    }
}
