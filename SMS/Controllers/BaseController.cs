using Common.Helper;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class BaseController : ControllerBase
    {
        public IActionResult Response<T>(APIResponse<T> response)
        {
            return response.IsValidationError switch
            {
                true when response.Status == HttpStatusCode.Unauthorized => Unauthorized(response.Messages),
                true when response.Status == HttpStatusCode.Conflict => Conflict(response.Messages),
                true when response.Status == HttpStatusCode.NotFound => NotFound(response.Messages),
                true when response.Status == HttpStatusCode.BadRequest => BadRequest(response.Messages),
                true when response.Status == HttpStatusCode.InternalServerError => BadRequest(response.Messages),
                _ => Ok(response),
            };
        }
    }
}
