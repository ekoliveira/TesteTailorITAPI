using System.Collections.Generic;
using System.Threading.Tasks;
using TesteTailorIT.Application.UseCases.Funcionario.Request;
using TesteTailorIT.Application.UseCases.Funcionario.Response;

namespace TesteTailorIT.Application.Interfaces.UseCases
{
    public interface IFuncionarioUseCase
    {
        Task InsertAsync(InserirFuncionarioRequest inserirCliente, IOutputPort<FuncionarioResponse> outputPort);

        Task Update(AlterarFuncionarioRequest alterarCliente, IOutputPort<FuncionarioResponse> outputPort);

        Task Delete(int id, IOutputPort<FuncionarioResponse> outputPort);

        Task GetById(int id, IOutputPort<FuncionarioResponse> outputPort);

        Task Get(IOutputPort<IEnumerable<FuncionarioResponse>> outputPort);

        Task GetHabilidades(IOutputPort<IEnumerable<HabilidadeResponse>> outputPort);
    }
}