using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace Cff.Extensions;

public static class HttpContextExt
{
    public static async Task ExecuteAsync<T>(this HttpContext context, Aff<T> aff, Func<T, IResult> succ) =>
        await match(await aff.Run(), succ, ResultsError).ExecuteAsync(context);

    public static Aff<T?> ReadFromJsonAff<T>(this HttpContext http) =>
        Aff(() => http.Request.ReadFromJsonAsync<T>(new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }, http.RequestAborted));

    public static Aff<Unit> ExecuteAff<T>(this HttpContext http, T )
}
