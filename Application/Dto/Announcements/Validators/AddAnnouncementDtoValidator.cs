using Domain.Interfaces;
using FluentValidation;

namespace Application.Dto.Announcements.Validators
{
    public class AddAnnouncementDtoValidator : AbstractValidator<AddAnnouncementDto>
    {
        public AddAnnouncementDtoValidator(IAnnouncementTypeRepository typeRepository, IAccountRepository accountRepository)
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Content)
                .NotEmpty()
                .MaximumLength(4000);
            /*
            RuleFor(o => o.UserId)
                .NotEmpty()
                .Custom((value, context) =>
                {
                    var user = accountRepository.GetById(value);
                    if (user == null)
                        context.AddFailure("User", "Podany użytkownik nie istnieje.");
                });
            */
            RuleFor(x => x.AnnouncementTypeId)
                .NotEmpty()
                .Custom((value, context) =>
                {
                    var type = typeRepository.GetById(value);
                    if (type == null)
                        context.AddFailure("AnnouncementType", "Podany typ ogłoszenia nie istnieje.");

                });


        }
    }
}
