using System.Security.Claims;

namespace SAED.Persistence.Extensions
{
    public static class UserExtensions
    {
        public static int? GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            var userId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null)
            {
                return null;
            }

            return int.Parse(userId);
        }
    }
}