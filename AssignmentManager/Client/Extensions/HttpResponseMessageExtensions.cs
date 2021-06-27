#nullable enable
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AssignmentManager.Client.Extensions
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task<object?> ReadJson<T>(this HttpContent client) where T : class
        {
            return await client.ReadFromJsonAsync(typeof(T), JsonSerializerCustomOptions.GetOptions());
        }
    }
}