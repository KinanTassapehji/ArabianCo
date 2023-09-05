using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace ArabianCo.Localization
{
    public static class ArabianCoLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(ArabianCoConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(ArabianCoLocalizationConfigurer).GetAssembly(),
                        "ArabianCo.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
