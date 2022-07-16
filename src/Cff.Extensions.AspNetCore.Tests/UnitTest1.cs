using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Cff.Extensions;
using FluentValidation;
using Microsoft.AspNetCore.TestHost;
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

        var ret = await flurl.Request("/api/NotificationMail")
                             .PostJsonAsync(new { });
    }

   
}
