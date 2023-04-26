using System.Threading;
using System.Threading.Tasks;
using Cff.Extensions.AspNetCore.TestServer.Dto;
using Cff.Extensions.Effects;
using Microsoft.AspNetCore.Http;

using static Cff.Extensions.Effects.IHasHttp<Runtime>;
using static Cff.Extensions.Effects.IHasValidator<Runtime>;

public static class Prelude
{
    public static async ValueTask SendMail
    (
        HttpContext http,
        IValidator<SendMailDto> validator
    ) 
    {
        var q = from req in ReadFromJsonAff<SendMailDto>()
                from __1 in ValidateAff(req)
                select new
                {
                    name = req.Name + "!"
                };

        using var cts = CancellationTokenSource.CreateLinkedTokenSource(http.RequestAborted);
        var rt = new Runtime(http, validator, cts);

        _ = await q.ExecuteAff().Run(rt);
    }
}

public interface IRuntime<RT> : IHasHttp<RT>, IHasValidator<RT> where RT : struct, IRuntime<RT> { }

readonly file record struct Runtime 
(
    HttpContext HttpContext,
    IValidator Validator,
    CancellationTokenSource CancellationTokenSource
) : IHasHttp<Runtime>,
    IHasValidator<Runtime>
{
    HttpContext IHas<Runtime, HttpContext>.It => HttpContext;
    IValidator IHas<Runtime, IValidator>.It => Validator;
}
