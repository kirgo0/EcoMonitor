using EcoMonitor.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Data;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EcoMonitor
{
    public class AuthorizeSecureAttribute : TypeFilterAttribute
    {
        public AuthorizeSecureAttribute(string role) : base(typeof(AuthorizeSecureFilter))
        {
            Arguments = new object[] { role };
        }
    }

    public class AuthorizeSecureFilter : IAsyncAuthorizationFilter
    {
        private readonly UserManager<User> _userManager;
        private readonly string _role;

        public AuthorizeSecureFilter(UserManager<User> userManager, string role)
        {
            _userManager = userManager;
            _role = role;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var isAllowAnonymous = context.ActionDescriptor.EndpointMetadata
            .Any(em => em.GetType() == typeof(AllowAnonymousAttribute));

            if (isAllowAnonymous)
            {
                return;
            }

            var userId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);

                if (roles.Contains(_role))
                    return; // Success exit point
                else
                {
                    context.Result = new ForbidResult();
                    return;
                }
            }
            else
            {
                context.Result = new ForbidResult();
                return;
            }

        }
    }

}
