using System.Threading.Tasks;
using Cff.Extensions.AspNetCore.TestServer.Dto;
using Microsoft.AspNetCore.Http;

namespace Cff.Extensions.AspNetCore.TestServer.Controller;

public static partial class Controller
{
    public static async ValueTask<IResult> NewMethod(HttpContext http) 
    {
        var q = from req in http.ReadFromJsonAff<PersonDto>()
                from _1 in new DtoValidator().ValidateAff(req)
                select req;

        return match(await q.Run(), Results.Ok, ResultsError);
    }
}
