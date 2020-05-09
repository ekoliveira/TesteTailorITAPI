using System;
using System.Collections.Generic;
using TesteTailorIT.Domain.Models;

namespace TesteTailorIT.Application.UseCases.Funcionario.Request
{
    public class InserirFuncionarioRequest
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Sexo { get; set; }
        public DateTime DataNascimento { get; set; }
        public List<HabilidadeModel> Habilidades { get; set; }
    }
}