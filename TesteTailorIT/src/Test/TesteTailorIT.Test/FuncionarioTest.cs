using System;
using System.Collections.Generic;
using TesteTailorIT.Domain.Exceptions;
using TesteTailorIT.Domain.Models;
using Xunit;

namespace TesteTailorIT.Test
{
    public class FuncionarioTest
    {
        [Fact(DisplayName = "Deve retornar funcionarios cadastrados")]
        public async void deve_retornar_funcionarios_cadastrados()
        {
            using (var context = new CarregarBancoInMemory().DataInMemory())
            {
                var funcionarios = await context.GetFuncionarios();
                Assert.True(funcionarios != null);
            }
        }

        [Fact(DisplayName = "Deve retornar funcionario por id")]
        public async void deve_retornar_autor_por_id()
        {
            using (var context = new CarregarBancoInMemory().DataInMemory())
            {
                var funcionario = await context.GetFuncionarioById(1);
                Assert.True(funcionario != null);
            }
        }

        [Fact(DisplayName = "Deve cadastrar um funcionario")]
        public async void deve_cadastrar_um_funcionario()
        {
            var funcionario = new FuncionarioModel
            {
                Id = 3,
                Nome = "Teste Oliveira",
                Email = "email@email.com",
                Sexo = "F",
                DataNascimento = new DateTime(1990, 03, 19),
                Habilidades = new List<HabilidadeModel>()
                {
                    new HabilidadeModel{Id = 1, Descricao = "C#"},
                    new HabilidadeModel{ Id = 3, Descricao = "Angular"},
                }
            };

            using (var context = new CarregarBancoInMemory().DataInMemory())
            {
                var funcionarioCadastrado = await context.InsertFuncionarioAsync(funcionario);
                var result = await context.GetFuncionarioById(3);
                Assert.True(result != null);
            }
        }

        [Fact(DisplayName = "Deve atualizar um funcionario")]
        public async void deve_atualizar_um_autor()
        {
            var funcionario = new FuncionarioModel
            {
                Id = 2,
                Nome = "Teste de Oliveira",
                Email = "email@email.com",
                Sexo = "F",
                DataNascimento = new DateTime(1990, 03, 19),
                Habilidades = new List<HabilidadeModel>()
                    {
                        new HabilidadeModel{Id = 1, Descricao = "C#"},
                        new HabilidadeModel{ Id = 3, Descricao = "Angular"},
                    }
            };

            using (var context = new CarregarBancoInMemory().DataInMemory())
            {
                var funcionarioAtualizado = await context.UpdateFuncionario(funcionario);
                var result = context.GetFuncionarioById(funcionarioAtualizado.Id);
                Assert.Equal("Teste de Oliveira", result.Result.Nome);
            }
        }

        [Fact(DisplayName = "Deve deletar funcionario por id")]
        public async void deve_deletar_autor_por_id()
        {
            using (var context = new CarregarBancoInMemory().DataInMemory())
            {
                var deletar = await context.DeleteFuncionario(1);
                try
                {
                    var result = await context.GetFuncionarioById(1);
                }
                catch (EntityNotFoundException)
                {
                    Assert.True(true);
                }
            }
        }
    }
}