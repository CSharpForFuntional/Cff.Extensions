var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/api/NotificationMail", async (HttpContext http) =>
{
    var q = from __ in unitEff
            from req in http.ReadFromJsonAff<Dto>()
            from _1 in new DtoValidator().ValidateAff(req)
            select req;

    return match(await q.Run(), Results.Ok, ResultsError);
}).Accepts<Dto>("application/json");

await app.RunAsync();

internal record Dto();

internal class DtoValidator : AbstractValidator<Dto>
{
    public DtoValidator()
    {
    }
}

public partial class Program { }
