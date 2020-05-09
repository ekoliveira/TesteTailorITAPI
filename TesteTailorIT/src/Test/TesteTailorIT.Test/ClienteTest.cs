//using System;
//using TesteTailorIT.Domain.Models;
//using Xunit;

//namespace TesteTailorIT.Test
//{
//    public class ClienteTest
//    {
//        [Fact(DisplayName = "Deve retornar clientes cadastrados")]
//        public async void deve_retornar_clientes_cadastrados()
//        {
//            using (var context = new CarregarBancoInMemory().DataInMemory())
//            {
//                var clientes = await context.GetFuncionarios();
//                Assert.True(clientes != null);
//            }
//        }

//        [Fact(DisplayName = "Deve retornar cliente por id")]
//        public async void deve_retornar_autor_por_id()
//        {
//            using (var context = new CarregarBancoInMemory().DataInMemory())
//            {
//                var cliente = await context.GetFuncionarioById(1);
//                Assert.True(cliente != null);
//            }
//        }

//        [Fact(DisplayName = "Deve cadastrar um cliente")]
//        public async void deve_cadastrar_um_autor()
//        {
//            var cliente = new FuncionarioModel { Id = 4, Nome = "Teste de Oliveira", Cpf = "37378271098", Rg = "431403867", DataNascimento = new DateTime(1994, 03, 19) };

//            using (var context = new CarregarBancoInMemory().DataInMemory())
//            {
//                var clienteCadastrado = await context.InsertAsync(cliente);
//                var result = await context.GetFuncionarioById(4);
//                Assert.True(result != null);
//            }
//        }

//        [Fact(DisplayName = "Deve atualizar um cliente")]
//        public void deve_atualizar_um_autor()
//        {
//            var cliente = new FuncionarioModel { Id = 1, Nome = "Teste de Oliveira", Cpf = "37378271098", Rg = "431403867", DataNascimento = new DateTime(1994, 03, 19) };

//            using (var context = new CarregarBancoInMemory().DataInMemory())
//            {
//                var clienteAtualizado = context.Update(cliente);
//                var result = context.GetFuncionarioById(clienteAtualizado.Id);
//                Assert.Equal("Teste de Oliveira", result.Result.Nome);
//            }
//        }

//        [Fact(DisplayName = "Deve deletar cliente por id")]
//        public void deve_deletar_autor_por_id()
//        {
//            using (var context = new CarregarBancoInMemory().DataInMemory())
//            {
//                var deletar = context.Delete(3);
//                var result = context.GetFuncionarioById(3);
//                Assert.True(result.Result == null);
//            }
//        }
//    }
//}