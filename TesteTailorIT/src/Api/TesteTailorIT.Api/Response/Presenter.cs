using System.Collections.Generic;
using System.Net;
using TesteTailorIT.Api.Serialization;
using TesteTailorIT.Application;

namespace TesteTailorIT.Api.Response
{
    public class Presenter : IPresenter
    {
        public JsonContentResult ContentResult { get; set; }

        public Presenter()
        {
            ContentResult = new JsonContentResult();
        }

        public JsonContentResult GetContentResult()
        {
            return ContentResult;
        }

        public void Handler(UseCaseResponseMessage response)
        {
            var isValid = response.IsValid();
            ContentResult.StatusCode = (int)(isValid ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            ContentResult.Content = isValid ? JsonSerializer.SerializeObject(response) : JsonSerializer.SerializeObject(response.Errors);
        }

        public void Handler(IEnumerable<UseCaseResponseMessage> response)
        {
            ContentResult.StatusCode = (int)HttpStatusCode.OK;
            ContentResult.Content = JsonSerializer.SerializeObject(response);
        }
    }
}