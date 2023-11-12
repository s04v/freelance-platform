using Core.Jobs;
using Core.Jobs.Applications;
using Core.Jobs.Attachment;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using WebAPI.Extensions;

namespace WebAPI.Controllers
{
    [Route("api/Job")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IMediator _mediator;

        public JobController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Mine")]
        public async Task<IEnumerable<MyJobItem>> GetMyJobs(CancellationToken token)
        {
            var request = new GetMyJobsRequest
            {
                UserUuid = this.GetUserId().Value
            };

            return await _mediator.Send(request, token);
        }

        [HttpGet]
        public async Task<JobListResponse> Get([FromQuery] int page, CancellationToken token)
        {
            var request = new GetJobsRequest()
            {
                PageNumber = page,
            };

            return await _mediator.Send(request, token);
        }

        [HttpGet("{id}")]
        public async Task<JobResponseItem> Get([FromRoute] Guid id, CancellationToken token)
        {
            var userId = this.GetUserId().Value;

            var request = new GetJobRequest
            {
                JobUuid = id,
                UserUuid = userId
            };

            return await _mediator.Send(request, token);
        }

        [Authorize]
        [HttpGet("{id}/MyApplication")]
        public async Task<MyApplicationResponse> GetMyApplication([FromRoute] Guid id, CancellationToken token)
        {
            var request = new GetMyApplicationRequest
            {
                UserUuid = this.GetUserId().Value,
                JobUuid = id,
            };

            return await _mediator.Send(request, token);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Create(
            [FromForm] CreateJobRequest request, 
            CancellationToken token)
        {
            request.UserUuid = this.GetUserId().Value;
            
            await _mediator.Send(request, token);

            return Ok();
        }

        [Authorize]
        [HttpPost("{id}/Apply")]
        public async Task<IActionResult> ApplyForJob(
            [FromRoute] Guid id,
            [FromBody] ApplyForJobRequest request, 
            CancellationToken token)
        {
            request.UserUuid = this.GetUserId().Value;
            request.JobUuid = id;

            var response = await _mediator.Send(request, token);

            return Ok(response);
        }

        [Authorize]
        [HttpDelete("Application/{id}")]
        public async Task<IActionResult> RevokeApplication(Guid id, CancellationToken token)
        {
            var request = new RevokeApplicationRequest
            {
                UserUuid = this.GetUserId().Value,
                ApplicationUuid = id
            };
            
            await _mediator.Send(request, token);
            return Ok();
        }

        [Authorize]
        [HttpPost("Application/{id}/Negotiate")]
        public async Task<IActionResult> StartApplicationNegotiation([FromRoute] Guid id, CancellationToken token)
        {
            var request = new StartNegotiationRequest
            {
                UserUuid = this.GetUserId().Value,
                ApplicationUuid = id
            };

            await _mediator.Send(request, token);
            return Ok();
        }

        [Authorize]
        [HttpPost("Application/{id}/Reject")]
        public async Task<IActionResult> RejectApplication([FromRoute] Guid id, CancellationToken token)
        {
            var request = new RejectApplicationRequest
            {
                ApplicationUuid = id,
                UserUuid = this.GetUserId().Value,
            };

            await _mediator.Send(request, token);
            return Ok();
        }

        [Authorize]
        [HttpGet("Applied")]
        public async Task<IEnumerable<MyApplicationWithJob>> GetMyApplications(CancellationToken token)
        {
            var request = new GetMyApplicationsRequest
            {
                UserUuid = this.GetUserId().Value,
            };

            return await _mediator.Send(request, token);
        }
    }
}
