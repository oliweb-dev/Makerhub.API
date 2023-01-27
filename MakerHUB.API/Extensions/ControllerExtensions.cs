using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MakerHUB.API.Extensions
{
    public static class ControllerExtensions
    {
        public static int GetUserId(this ControllerBase controller)
        {
            return int.Parse(controller.User.FindFirstValue(ClaimTypes.NameIdentifier));
        }
    }
}
