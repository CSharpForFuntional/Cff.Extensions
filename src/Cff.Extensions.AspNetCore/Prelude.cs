using FluentValidation;
using LanguageExt.Common;
using Microsoft.AspNetCore.Http;

namespace Cff.Extensions;

public partial class Prelude
{
    public static IResult ResultsError(Error err) => err.Exception.Case switch
    {
        ValidationException e =>
            Results.ValidationProblem(e.Errors
                                       .GroupBy(x => x.PropertyName)
                                       .ToDictionary(g => g.Key,
                                                     g => g.Select(x => x.ErrorMessage)
                                                           .ToArray())),
        _ => Results.Problem(err.ToString())
    };
}
