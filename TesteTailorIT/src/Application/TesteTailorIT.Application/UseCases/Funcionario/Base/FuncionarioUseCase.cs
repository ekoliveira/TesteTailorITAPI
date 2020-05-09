using AutoMapper;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteTailorIT.Application.Interfaces.Repositories;
using TesteTailorIT.Application.Interfaces.UseCases;
using TesteTailorIT.Application.UseCases.Funcionario.Request;
using TesteTailorIT.Application.UseCases.Funcionario.Response;
using TesteTailorIT.Domain.Models;

namespace TesteTailorIT.Application.UseCases.Funcionario.Base
{
    public class FuncionarioUseCase : IFuncionarioUseCase
    {
        private readonly IFuncionarioRepository _funcionarioRepository;
        private readonly IValidator<InserirFuncionarioRequest> _inserirFuncionarioValidator;
        private readonly IValidator<AlterarFuncionarioRequest> _alterarFuncionarioValidator;
        private readonly IMapper _mapper;

        public FuncionarioUseCase(IFuncionarioRepository funcionarioRepository,
           IValidator<InserirFuncionarioRequest> inserirFuncionarioValidator,
           IValidator<AlterarFuncionarioRequest> alterarFuncionarioValidator,
           IMapper mapper)
        {
            _funcionarioRepository = funcionarioRepository;
            _inserirFuncionarioValidator = inserirFuncionarioValidator;
            _alterarFuncionarioValidator = alterarFuncionarioValidator;
            _mapper = mapper;
        }

        public async Task InsertAsync(InserirFuncionarioRequest inserirFuncionario, IOutputPort<FuncionarioResponse> outputPort)
        {
            var validations = _inserirFuncionarioValidator.Validate(inserirFuncionario);

            if (!validations.IsValid)
            {
                outputPort.Handler(new FuncionarioResponse(validations.Errors.Select(x => x.ErrorMessage)));
                return;
            }

            var funcionario = _mapper.Map<FuncionarioModel>(inserirFuncionario);
            await _funcionarioRepository.InsertFuncionarioAsync(funcionario);

            outputPort.Handler(_mapper.Map<FuncionarioResponse>(funcionario));
        }

        public async Task Update(AlterarFuncionarioRequest alterarFuncionario, IOutputPort<FuncionarioResponse> outputPort)
        {
            var validations = _alterarFuncionarioValidator.Validate(alterarFuncionario);

            if (!validations.IsValid)
            {
                outputPort.Handler(new FuncionarioResponse(validations.Errors.Select(x => x.ErrorMessage)));
                return;
            }

            if (!await Exists(alterarFuncionario.Id, outputPort))
                return;

            var funcionario = _mapper.Map<FuncionarioModel>(alterarFuncionario);
            await _funcionarioRepository.UpdateFuncionario(funcionario);

            outputPort.Handler(_mapper.Map<FuncionarioResponse>(funcionario));
        }

        public async Task Delete(int id, IOutputPort<FuncionarioResponse> outputPort)
        {
            if (!await Exists(id, outputPort))
                return;

            await _funcionarioRepository.DeleteFuncionario(id);
        }

        public async Task GetById(int id, IOutputPort<FuncionarioResponse> outputPort)
        {
            var funcionario = await _funcionarioRepository.GetFuncionarioById(id);

            if (funcionario != null)
                outputPort.Handler(_mapper.Map<FuncionarioResponse>(funcionario));
        }

        public async Task Get(IOutputPort<IEnumerable<FuncionarioResponse>> outputPort)
        {
            outputPort.Handler(_mapper.Map<IEnumerable<FuncionarioResponse>>(await _funcionarioRepository.GetFuncionarios()));
        }

        public async Task GetHabilidades(IOutputPort<IEnumerable<HabilidadeResponse>> outputPort)
        {
            outputPort.Handler(_mapper.Map<IEnumerable<HabilidadeResponse>>(await _funcionarioRepository.GetHabilidades()));
        }

        private async Task<bool> Exists(int id, IOutputPort<FuncionarioResponse> outputPort)
        {
            var existe = await _funcionarioRepository.ExistsFuncionario(id);

            if (!existe)
                outputPort.Handler(new FuncionarioResponse("Id não encoontrado", true));

            return existe;
        }
    }
}