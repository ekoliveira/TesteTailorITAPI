using TesteTailorIT.Domain.Base;

namespace TesteTailorIT.Domain.Models
{
    public class FuncionarioHabilidadeModel : DomainModel
    {
        public FuncionarioModel Funcionario { get; set; }
        public HabilidadeModel Habilidade { get; set; }
    }
}