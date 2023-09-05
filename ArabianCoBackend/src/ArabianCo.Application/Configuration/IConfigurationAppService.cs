using System.Threading.Tasks;
using ArabianCo.Configuration.Dto;

namespace ArabianCo.Configuration
{
    internal interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
