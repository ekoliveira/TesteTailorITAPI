using System.Collections.Generic;
using TesteTailorIT.Application;

namespace TesteTailorIT.Api.Response
{
    public interface IPresenter : IOutputPort<UseCaseResponseMessage>, IOutputPort<IEnumerable<UseCaseResponseMessage>>
    {
        JsonContentResult GetContentResult();
    }
}