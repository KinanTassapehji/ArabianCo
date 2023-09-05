using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace ArabianCo.Controllers
{
    public abstract class ArabianCoControllerBase: AbpController
    {
        protected ArabianCoControllerBase()
        {
            LocalizationSourceName = ArabianCoConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
