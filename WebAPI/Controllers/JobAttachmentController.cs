using Azure.Core;
using Core.Jobs.Attachment;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.OpenApi.Validations;
using System.Reflection.Metadata.Ecma335;


namespace WebAPI.Controllers
{
    [Route("api/Job/Attachment")]
    [ApiController]
    public class JobAttachmentController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly FileExtensionContentTypeProvider _contentTypeProvider;


        public JobAttachmentController(IMediator mediator)
        {
            _mediator = mediator;
            _contentTypeProvider = new FileExtensionContentTypeProvider();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var request = new DownloadJobAttachmentRequest
            {
                AttachmentUuid = id,
            };
            
            var response = await _mediator.Send(request);

            //return Ok(response);
            return new FileStreamResult(response.FileContent, response.ContentType)
            {
                FileDownloadName = response.FileName
            };
        }
    }
}
