using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SistemaGestaoVigilanciaGP2018.Models
{
    public class PedidoVigilancia
    {
        [Required]
        [Key]
        [Display(Name = "Primeiro Nome")]
        public string PrimeiroNome { get; set; }

        [Required]
        [Display(Name = "Ultimo Nome")]
        public string UltimoNome { get; set; }

        [Required]
        [Display(Name = "Numero de Docente")]
        public string NumeroDocente { get; set; }

        [Required]
        [Display(Name = "Unidade Curricular")]
        public string UnidadeCurricular { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data da Vigilancia")]
        public DateTime DataVigilancia { get; set; }


    }
}