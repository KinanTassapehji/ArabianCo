using Abp.Configuration;
using Abp.Net.Mail;
using System.Collections.Generic;

namespace ArabianCo.Configuration
{
    public class AppSettingProvider : SettingProvider
    {
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new[]
            {
                new SettingDefinition(AppSettingNames.UiTheme, "red", scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User, clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()),
                new SettingDefinition(AppSettingNames.MaxFileSize, "10", scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User, clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()),
                new SettingDefinition(EmailSettingNames.Smtp.Host, "smtp.office365.com", scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User, clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()),
                new SettingDefinition(EmailSettingNames.Smtp.UserName, "maintenance@arabianco.com", scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User, clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()),
                new SettingDefinition(EmailSettingNames.Smtp.Password, "xyvppfvpmzmvcvvn", scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User, clientVisibilityProvider: new VisibleSettingClientVisibilityProvider(),isEncrypted:true),
                new SettingDefinition(EmailSettingNames.Smtp.Port, "587", scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User, clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()),
                new SettingDefinition(EmailSettingNames.DefaultFromDisplayName, "Arabian Company", scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User, clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()),
                new SettingDefinition(EmailSettingNames.DefaultFromAddress, "maintenance@arabianco.com", scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User, clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()),
                new SettingDefinition(EmailSettingNames.Smtp.EnableSsl, "true", scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User, clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()),
                new SettingDefinition(EmailSettingNames.Smtp.UseDefaultCredentials, "false", scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User, clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()),
                new SettingDefinition(EmailSettingNames.Smtp.EnableSsl, "true", scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User, clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()),
            };
        }
    }
}
