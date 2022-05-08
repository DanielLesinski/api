using Domain.Interfaces;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Application.Dto.Account.Validators
{
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator(IAccountRepository accountRepository)
        {
            RuleFor(x => x.Password)
                .MinimumLength(8)
                .MaximumLength(30);

            RuleFor(x => x.ConfirmPassword)
                .Equal(e => e.Password);

            RuleFor(x => x.Email)
                .EmailAddress()
                .MaximumLength(50)
                .Custom((value, context) =>
                {
                    var emailIsNotNull = accountRepository.GetAllUsers().Any(o => o.Email == value);
                    if (emailIsNotNull)
                        context.AddFailure("Email", "Ten adres jest zajęty");

                });

            RuleFor(x => x.PhoneNumber)
                .MinimumLength(9)
                .MaximumLength(12)
                .Matches(new Regex(@"^[0-9+]*")).WithMessage("Numer telefony jest niepoprawny");

        }
    }
}
