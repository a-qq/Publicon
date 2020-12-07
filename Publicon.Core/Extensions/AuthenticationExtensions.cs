using Publicon.Core.Exceptions;
using System;
using System.Security.Claims;

namespace Publicon.Core.Extensions
{
    public static class AuthExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal principal)
        {
            var id = principal.Identity.Name;

            if (string.IsNullOrEmpty(id))
            {
                throw new PubliconException(ErrorCode.InvalidUserId);
            }
            return Guid.Parse(id);
        }
    }
}
