
using Newtonsoft.Json;

namespace TestApi
{
    public static class JsonExtensions
    {
        public static string ToJson(this object obj, bool format = false)
        {
            var jsonSerializerSettings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                StringEscapeHandling = StringEscapeHandling.EscapeHtml,
                
            };
            if (format)
            {
                jsonSerializerSettings.Formatting = Formatting.Indented;
            }
            return JsonConvert.SerializeObject(obj, jsonSerializerSettings);
        }
    }

    
}