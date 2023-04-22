namespace Cff.Extensions.AspNetCore.TestServer.Dto;

public class SendMailDtoValidator : AbstractValidator<SendMailDto>
{
    public SendMailDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}
