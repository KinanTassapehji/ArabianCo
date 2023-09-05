using Abp.Authorization;
using ArabianCo.Authorization.Roles;
using ArabianCo.Authorization.Users;

namespace ArabianCo.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
