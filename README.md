# jsonclient #

JsonClient .NET is a lightweight .NET class library that lets you access Json web services. It's asynchronous, it's
dynamic and it's simple.

## Installation ##

You can install JsonClient for a .NET project using Nuget, just search for the package 'JsonClient' and install
it. You can use the package manager as well:

```
PM> Install-Package JsonClient
```

## Golden Rules ##

All of the code is documented and the library is very easy to use. Just remember these three golden rules!

### 1. Strings are Special ###

When you call any of the functions that send data, they automatically convert the C# object into Json, so you 
can do this:

```c#
var result = await JsonClient.PostAsync("http://localhost:3000/posts",
  new { 
    title = "New Post",
    author = "Dave",
    content = "My new blog post."
    });
```

or this:

```c#
var result = await JsonClient.PostAsync("http://localhost:3000/posts",
  new Post("New Post", "Dave", "My new blog post"));
```

In these cases, JsonClient will convert the objects to Json. However, if you pass a string, then no conversion occurs.
This is because JsonClient always assumes that if you provide it with a string, it's in json already. This is because
it makes it easier to do this:

```c#
var result = await JsonClient.PostAsync("http://localhost:3000/posts",
  @"{""title"":""New Post"",""author"":""Dave"", ""content"":""My new blog post"");
```

### 2. Async is only available with .NET 4.5 ###

If you have a .NET 4.5 project, installing JsonClient gives you full access to the async functionality - you
can await any of the methods:

```c#
var result = await JsonClient.GetAsync("http://localhost:3000/posts");
```

Async and await are only available in .NET 4.5. If you're using .NET 4 or earlier, you'll need to use the synchronous 
calls:

```c#
var result = JsonClient.Get("http://localhost:3000/posts");
```

### 3. Advanced features are there if you need them! ###

In most examples, you see the static JsonClient functions being called. If you need any advanced functionality,
just create an instance of JsonClient - and customise that!

```c#
var client = new JsonClient("http://localhost:3000");
client.CustomHeaders.Add("X-Custom-Header", "Something");
client.Timeout = 4000;
var result = await client.GetAsync("/users");
```

## Handling Exceptions ##

JsonClient can work with dynamic objects and calls web services. You never need to wrap calls to JsonClient in 
exception handlers because it'll handle exceptions for you:

```c#
var result = await JsonClient.GetAsync("http://localhost:3000/posts");
if(result.Response.StatusCode == HttpStatusCode.NotFound)
   Console.WriteLine("Not found!");
```

In the example above, the WebException is not thrown, it is caught and the details are stored in the Response. You can
always get the exception object if you need it:

```c#
var theException = result.Error;
```
