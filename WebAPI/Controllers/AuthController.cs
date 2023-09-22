using Core.Auth;
using Core.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Extensions;

namespace WebAPI.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPut("Account")]
        public async Task<ActionResult> Register(
            [FromBody] UpdateAccountRequest request,
            CancellationToken token)
        {
            request.UserUuid = this.GetUserId().Value;

            await _mediator.Send(request, token);

            return Ok();
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register(
            [FromBody] RegisterUserRequest request,
            CancellationToken token)
        {
            await _mediator.Send(request, token);
                
            return Ok();
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(
            [FromBody] LoginWithCredentialsRequest request,
            CancellationToken token)
        {
            var response = await _mediator.Send(request, token);

            return Ok(response);
        }

        [HttpGet("Activate/{activateToken}")]
        public async Task<ActionResult> ActivateAccount(
            [FromRoute] string activateToken,
            CancellationToken token)
        {
            var request = new ActivateAccountRequest
            {
                ActivateToken = activateToken
            };

            await _mediator.Send(request, token);

            return Ok();
        }

        [HttpPost("RecoveryPassword")]
        public async Task<ActionResult> RecoveryPassword(
            [FromBody] RecoveryPasswordRequest request,
            CancellationToken token)
        {
            var response = await _mediator.Send(request, token);

            return Ok(response);
        }

        [HttpPut("RecoveryPassword")]
        public async Task<ActionResult> ChangePassword(
            [FromBody] ChangePasswordRequest request,
            CancellationToken token)
        {
            var response = await _mediator.Send(request, token);

            return Ok(response);
        }
    }
}
