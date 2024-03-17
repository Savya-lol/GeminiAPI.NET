# 🚀 GeminiAPI.NET

GeminiAPI.NET makes integrating Google's Gemini into your .NET applications a breeze!

## Requirements
- Tested on .NET 8.0
- Newtonsoft.Json library

## 🛠️ How to Use
It's as easy as 1️⃣, 2️⃣, 3️⃣!

1. **Clone & Add**: Clone the repository and include `GeminiService.cs` in your project.
2. **Instantiate**: Create an object of the `GeminiService` class with your `apiKey`.

    ```csharp
    private readonly GeminiService geminiService;
    geminiService = new GeminiService(apiKey);
    ```

3. **Get Response**: Retrieve the response message by calling the asynchronous `GetAIResponseAsync()` function with the prompt.

    ```csharp
    string AIresponse = await geminiService.GetAIResponseAsync(prompt);
    ```

And that's it! 🎉 You're ready to tap into Gemini's powers right within your .NET application.

🌟 **Feel free to customize, enhance, or contribute to the project on GitHub!**

---
