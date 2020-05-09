using Microsoft.AspNetCore.Mvc;

namespace TesteTailorIT.Api.Response
{
    public sealed class JsonContentResult : ContentResult
    {
        public JsonContentResult()
        {
            ContentType = "application/json";
        }
    }
}