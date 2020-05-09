using System.Collections.Generic;

namespace TesteTailorIT.Application.UseCases.Funcionario.Response
{
    public class HabilidadeResponse : UseCaseResponseMessage
    {
        public int Id { get; set; }
        public string Descricao { get; set; }

        public HabilidadeResponse()
        {
        }

        public HabilidadeResponse(string message, bool error) : base(message, error)
        {
        }

        public HabilidadeResponse(IEnumerable<string> errors) : base(errors)
        {
        }
    }
}