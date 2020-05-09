using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteTailorIT.Application.Interfaces.Repositories;
using TesteTailorIT.Domain.Exceptions;
using TesteTailorIT.Domain.Models;
using TesteTailorIT.Infra.Data.Models;

namespace TesteTailorIT.Infra.Data.Repositories
{
    public class FuncionarioRepository : DbContext, IFuncionarioRepository
    {
        protected readonly IMapper _mapper;

        public FuncionarioRepository(DbContextOptions<FuncionarioRepository> options, IMapper mapper) : base(options)
        {
            _mapper = mapper;
        }

        public DbSet<FuncionarioDataModel> Funcionario { get; set; }

        public DbSet<HabilidadeDataModel> Habilidade { get; set; }

        public DbSet<FuncionarioHabilidadeDataModel> FuncionarioHabilidade { get; set; }

        public async Task<IEnumerable<FuncionarioModel>> GetFuncionarios()
        {
            var funcionarios = await Funcionario.ToListAsync();

            var funcionariosList = new List<FuncionarioModel>();

            foreach (var item in funcionarios)
            {
                funcionariosList.Add(await GetFuncionarioById(item.Id));
            }

            return funcionariosList;
        }

        public async Task<FuncionarioModel> GetFuncionarioById(int id)
        {
            var funcionario = _mapper.Map<FuncionarioModel>(await Funcionario.FindAsync(id));

            var habilidadesList = new List<HabilidadeModel>();

            var funcionarioHabilidades = await FuncionarioHabilidade.ToListAsync();
            var funcionarioHabilidadeFiltrado = funcionarioHabilidades.Where(x => x.Funcionario != null && x.Funcionario.Id == id).ToList();

            var habilidades = await Habilidade.ToListAsync();

            foreach (var item in funcionarioHabilidadeFiltrado)
            {
                var filtro = _mapper.Map<HabilidadeModel>(habilidades.Where(x => x.Id == item.Habilidade.Id).FirstOrDefault());
                habilidadesList.Add(filtro);
            }

            funcionario.Habilidades = habilidadesList;

            return funcionario;
        }

        public async Task<FuncionarioModel> InsertFuncionarioAsync(FuncionarioModel funcionarioModel)
        {
            var funcionarioDataModel = _mapper.Map<FuncionarioDataModel>(funcionarioModel);
            await Funcionario.AddAsync(funcionarioDataModel);
            await SaveChangesAsync();

            funcionarioModel.Id = funcionarioDataModel.Id;

            var funcionarioHabilidadeList = new List<FuncionarioHabilidadeDataModel>();

            foreach (var e in funcionarioModel.Habilidades)
            {
                var habilidadeDataModel = await Habilidade.FindAsync(e.Id);
                AdicionarFuncionarioHabilidade(funcionarioDataModel, habilidadeDataModel);
            }

            await SaveChangesAsync();

            return funcionarioModel;
        }

        public FuncionarioModel InsertFuncionario(FuncionarioModel funcionarioModel)
        {
            var funcionarioDataModel = _mapper.Map<FuncionarioDataModel>(funcionarioModel);
            Funcionario.Add(funcionarioDataModel);
            SaveChanges();
            funcionarioModel.Id = funcionarioDataModel.Id;

            var funcionarioHabilidadeList = new List<FuncionarioHabilidadeDataModel>();

            foreach (var e in funcionarioModel.Habilidades)
            {
                var habilidadeDataModel = _mapper.Map<HabilidadeDataModel>(e);
                Entry(habilidadeDataModel).State = EntityState.Detached;
                funcionarioHabilidadeList.Add(new FuncionarioHabilidadeDataModel() { Funcionario = funcionarioDataModel, Habilidade = habilidadeDataModel });
            }

            FuncionarioHabilidade.AddRange(funcionarioHabilidadeList);

            SaveChanges();

            return funcionarioModel;
        }

        public async Task<FuncionarioModel> UpdateFuncionario(FuncionarioModel funcionarioModel)
        {
            var funcionarioDataModel = await Funcionario.FindAsync(funcionarioModel.Id);

            if (funcionarioDataModel != null)
            {
                Entry(funcionarioDataModel).State = EntityState.Detached;
                funcionarioDataModel = _mapper.Map<FuncionarioDataModel>(funcionarioModel);
                Funcionario.Update(funcionarioDataModel);

                var funcionarioHabiliade = await FuncionarioHabilidade.ToListAsync();
                var funcionarioHabiliadeFiltrado = funcionarioHabiliade.Where(x => x.Funcionario != null && x.Funcionario.Id == funcionarioDataModel.Id);

                FuncionarioHabilidade.RemoveRange(funcionarioHabiliadeFiltrado);

                foreach (var e in funcionarioModel.Habilidades)
                {
                    var habilidadeDataModel = await Habilidade.FindAsync(e.Id);
                    AdicionarFuncionarioHabilidade(funcionarioDataModel, habilidadeDataModel);
                }

                await SaveChangesAsync();

                return funcionarioModel;
            }
            else
            {
                throw new EntityNotFoundException("Entidade não encontrada!");
            }
        }

        public async Task<FuncionarioModel> DeleteFuncionario(int id)
        {
            var funcionarioDataModel = await Funcionario.FindAsync(id);
            if (funcionarioDataModel == null)
            {
                throw new EntityNotFoundException("Entidade não encontrada!");
            }

            var funcionarioHabiliade = await FuncionarioHabilidade.ToListAsync();
            var funcionarioHabiliadeFiltrado = funcionarioHabiliade.Where(x => x.Funcionario != null && x.Funcionario.Id == funcionarioDataModel.Id);

            FuncionarioHabilidade.RemoveRange(funcionarioHabiliadeFiltrado);
            Funcionario.Remove(funcionarioDataModel);

            await SaveChangesAsync();

            var retorno = _mapper.Map<FuncionarioModel>(funcionarioDataModel);
            retorno.Habilidades = _mapper.Map<List<HabilidadeModel>>(funcionarioHabiliadeFiltrado);

            return retorno;
        }

        public async Task<bool> ExistsFuncionario(int id)
        {
            var _entity = await Funcionario.FindAsync(id);

            if (_entity != null)
            {
                return true;
            }

            return false;
        }

        public void AdicionarFuncionarioHabilidade(FuncionarioDataModel funcionarioDataModel, HabilidadeDataModel habilidadeDataModel)
        {
            FuncionarioHabilidade.Add(new FuncionarioHabilidadeDataModel() { Funcionario = funcionarioDataModel, Habilidade = habilidadeDataModel });
        }

        public async Task<IEnumerable<HabilidadeModel>> GetHabilidades()
        {
            return _mapper.Map<IEnumerable<HabilidadeModel>>(await Habilidade.ToListAsync());
        }
    }
}