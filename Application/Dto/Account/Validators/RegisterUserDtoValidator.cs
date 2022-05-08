using Domain.Interfaces;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Application.Dto.Account.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(IAccountRepository accountRepository)
        {
            RuleFor(x => x.Password)
                .MinimumLength(8)
                .MaximumLength(30)
                .NotEmpty();

            RuleFor(x => x.ConfirmPassword)
                .Equal(e => e.Password)
                .NotEmpty();

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(50)
                .Custom((value, context) =>
                {
                    var emailIsNotNull = accountRepository.GetAllUsers().Any(o => o.Email == value);
                    if (emailIsNotNull)
                        context.AddFailure("Email", "Ten adres jest zajęty");
                        
                });

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .MinimumLength(9)
                .MaximumLength(12)
                .Matches(new Regex(@"^[0-9+]*")).WithMessage("Numer telefony jest niepoprawny");

            RuleFor(x => x.Name)
                .NotEmpty();

            RuleFor(x => x.LastName)
                .NotEmpty();

        }
    }
}
