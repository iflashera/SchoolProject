using Common.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using Services.IServices;

namespace API.Filters
{
    public class SmsAuthorizeAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any())
                return;

            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                var authorizationContext = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();

                if (string.IsNullOrEmpty(authorizationContext))
                {
                    context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Unauthorized);
                    return;
                }
                var token = authorizationContext.Split(" ")[1];
                var handler = new JwtSecurityTokenHandler();
                var tokenS = handler.ReadToken(token) as JwtSecurityToken;
                //var roleId = Convert.ToInt32(tokenS.Claims.FirstOrDefault(r => r.Type == CustomClaimTypes.Role).Value);
                var roleName = tokenS.Claims.FirstOrDefault(r => r.Type == CustomClaimTypes.Role).Value;

                var filterService = context.HttpContext.RequestServices.GetService<IFilterService>();

                var descriptor = context?.ActionDescriptor as ControllerActionDescriptor;
                if (descriptor != null)
                {
                    var actionName = descriptor.ActionName;
                    var ctrlName = descriptor.ControllerName;
                    if (ctrlName != "Common" && actionName != "GetRoleAccesses")
                    {
                        var acceses = await filterService.GetAcceses(actionName, ctrlName, roleName);
                        if (acceses.Count == 0)
                        {
                            context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Forbidden);
                        }
                    }
                }
                else
                {
                    context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Forbidden);
                }
            }
            else
            {
                context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Unauthorized);
            }

        }
    }
}
