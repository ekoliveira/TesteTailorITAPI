using System;
using System.Collections.Generic;
using TesteTailorIT.Domain.Base;

namespace TesteTailorIT.Domain.Models
{
    public class FuncionarioModel : DomainModel
    {
        public virtual string Nome { get; set; }
        public virtual string Email { get; set; }
        public virtual string Sexo { get; set; }
        public virtual DateTime DataNascimento { get; set; }
        public List<HabilidadeModel> Habilidades { get; set; }
    }
}