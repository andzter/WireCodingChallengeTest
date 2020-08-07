using System;
using System.Configuration;

namespace WireCodingChallenge.Lib
{
    public static class Settings
    {
 

        public static string Get(string key)
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                if (config.AppSettings.Settings[key] != null) return config.AppSettings.Settings[key].Value;
                return "";
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string ApplicationPath()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }
         

    }
}
