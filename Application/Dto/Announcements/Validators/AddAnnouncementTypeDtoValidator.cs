using Domain.Interfaces;
using FluentValidation;

namespace Application.Dto.Announcements.Validators
{
    public class AddAnnouncementTypeDtoValidator : AbstractValidator<AddAnnouncementTypeDto>
    {
        public AddAnnouncementTypeDtoValidator(IAnnouncementTypeRepository typeRepository)
        {
            RuleFor(o => o.Name)
                .NotEmpty()
                .MaximumLength(100)
                .Custom((value, context) =>
                {
                    var type = typeRepository.GetAll().Any(o => o.Name == value);
                    if (type)
                        context.AddFailure("Name", "Podany typ już istneije");
                });
        }
    }
}
