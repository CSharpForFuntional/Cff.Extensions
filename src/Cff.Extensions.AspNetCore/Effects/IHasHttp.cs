using System.Diagnostics;
using System.Dynamic;
using System.Text.Json;
using FluentValidation;
using LanguageExt;
using LanguageExt.Effects.Traits;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cff.Extensions.Effects;

public interface IHasCancel<RT> : HasCancel<RT> where RT : struct, IHasCancel<RT>
{
    RT HasCancel<RT>.LocalCancel => default;
    CancellationToken HasCancel<RT>.CancellationToken => CancellationTokenSource.Token;
}

public interface IHas<RT, T> : IHasCancel<RT> where RT : struct, IHas<RT, T>
{
    protected T It { get; }

    public static Eff<RT, T> Eff => Eff<RT, T>(static rt => rt.It);
}

public interface IHasHttp<RT> : IHas<RT, HttpContext> where RT : struct, IHasHttp<RT>
{

    public static JsonSerializerOptions JsonSerializerOptions { get; } = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public static Aff<RT, T> ReadFromJsonAff<T>() =>
        from http in Eff
        from res in Aff(() => http.Request.ReadFromJsonAsync<T>
        (
            JsonSerializerOptions,
            http.RequestAborted
        ))
        select res;

    public static Aff<RT, Unit> ExecuteAff<T>(Aff<RT, T> aff) =>
        from http in Eff
        from res in aff.Map(x =>
        {
            var expando = JsonSerializer.SerializeToNode(x, JsonSerializerOptions)!;

            expando.Root["traceId"] = Activity.Current?.Id ?? http.TraceIdentifier;

            return Results.Text(expando.ToJsonString(JsonSerializerOptions), "text/json", System.Text.Encoding.UTF8);
        })
        | @catch(e => true, ex => Eff(() => ex switch
        {
            ValidationException e =>
                Results.ValidationProblem(e.Errors
                                           .GroupBy(x => x.PropertyName)
                                           .ToDictionary(g => g.Key,
                                                         g => g.Select(x => x.ErrorMessage)
                                                               .ToArray())),
            _ => Results.Problem(ex.ToString())
        }))
        from _1 in Aff(async () =>
        {
            await res.ExecuteAsync(http);
            return unit;
        })
        select unit;
}

public static class Extensions
{
    public static Aff<RT, Unit> ExecuteAff<RT, T>(this Aff<RT, T> aff) where RT : struct, IHasHttp<RT> =>
        IHasHttp<RT>.ExecuteAff(aff);
}
