using MediatR;
using Microsoft.AspNetCore.Mvc;
using Publicon.Core.Exceptions;
using Publicon.Infrastructure.Commands;
using Publicon.Infrastructure.Queries;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Publicon.Api.Controllers
{
    public class AbstractController : ControllerBase
    {
        private readonly IMediator _mediator;

        protected AbstractController(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected async Task<T> Handle<T>(IRequest<T> request)
        {
            try
            {
                if (request is AuthQuery)
                {
                    (request as AuthQuery).UserId = UserId.Value;
                }

                if (request is Command)
                {
                    (request as Command).UserId = UserId.Value;
                }
            } catch(ArgumentNullException e)
            {
                throw new PubliconException(ErrorCode.InvalidCredentials, e.Message);
            }
            return await _mediator.Send(request);
        }

        protected Guid? UserId => GetUserIdFromToken();

        private Guid? GetUserIdFromToken()
        {
            if (User?.Identity?.IsAuthenticated == true)
            {
                return Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            }

            throw new PubliconException(ErrorCode.InvalidCredentials);
        }
    }
}
