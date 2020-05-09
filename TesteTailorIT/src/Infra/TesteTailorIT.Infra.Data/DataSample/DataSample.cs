using System;
using System.Collections.Generic;
using System.Linq;
using TesteTailorIT.Domain.Models;
using TesteTailorIT.Infra.Data.Models;
using TesteTailorIT.Infra.Data.Repositories;

namespace TesteTailorIT.Infra.Data.DataSample
{
    public static class DataSample
    {
        public static void Initialize(FuncionarioRepository context)
        {
            context.Database.EnsureCreated();

            if (context.Funcionario.Any())
            {
                return;
            }

            //(C#, Java, Angular, SQL, ASP)
            var habilidades = new HabilidadeDataModel[]
            {
                new HabilidadeDataModel{ Descricao = "C#"},
                new HabilidadeDataModel{ Descricao = "Java"},
                new HabilidadeDataModel{ Descricao = "Angular"},
                new HabilidadeDataModel{ Descricao = "SQL"},
                new HabilidadeDataModel{ Descricao = "ASP"},
            };

            var funcionarios = new FuncionarioModel[]
            {
                new FuncionarioModel
                {
                    Nome = "Erick Henrique de Oliveira",
                    Email ="email@email.com",
                    Sexo = "M",
                    DataNascimento = new DateTime(1994,03,19),
                    Habilidades = new List<HabilidadeModel>()
                    {
                        new HabilidadeModel{ Id = 1, Descricao = "C#"},
                        new HabilidadeModel{ Id = 2, Descricao = "Java"},
                        new HabilidadeModel{ Id = 3, Descricao = "Angular"},
                        new HabilidadeModel{ Id = 4, Descricao = "SQL"},
                        new HabilidadeModel{ Id = 5, Descricao = "ASP"},
                    }
                },

                new FuncionarioModel
                {
                    Nome = "Claudia Carvalho dos Santos",
                    Email ="email@email.com",
                    Sexo = "F",
                    DataNascimento = new DateTime(1990,03,19),
                    Habilidades = new List<HabilidadeModel>()
                    {
                        new HabilidadeModel{Id = 1, Descricao = "C#"},
                        new HabilidadeModel{ Id = 3, Descricao = "Angular"},
                    }
                },

                new FuncionarioModel
                {
                    Nome = "Mafis Santos",
                    Email ="email@email.com",
                    Sexo = "F",
                    DataNascimento = new DateTime(1990,03,19),
                    Habilidades = new List<HabilidadeModel>()
                    {
                        new HabilidadeModel{Id = 1, Descricao = "C#"},
                        new HabilidadeModel{ Id = 3, Descricao = "Angular"},
                        new HabilidadeModel{ Id = 4, Descricao = "SQL"},
                    }
                },

                new FuncionarioModel
                {
                    Nome = "Kiazoama Carvalho",
                    Email ="email@email.com",
                    Sexo = "F",
                    DataNascimento = new DateTime(1990,03,19),
                    Habilidades = new List<HabilidadeModel>()
                    {
                        new HabilidadeModel{Id = 1, Descricao = "C#"},
                        new HabilidadeModel{ Id = 3, Descricao = "Angular"},
                        new HabilidadeModel{ Id = 5, Descricao = "ASP"},
                    }
                },

                new FuncionarioModel
                {
                    Nome = "Niheso Fernandes",
                    Email ="email@email.com",
                    Sexo = "M",
                    DataNascimento = new DateTime(1990,03,19),
                    Habilidades = new List<HabilidadeModel>()
                    {
                        new HabilidadeModel{Id = 1, Descricao = "C#"},
                    }
                },

                new FuncionarioModel
                {
                    Nome = "Ryael Oliveira",
                    Email ="email@email.com",
                    Sexo = "M",
                    DataNascimento = new DateTime(1990,03,19),
                    Habilidades = new List<HabilidadeModel>()
                    {
                        new HabilidadeModel{Id = 1, Descricao = "C#"},
                    }
                },

                new FuncionarioModel
                {
                    Nome = "Naihe Hamada",
                    Email ="email@email.com",
                    Sexo = "F",
                    DataNascimento = new DateTime(1990,03,19),
                    Habilidades = new List<HabilidadeModel>()
                    {
                        new HabilidadeModel{ Id = 5, Descricao = "ASP"},
                    }
                },
            };

            context.AddRange(habilidades);
            context.SaveChanges();

            foreach (var item in funcionarios)
            {
                context.InsertFuncionario(item);
            }
        }
    }
}