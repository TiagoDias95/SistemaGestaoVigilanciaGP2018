using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SistemaGestaoVigilanciaGP2018.Models
{
    public class PedidoVigilancia
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int IdPedido { get; set; }

        [Required]
        [Display(Name = "Numero de Docente")]
        public string NumeroDocente { get; set; }

        [Required]
        [Display(Name = "Primeiro Nome")]
        public string PrimeiroNome { get; set; }

        [Required]
        [Display(Name = "Ultimo Nome")]
        public string UltimoNome { get; set; }


        public int CursoId { get; set; }
        [Display(Name = "Curso")]
        public string Curso { get; set; }

        public ICollection<Curso> CursoList { get; set; }

        public int UCid { get; set; }
        [Display(Name = "Unidade Curricular")]
        public string UC { get; set; }
        public ICollection<UnidadeCurricular> UCList { get; set; }

        [Required]
        [Display(Name = "Sala")]
        public string Sala { get; set; }


        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data da Vigilancia")]
        public DateTime DataVigilancia { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Data da Vigilancia")]
        public DateTime HoraVigilancia { get; set; }



    }

    public class Curso
    {
        [Key]
        public int IdC { get; set; }
        public int IdpedidoV { get; set; }


        public string NomeCurso { get; set; }
    }

    public class UnidadeCurricular
    {
        [Key]
        public int IdUC { get; set; }
        public int IdpedidoV { get; set; }

        public string NomeUC { get; set; }
    }
}