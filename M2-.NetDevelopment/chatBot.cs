using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () =>
{
    return Results.Content(@"
<!DOCTYPE html>
<html>
<head>
    <title>C# ChatBot</title>
    <style>
        body{
            font-family:Arial;
            max-width:800px;
            margin:auto;
            padding:20px;
            background:#f4f4f4;
        }

        .chat{
            background:white;
            padding:20px;
            border-radius:10px;
        }

        input{
            width:70%;
            padding:10px;
        }

        button{
            padding:10px 20px;
            cursor:pointer;
        }

        #messages{
            margin-top:20px;
        }

        .user{
            color:blue;
            margin-bottom:10px;
        }

        .bot{
            color:green;
            margin-bottom:15px;
        }
    </style>
</head>
<body>

<div class='chat'>
    <h1>Simple C# ChatBot</h1>

    <input id='message' placeholder='Say something...' />
    <button onclick='sendMessage()'>Send</button>

    <div id='messages'></div>
</div>

<script>

async function sendMessage()
{
    const input = document.getElementById('message');
    const text = input.value;

    if(text.trim() === '')
        return;

    const response = await fetch('/chat?message=' + encodeURIComponent(text));

    const botReply = await response.text();

    const messages = document.getElementById('messages');

    messages.innerHTML +=
        '<div class=""user""><strong>You:</strong> '
        + text +
        '</div>';

    messages.innerHTML +=
        '<div class=""bot""><strong>Bot:</strong> '
        + botReply +
        '</div>';

    input.value = '';
}

</script>

</body>
</html>
", "text/html");
});

app.MapGet("/chat", (string message) =>
{
    message = message.ToLower();

    if (message.Contains("hello"))
        return "Hello! Nice to meet you.";

    if (message.Contains("hi"))
        return "Hi there!";

    if (message.Contains("how are you"))
        return "I'm doing great, thanks for asking.";

    if (message.Contains("name"))
        return "I'm a chatbot built with C# and ASP.NET Core.";

    if (message.Contains("time"))
        return $"Current server time: {DateTime.Now:T}";

    if (message.Contains("date"))
        return $"Today's date is {DateTime.Now:d}";

    if (message.Contains("bye"))
        return "Goodbye!";

    return "Sorry, I don't understand that yet.";
});

app.Run();