using System.Text.Json;
using System.Text.Json.Serialization;

namespace AssignmentManager.Client.Extensions
{
    public static class JsonSerializerCustomOptions
    {
        public static JsonSerializerOptions GetOptions()
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.Converters.Add(new JsonStringEnumConverter());
            options.PropertyNameCaseInsensitive = true;
            options.ReferenceHandler = ReferenceHandler.Preserve;
            return options;
        }
        
    }
}