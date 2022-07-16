using FluentValidation;
using LanguageExt;
using static LanguageExt.Prelude;

namespace Cff.Extensions;

public static class ValidatorExtensions
{
    public static Aff<Unit> ValidateAff<T>(this IValidator<T> validator, T req) =>
        Aff(() => validator.ValidateAndThrowAsync(req).ToUnit().ToValue());
}
