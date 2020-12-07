using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Publicon.Infrastructure.Commands.Models.User;
using Publicon.Infrastructure.DTOs;
using System.Threading.Tasks;

namespace Publicon.Api.Controllers
{
    
    [Route("api/users/")]
    [ApiController]
    public class UsersController : AbstractController
    {
        public UsersController(IMediator mediator) : base (mediator) { }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterUserCommand command)
        {
            await Handle(command);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPut("activate")] //maybe get?
        public async Task<ActionResult> Activate(ActivateUserCommand command)
        {
            await Handle(command);
            return NoContent();
        }

        [AllowAnonymous]
        [HttpGet("resend-activation-link/{email}")]
        public async Task<ActionResult> ResendActivationLink(string email)
        {
            await Handle(new ResendActivationLinkCommand(email));
            return NoContent();
        }

        [AllowAnonymous]
        [HttpPost("log-in")]
        public async Task<ActionResult<TokenDTO>> Login(LoginUserCommand command)
        {
            var loginResponse = await Handle(command);
            return Ok(loginResponse);
        }

        [AllowAnonymous]
        [HttpPost("refresh")]
        public async Task<ActionResult<TokenDTO>> Refresh(RefreshTokenCommand command)
        {
            var refreshResponse = await Handle(command);
            return Ok(refreshResponse);
        }

        [AllowAnonymous]
        [HttpGet("send-reset-password-email/{email}")]
        public async Task<ActionResult> SendResetPasswordEmail(string email)
        {
            await Handle(new SendResetPasswordEmailCommand(email));
            return NoContent();
        }

        [AllowAnonymous]
        [HttpPost("reset-password")]
        public async Task<ActionResult> ResetPassword(ResetPasswordCommand command)
        {
            await Handle(command);
            return NoContent();
        }

        [Authorize]
        [HttpPut("change-password")]
        public async Task<ActionResult> ChangePassword(ChangePasswordCommand command)
        {
            await Handle(command);
            return NoContent();
        }

        [Authorize(Roles = "User")]
        [HttpDelete]
        public async Task<ActionResult> DeleteAccount()
        {
            await Handle(new DeleteAccountCommand());
            return NoContent();
        }
    }
}
