#nullable enable
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AssignmentManager.Client.Extensions
{
    public static class HttpContentExtensions
    {
        public static async Task<T?> ReadJson<T>(this HttpContent client)
        {
            return await client.ReadFromJsonAsync<T>(JsonSerializerCustomOptions.GetOptions());
        }
    }
}