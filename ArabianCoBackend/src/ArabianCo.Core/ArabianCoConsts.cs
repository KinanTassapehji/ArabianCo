using ArabianCo.Debugging;

namespace ArabianCo
{
    public class ArabianCoConsts
    {
        public const string LocalizationSourceName = "ArabianCo";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = false;

        public const string AppServerRootAddressKey = "App:ServerRootAddress";


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "e3103c6bf4304b32b19850f156860b34";
    }
}
