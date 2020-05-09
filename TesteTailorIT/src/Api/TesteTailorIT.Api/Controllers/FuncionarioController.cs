using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TesteTailorIT.Api.Response;
using TesteTailorIT.Application.Interfaces.UseCases;
using TesteTailorIT.Application.UseCases.Funcionario.Request;

namespace TesteTailorIT.Api.Controllers
{
    [Route("[controller]")]
    public class FuncionarioController : Controller
    {
        private readonly IFuncionarioUseCase _funcionarioUseCase;
        private readonly IPresenter _presenter;

        public FuncionarioController(IFuncionarioUseCase funcionarioUseCase, IPresenter presenter)
        {
            _funcionarioUseCase = funcionarioUseCase;
            _presenter = presenter;
        }

        [HttpPost]
        [Route(nameof(Inserir))]
        public async Task<IActionResult> Inserir([FromBody]InserirFuncionarioRequest inserirCliente)
        {
            await _funcionarioUseCase.InsertAsync(inserirCliente, _presenter);
            return _presenter.GetContentResult();
        }

        [HttpPut]
        [Route(nameof(Alterar))]
        public async Task<IActionResult> Alterar([FromBody]AlterarFuncionarioRequest alterarCliente)
        {
            await _funcionarioUseCase.Update(alterarCliente, _presenter);
            return _presenter.GetContentResult();
        }

        [HttpDelete]
        [Route(nameof(Excluir))]
        public async Task<IActionResult> Excluir(int id)
        {
            await _funcionarioUseCase.Delete(id, _presenter);
            return _presenter.GetContentResult();
        }

        [HttpGet]
        [Route(nameof(ObterPorId))]
        public async Task<IActionResult> ObterPorId(int id)
        {
            await _funcionarioUseCase.GetById(id, _presenter);
            return _presenter.GetContentResult();
        }

        [HttpGet]
        [Route(nameof(ObterLista))]
        public async Task<IActionResult> ObterLista()
        {
            await _funcionarioUseCase.Get(_presenter);
            return _presenter.GetContentResult();
        }

        [HttpGet]
        [Route(nameof(ObterHabilidades))]
        public async Task<IActionResult> ObterHabilidades()
        {
            await _funcionarioUseCase.GetHabilidades(_presenter);
            return _presenter.GetContentResult();
        }
    }
}