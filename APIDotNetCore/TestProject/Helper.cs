using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace TestProject
{
    public class Helper
    {
        public static string Token { get; set; } = string.Empty;

        public void GetToken()
        {
            string urlApiGeraToken = "https://localhost:7006/api/CreateTokenIdentity";

            using (var httpClient = new HttpClient())
            {
                var data = new
                {
                    email = "pedro.test@testmail.com",
                    password = "Pedro@1234",
                    document = "123456789"
                };

                string JsonObject = JsonConvert.SerializeObject(data);
                var content = new StringContent(JsonObject, Encoding.UTF8, "application/json");

                var result = httpClient.PostAsync(urlApiGeraToken, content);
                result.Wait();
                if (result.Result.IsSuccessStatusCode)
                {
                    var tokenJson = result.Result.Content.ReadAsStringAsync();
                    Token = JsonConvert.DeserializeObject(tokenJson.Result).ToString();
                }
            }
        }

        public string execApiGet(
            bool hasAuth, 
            string url,
            string method)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Clear();
                if (hasAuth)
                {
                    GetToken();

                    if (string.IsNullOrWhiteSpace(Token))
                        return null;

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                }
                var response = httpClient.GetStringAsync(url + method);
                response.Wait();
                return response.Result;
            }
        }

        public async Task<string> execApiPost(
            bool hasAuth, 
            string url,
            string method,
            object data = null)
        {
            string JsonObjeto = data != null ? JsonConvert.SerializeObject(data) : "";
            var content = new StringContent(JsonObjeto, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Clear();
                if (hasAuth)
                {
                    GetToken();

                    if (string.IsNullOrWhiteSpace(Token))
                        return null;

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                }
                var response = httpClient.PostAsync(url + method, content);
                response.Wait();
                if (response.Result.IsSuccessStatusCode)
                {
                    var ret = await response.Result.Content.ReadAsStringAsync();

                    return ret;
                }
            }

            return null;

        }
    }
}
