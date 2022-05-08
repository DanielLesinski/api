using Domain.Interfaces;
using FluentValidation;

namespace Application.Dto.AccountRole.Validators
{
    public class RoleDtoValidator : AbstractValidator<RoleDto>
    {
        public RoleDtoValidator(IAccountRoleRepository accountRoleRepository)
        {
            
            RuleFor(x => x.Name)
                .NotEmpty()
                .Custom((value, context) =>
                {
                    var role = accountRoleRepository.GetAllRoles().SingleOrDefault(o => o.Name == value);
                    if (role != null)
                        context.AddFailure("Name", "Taka rola już istnieje.");

                });

        }
    }
}
