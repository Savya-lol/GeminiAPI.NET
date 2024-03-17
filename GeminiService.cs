using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

  public class GeminiService
  {
      private static readonly HttpClient _client = new HttpClient();
      private readonly string _apiKey;

      public GeminiService(string apiKey)
      {
          _apiKey = apiKey;
      }

      public async Task<string> GetAIResponseAsync(string user_input)
      {
          try
          {
              var requestData = new
              {
                  contents = new[]
                  {
                  new
                  {
                      role = "user",
                      parts = new[]
                      {
                          new
                          {
                              text = user_input
                          }
                      }
                  }
              },
                  generationConfig = new
                  {
                      temperature = 0.9,
                      topK = 1,
                      topP = 1,
                      maxOutputTokens = 2048,
                      stopSequences = new string[] { }
                  },
                  safetySettings = new[]
                  {
                  new
                  {
                      category = "HARM_CATEGORY_HARASSMENT",
                      threshold = "BLOCK_MEDIUM_AND_ABOVE"
                  },
                  new
                  {
                      category = "HARM_CATEGORY_HATE_SPEECH",
                      threshold = "BLOCK_MEDIUM_AND_ABOVE"
                  },
                  new
                  {
                      category = "HARM_CATEGORY_SEXUALLY_EXPLICIT",
                      threshold = "BLOCK_MEDIUM_AND_ABOVE"
                  },
                  new
                  {
                      category = "HARM_CATEGORY_DANGEROUS_CONTENT",
                      threshold = "BLOCK_MEDIUM_AND_ABOVE"
                  }
              }
              };

              var jsonContent = JsonConvert.SerializeObject(requestData);
              var responseString = await PostRequestAsync(jsonContent);

              var jsonResponse = JObject.Parse(responseString);
              var generatedText = GetGeneratedText(jsonResponse);

              return !string.IsNullOrEmpty(generatedText) ? generatedText : "No response from the bot.";
          }
          catch (Exception ex)
          {
              Console.WriteLine($"Error: {ex.Message}");
              return "An error occurred. Please try again later.";
          }
      }

      private async Task<string> PostRequestAsync(string jsonContent)
      {
          string apiEndpoint = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.0-pro:generateContent?key={_apiKey}";

          var requestContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
          var response = await _client.PostAsync(apiEndpoint, requestContent);
          response.EnsureSuccessStatusCode();

          return await response.Content.ReadAsStringAsync();
      }

      private static string GetGeneratedText(JObject jsonResponse)
      {
          var content = jsonResponse["candidates"]?[0]?["content"];
          var parts = content?["parts"];
          return parts?[0]?["text"]?.ToString();
      }
  }
