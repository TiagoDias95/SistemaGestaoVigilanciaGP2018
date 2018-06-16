using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaGestaoVigilanciaGP2018.Models
{
    public class PedidoVigilancia
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPedido { get; set; }

        [Required]
        [Display(Name = "Numero de Docente")]
        public string NumeroDocente { get; set; }

        [Required]
        [Key]
        [Display(Name = "Primeiro Nome")]
        public string PrimeiroNome { get; set; }

        [Required]
        [Display(Name = "Ultimo Nome")]
        public string UltimoNome { get; set; }

        [Required]
        [Display(Name = "Unidade Curricular")]
        public string UnidadeCurricular { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data da Vigilancia")]
        public DateTime DataVigilancia { get; set; }



    }
}