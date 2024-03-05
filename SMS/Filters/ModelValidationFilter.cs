using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Filters
{
    public class ModelValidationFilter : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ModelState.IsValid)
            {
                await next();
            }
            else
            {
                if (!context.ModelState.IsValid)
                {
                    var validationErrors = context.ModelState
                        .Where(x => x.Value.Errors.Any())
                        .ToDictionary(
                            kvp => kvp.Key,
                            kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                        );

                    var errorResponse = new
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        Message = "Validation failed",
                        Errors = validationErrors
                    };

                    var jsonErrorResponse = new JsonResult(errorResponse)
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest
                    };

                    context.Result = jsonErrorResponse;
                    return;
                }

               
            }
        }
    }
}
