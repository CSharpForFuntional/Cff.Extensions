using Flurl.Http;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Cff.Extensions.AspNetCore.Tests;

public class PreludeSpec
{
    [Fact]
    public async Task Test1Async()
    {
        var app = new WebApplicationFactory<Program>();

        var client = app.CreateClient();

        var flurl = new FlurlClient(client);

        _ = await flurl.Request("/api/NotificationMail")
                       .PostJsonAsync(new
                       {
                           Name = "Hello"
                       });
    }
}
