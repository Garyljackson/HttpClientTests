using System.Text.Json;

namespace ConsoleApp
{
    public static class DefaultJsonSerializerOptions
    {
        public static JsonSerializerOptions Options =>
            new JsonSerializerOptions
                {PropertyNameCaseInsensitive = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase};
    }
}