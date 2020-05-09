using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TesteTailorIT.Infra.Data.Models.Base;

namespace TesteTailorIT.Infra.Data.Models
{
    public class FuncionarioDataModel : DataModel
    {
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Nome { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(200)")]
        public string Email { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(9)")]
        public string Sexo { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime DataNascimento { get; set; }
    }
}