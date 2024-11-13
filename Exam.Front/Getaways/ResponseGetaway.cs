using Newtonsoft.Json;

namespace Exam.Front.Getaways
{
    public class ResponseGetaway : IResponseGetaway
    {
        private readonly IConfiguration _configuration;

        public ResponseGetaway(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> GetAsync(string url)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(_configuration["ApiBaseAddress"]);

            var response = await client.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> PostAsync<T>(T t, string url)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(_configuration["ApiBaseAddress"]);

            StringContent httpContent = new StringContent(JsonConvert.SerializeObject(t, Formatting.Indented), System.Text.Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, httpContent);
            return await response.Content.ReadAsStringAsync();
        }
        public async Task<string> PutAsync<T>(T t, string url)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(_configuration["ApiBaseAddress"]);

            StringContent httpContent = new StringContent(JsonConvert.SerializeObject(t, Formatting.Indented), System.Text.Encoding.UTF8, "application/json");

            var response = await client.PutAsync(url, httpContent);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
