using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using ArabianCo.Configuration.Dto;

namespace ArabianCo.Configuration
{
    [AbpAuthorize]
    internal class ConfigurationAppService : ArabianCoAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
