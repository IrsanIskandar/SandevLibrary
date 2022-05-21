using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SandevLibrary.Extensions
{
    public static class JsonConverterExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static string ObjectToJson<T>(this T instance) => JsonConvert.SerializeObject(instance, JsonSettings.SerializerDefaults);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T JsonToObject<T>(this string json) => JsonConvert.DeserializeObject<T>(json, JsonSettings.SerializerDefaults);
    }

    internal static class JsonSettings
    {
        static readonly DefaultContractResolver _contractResolver =
            new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

        internal static JsonSerializerSettings SerializerDefaults { get; } = new JsonSerializerSettings
        {
            DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
            ContractResolver = _contractResolver,
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore
        };
    }
}
