namespace Cff.Extensions.AspNetCore.TestServer.Dto;

internal record PersonDto
{
    public string Name { get; init; }
}

internal class DtoValidator : AbstractValidator<PersonDto>
{
    public DtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}
