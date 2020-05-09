using System;
using System.Collections.Generic;
using TesteTailorIT.Domain.Models;

namespace TesteTailorIT.Application.UseCases.Funcionario.Response
{
    public class FuncionarioResponse : UseCaseResponseMessage
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Sexo { get; set; }
        public DateTime DataNascimento { get; set; }
        public List<HabilidadeModel> Habilidades { get; set; }

        public FuncionarioResponse()
        {
        }

        public FuncionarioResponse(string message, bool error) : base(message, error)
        {
        }

        public FuncionarioResponse(IEnumerable<string> errors) : base(errors)
        {
        }
    }
}