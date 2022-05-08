using Application.Dto;
using Application.Dto.Account;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Application.Exceptions;

namespace Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository accountRepository;
        private readonly IMapper mapper;
        private readonly IPasswordHasher<User> hasher;
        private readonly AuthenticationSettings authenticationSettings;
        private readonly IUserContextService userContextService;

        public AccountService(IAccountRepository accountRepository, IMapper mapper,
            IPasswordHasher<User> hasher, AuthenticationSettings authenticationSettings,
            IUserContextService userContextService)
        {
            this.accountRepository = accountRepository;
            this.mapper = mapper;
            this.hasher = hasher;
            this.authenticationSettings = authenticationSettings;
            this.userContextService = userContextService;
        }

        
        public async Task RegisterUser(RegisterUserDto registerUserDto)
        {
            var newUser = mapper.Map<User>(registerUserDto);
            var hashedPassword = hasher.HashPassword(newUser, registerUserDto.Password);
            newUser.PasswordHash = hashedPassword;
            await accountRepository.RegisterUser(newUser);
        }

        public async Task<string> GenerateJwt(LoginUserDto loginUserDto)
        {
            var user =  await accountRepository.GetAllUsers().SingleOrDefaultAsync(o => o.Email == loginUserDto.Email);
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.Name} {user.LastName}"),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(
                authenticationSettings.JwtIssuer,
                authenticationSettings.JwtIssuer,
                claims,
                expires : expires,
                signingCredentials: cred);
            
            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }

        public async Task<GetUserDto> GetUser(int id)
        {
            var user = await accountRepository.GetById(id);
            var getUser = mapper.Map<GetUserDto>(user);
            return getUser;
        }

        public async Task<GetUserDto> GetInfoAboutYourself()
        {
            int id = (int)userContextService.GetUserId;
            var user = await accountRepository.GetById(id);
            var getUser = mapper.Map<GetUserDto>(user);
            return getUser;
        }

        public async Task<ListUserDto> GetAllUser()
        {
            var users = accountRepository.GetAllUsers();
            return mapper.Map<ListUserDto>(users);
        }

        public async Task<ListUserDto> SearchByKeyword(string keyword)
        {
            keyword = keyword.ToLowerInvariant();
            var user = accountRepository.GetAllUsers()
                .Where(o => o.Name.ToLower().Contains(keyword) || o.LastName.ToLower().Contains(keyword)).ToList();
            return mapper.Map<ListUserDto>(user);
        }

        public async Task UpdateUser(int id, UpdateUserDto updateUserDto)
        {
            var existingUser = await accountRepository.GetById(id);
            var updatedUser = mapper.Map(updateUserDto, existingUser);

            if(updateUserDto.Password != null)
            {
                var hashedPassword = hasher.HashPassword(updatedUser, updateUserDto.Password);
                updateUserDto.Password = hashedPassword;
            }

            await accountRepository.UpdateUser(updatedUser);
        }

        public async Task DeleteUser(int id)
        {
            var user = await accountRepository.GetById(id);
            if (user == null)
                throw new NotFoundException("Nie znaleziono zasobu");
            await accountRepository.DeleteUser(user);
        }
    }
}
