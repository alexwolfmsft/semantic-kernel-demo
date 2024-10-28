using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using SemanticKernelIntro;

var builder = Kernel.CreateBuilder();

builder.AddAzureOpenAIChatCompletion("gpt-4o", "https://alexai2.openai.azure.com/", "357517bc2dd6453eaf6393996c912433");
builder.Plugins.AddFromType<NewsPlugin>();
builder.Plugins.AddFromType<ArchivePlugin>();

Kernel kernel = builder.Build();

var chatService = kernel.GetRequiredService<IChatCompletionService>();

ChatHistory chatMessages = new ChatHistory();

while (true)
{
    Console.WriteLine("Prompt:");
    chatMessages.AddUserMessage(Console.ReadLine());

    var completions = chatService.GetStreamingChatMessageContentsAsync(
        chatMessages,
        executionSettings: new OpenAIPromptExecutionSettings()
        {
            ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions,
        },
        kernel: kernel);

    string fullMessage = "";
    await foreach(var content in completions)
    {
        Console.Write(content.Content);
        fullMessage += content.Content;
    }

    chatMessages.AddAssistantMessage(fullMessage);
    Console.WriteLine();
}

// Plugins
