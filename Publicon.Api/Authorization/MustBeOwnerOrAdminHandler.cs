using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Publicon.Infrastructure.Queries.Models.Publication;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Publicon.Api.Authorization
{
    public class MustBeOwnerOrAdminHandler : AuthorizationHandler<MustBeOwnerOrAdminRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMediator _mediator;

        public MustBeOwnerOrAdminHandler(
            IHttpContextAccessor httpContextAccessor,
            IMediator mediator)
        {
            _httpContextAccessor = httpContextAccessor;
            _mediator = mediator;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            MustBeOwnerOrAdminRequirement requirement)
        {
            var userRole = context.User.FindFirstValue(ClaimTypes.Role);
            if(userRole == "Administrator")
            {
                context.Succeed(requirement);
                return;
            }

            var publicationIdAsString = _httpContextAccessor.HttpContext.GetRouteValue("publicationId").ToString();
            if (!Guid.TryParse(publicationIdAsString, out Guid publicationId))
            {
                context.Fail();
                return;
            }

            var userIdAsString = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdAsString, out Guid userId))
            {
                context.Fail();
                return;
            }

            if (!await _mediator.Send(new ValidateOwnershipQuery(publicationId, userId)))
            {
                context.Fail();
                return;
            }

            context.Succeed(requirement);
            return;
        }

    }
}
