using Domain.Interfaces;
using FluentValidation;

namespace Application.Dto.Announcements.Validators
{
    public class UpdateAnnouncementDtoValidator : AbstractValidator<UpdateAnnouncementDto>
    {
        public UpdateAnnouncementDtoValidator(IAnnouncementTypeRepository typeRepository, IAnnouncementRepository repository)
        {
            RuleFor(o => o.Id)
                .NotEmpty()
                .Custom((value, context) =>
                {
                    var announcement = repository.GetAll().Any(o => o.Id == value);
                    if (!announcement)
                        context.AddFailure("Id", "Nie ma takiego ogłoszenia");
                });

            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Content)
                .NotEmpty()
                .MaximumLength(4000);

            RuleFor(x => x.AnnouncementTypeId)
                .NotEmpty()
                .Custom((value, context) =>
                {
                    var type = typeRepository.GetAll().Any(o => o.Id == value);
                    if (!type)
                        context.AddFailure("AnnouncementType", "Podany typ ogłoszenia nie istnieje.");

                });
        }
    }
}
