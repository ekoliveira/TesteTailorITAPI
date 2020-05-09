using System.ComponentModel.DataAnnotations;
using TesteTailorIT.Infra.Data.Models.Base;

namespace TesteTailorIT.Infra.Data.Models
{
    public class FuncionarioHabilidadeDataModel : DataModel
    {
        [Required]
        public HabilidadeDataModel Habilidade { get; set; }

        [Required]
        public FuncionarioDataModel Funcionario { get; set; }
    }
}