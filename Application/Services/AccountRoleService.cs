using Application.Dto.AccountRole;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class AccountRoleService : IAccountRoleService
    {
        private readonly IAccountRoleRepository accountRoleRepository;
        private readonly IMapper mapper;

        public AccountRoleService(IAccountRoleRepository accountRoleRepository, IMapper mapper)
        {
            this.accountRoleRepository = accountRoleRepository;
            this.mapper = mapper;
        }

        public async Task<RoleOutDto> AddRole(RoleDto roleDto)
        {
            var role = mapper.Map<Role>(roleDto);
            var newRole = await accountRoleRepository.AddRole(role);
            return mapper.Map<RoleOutDto>(newRole);
        }

        public async Task DeleteRole(int id)
        {
            var role = await accountRoleRepository.GetRoleById(id);
            if (role == null)
                throw new NotFoundException("Nie znaleziono zasobu");
            await accountRoleRepository.DeleteRole(role);
        }

        public async Task<IList<RoleOutDto>> GetAllRoles()
        {
            var roles = await accountRoleRepository.GetAllRoles().ToListAsync();
            return mapper.Map<IList<RoleOutDto>>(roles);
        }
    }
}
