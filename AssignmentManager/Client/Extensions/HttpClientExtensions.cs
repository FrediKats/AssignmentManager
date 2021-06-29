#nullable enable
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AssignmentManager.Client.Extensions
{
    public static class HttpClientExtensions
    {

        public static async Task<T?> GetJson<T>(this HttpClient client, string url)
        {
            return await client.GetFromJsonAsync<T>(url, JsonSerializerCustomOptions.GetOptions());
        }
        
        
    }
}