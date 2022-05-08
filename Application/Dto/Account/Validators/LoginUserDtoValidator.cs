using Domain.Entities;
using Domain.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace Application.Dto.Account.Validators
{
    public class LoginUserDtoValidator : AbstractValidator<LoginUserDto>
    {
        public LoginUserDtoValidator(IAccountRepository accountRepository, IPasswordHasher<User> hasher)
        {
            User? user = null;

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(50)
                .Custom((value, context) =>
                {
                    user = accountRepository.GetAllUsers().SingleOrDefault(o => o.Email == value);
                    if (user == null)
                        context.AddFailure("Email and Password", "Niepoprawny adres Email lub Hasło");

                });

            RuleFor(x => x.Password)
                .MinimumLength(8)
                .MaximumLength(30)
                .NotEmpty()
                .Custom((value, context) =>
                {
                    if(user != null)
                    {
                        var result = hasher.VerifyHashedPassword(user, user.PasswordHash, value);
                        if (result != PasswordVerificationResult.Success)
                            context.AddFailure("Email and Password", "Niepoprawny adres Email lub Hasło");
                    }

                });
        }
    }
}
