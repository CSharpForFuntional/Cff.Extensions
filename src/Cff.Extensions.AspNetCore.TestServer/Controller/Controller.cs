using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Cff.Extensions.AspNetCore.TestServer.Dto;
using Microsoft.AspNetCore.Http;

public static class Prelude
{
    public static async ValueTask<IResult> SendMail(HttpContext http, IValidator<SendMailDto> validator) 
    {
        var q = from req in http.ReadFromJsonAff<SendMailDto>()
                from _1 in validator.ValidateAff(req)
                select req;

        return match(await q.Run(), Results.Ok, ResultsError);
    }
}
