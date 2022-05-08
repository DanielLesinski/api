using Domain.Entities.Announcements;
using Domain.Interfaces;
using FluentValidation;

namespace Application.Dto.Announcements.Validators
{
    public class UpdateAnnouncementTypeDtoValidator : AbstractValidator<UpdateAnnouncementTypeDto>
    {
        //private readonly IAnnouncementTypeRepository typeRepository;
        public UpdateAnnouncementTypeDtoValidator(IAnnouncementTypeRepository typeRepository)
        {
            //this.typeRepository = typeRepository;

            RuleFor(o => o.Id)
                .NotEmpty()
                .Custom((value, context) => 
                {
                    var type = typeRepository.GetAll().Any(o => o.Id == value);
                    if (!type)
                        context.AddFailure("Id", "Nie ma takiego typu");
                });

            RuleFor(o => o.Name)
                .NotEmpty()
                .MaximumLength(100)
                .Custom((value, context) =>
                {
                    var type = typeRepository.GetAll().Any(o => o.Name == value);
                    if (type)
                        context.AddFailure("Name", "Podany typ już istnieje");
                });
        }
    }
}
