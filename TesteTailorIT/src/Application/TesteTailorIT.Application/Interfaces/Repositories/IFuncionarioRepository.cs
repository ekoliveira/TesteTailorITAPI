using System.Collections.Generic;
using System.Threading.Tasks;
using TesteTailorIT.Domain.Models;

namespace TesteTailorIT.Application.Interfaces.Repositories
{
    public interface IFuncionarioRepository
    {
        Task<IEnumerable<FuncionarioModel>> GetFuncionarios();

        Task<FuncionarioModel> GetFuncionarioById(int id);

        Task<FuncionarioModel> InsertFuncionarioAsync(FuncionarioModel domainModel);

        FuncionarioModel InsertFuncionario(FuncionarioModel domainModel);

        Task<FuncionarioModel> UpdateFuncionario(FuncionarioModel domainModel);

        Task<FuncionarioModel> DeleteFuncionario(int id);

        Task<bool> ExistsFuncionario(int id);

        Task<IEnumerable<HabilidadeModel>> GetHabilidades();
    }
}