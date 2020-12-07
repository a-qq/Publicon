using Microsoft.AspNetCore.Authorization;

namespace Publicon.Api.Authorization
{
    public class MustBeOwnerOrAdminRequirement : IAuthorizationRequirement
    {
        public MustBeOwnerOrAdminRequirement() { }
    }
}
