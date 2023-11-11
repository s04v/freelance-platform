using Core.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Extensions;

namespace WebAPI.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet("Me")]
        public async Task<IActionResult> GetMe(CancellationToken token)
        {
            var request = new GetMeRequest
            {
                UserUuid = this.GetUserId().Value,
            };

            return Ok(await _mediator.Send(request, token));
        }

        [Authorize]
        [HttpPut("Me")]
        public async Task<IActionResult> Put([FromBody] UpdateUserRequest request, CancellationToken token)
        {
            request.UserUuid = this.GetUserId().Value;

            await _mediator.Send(request, token);

            return Ok();
        }

        [Authorize]
        [HttpPost("Me/Photo")]
        public async Task<IActionResult> UpdatePhoto([FromForm] UpdateUserPhotoRequest request, CancellationToken token)
        {
            request.UserUuid = this.GetUserId().Value;

            await _mediator.Send(request, token);

            return Ok();
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] Guid id, CancellationToken token)
        {
            var request = new GetUserRequest
            {
                Uuid = id,
            };

            return Ok(await _mediator.Send(request, token));
        }
    }
}
