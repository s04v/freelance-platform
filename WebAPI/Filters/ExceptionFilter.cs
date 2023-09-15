using Core.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace WebAPI.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var ex = context.Exception;
            if (ex.GetType() == typeof(ErrorsException)) 
            {
                var errorResponse = new ErrorResponse
                {
                    Errors = context.Exception.Message.Split(';'),
                };

                var result = new JsonResult(errorResponse)
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                };

                context.Result = result;
                
                return;
            } 
        }
    }
}
