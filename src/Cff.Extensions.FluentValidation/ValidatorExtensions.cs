namespace Cff.Extensions;

public static class ValidatorExtensions
{
    public static Aff<Unit> ValidateAff<T>(this IValidator<T> validator, T req) =>
        Aff(() => validator.ValidateAndThrowAsync(req).ToUnit().ToValue());
}
