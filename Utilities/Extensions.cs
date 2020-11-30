using Microsoft.Extensions.Configuration;

namespace ODataSample {
    public static class Extensions {
        public static T GetConfigValue<T>(this IConfiguration config, string name, T defaultValue = default(T)) {
            return (T)config.GetValue(typeof(T), name, defaultValue);
        }
    }
}