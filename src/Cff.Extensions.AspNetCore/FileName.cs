using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Cff.Extensions;

public static class HostExtension
{
    public static IHostBuilder UseRemoveValidator(this IHostBuilder hostBuilder) =>
        hostBuilder.ConfigureServices((context, services) =>
        {
            _ = services.AddControllers(options =>
                options.ModelValidatorProviders.Clear());
            _ = services.Configure<ApiBehaviorOptions>(options =>
                options.SuppressModelStateInvalidFilter = true);
        });
}
