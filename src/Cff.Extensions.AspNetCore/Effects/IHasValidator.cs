using FluentValidation;

namespace Cff.Extensions.Effects;

public interface IHasValidator<RT> : IHas<RT, IValidator> where RT : struct, IHasValidator<RT>
{
    public static Aff<RT, Unit> ValidateAff<T>(T req) =>
        from validator in Eff
        from _ in Aff(async () =>
        {
            var ret = await validator.ValidateAsync(new ValidationContext<T>(req));
            return ret.IsValid switch
            {
                false => throw new ValidationException(ret.Errors),
                _ => unit
            };
        })
        select unit;
}
