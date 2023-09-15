using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Extensions
{
    public static class ControllerExtensions
    {
        public static Guid? GetUserId(this ControllerBase controller)
        {
            var id = controller.HttpContext.User?.Claims?.FirstOrDefault(o => o.Type == "id")?.Value;
            
            if (id != null)
            {
                return new Guid(id);
            }

            return null;
        }
    }
}
