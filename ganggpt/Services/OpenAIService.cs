using System;
using System.Net.Http;
using ganggpt.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Linq;
using System.Text.Json;

namespace ganggpt.Services
{
    public class OpenAIService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public OpenAIService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["OpenAI:ApiKey"];
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
        }

        public async Task<string> GenerateResponseAsync(string prompt)
        {
            string ukSlangPrompt = $"[reply in uk slang UK Slang] {prompt}";

            var data = new
            {
                model = "gpt-3.5-turbo-0301",
                messages = new[]
                {
                    new { role = "system", content = "You are a helpful assistant that speaks in UK slang." },
                    new { role = "user", content = ukSlangPrompt }
                },
                max_tokens = 250,
                temperature = 0.4,
                top_p = 1
            };

            var response = await _httpClient.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", data);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"OpenAI response content: {responseContent}");

                var result = JsonSerializer.Deserialize<OpenAIResponse>(responseContent);
                Console.WriteLine($"OpenAI response result: {JsonSerializer.Serialize(result)}");

                var assistantResponse = result.Choices[0].Message;
                if (assistantResponse != null && assistantResponse.Role == "assistant")
                {
                    return assistantResponse.Content.Trim();
                }
                else
                {
                    throw new Exception("Error extracting the assistant's response.");
                }
            }
            else
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"OpenAI response content: {responseContent}");
                throw new Exception("Error generating response from ChatGPT.");
            }
        }

    }
}
