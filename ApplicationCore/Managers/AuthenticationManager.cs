using ApplicationCore.DTOs;
using ApplicationCore.Infrastructure;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using AutoMapper;
using Infrastructure.EF;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Managers
{
    public class AuthenticationManager : IAuthenticationManager
    {
        public UserManager<AppUser> UserManager { get; private set; }
        public SignInManager<AppUser> SignInManager { get; private set; }
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AuthenticationManager(ApplicationDbContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            _context = context;
            _mapper = mapper;
        }

        public async Task<OperationDetails> Register(UserDTO userDTO)
        {

            AppUser user = await UserManager.FindByEmailAsync(userDTO.Email);

            if (user == null)

            {
                var userIdentity = _mapper.Map<UserDTO, AppUser>(userDTO);
               var result = await UserManager.CreateAsync(userIdentity, userDTO.Password);

                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault().ToString(), "");

                await UserManager.AddToRoleAsync(userIdentity, "User");
                await _context.SaveChangesAsync();

                  
                return new OperationDetails(true, "Congratulations! Your account has been created.", "");
            }
            else
            {
                return new OperationDetails(false, "User with this login already exists", "Email");
            }
        }

        public async Task<OperationDetails> Login(UserDTO userDTO)
        {
            var identity = await GetClaimsIdentity(userDTO.UserName, userDTO.Password);
            if (identity == null)
            {
                return new OperationDetails(false, "Invalid username or password.", "");
            }

            var auth = await SignInManager.PasswordSignInAsync(userDTO.UserName, userDTO.Password, userDTO.RememberMe, lockoutOnFailure: false);

            return new OperationDetails(auth.Succeeded , " " , " ");
        }

        public async Task<ConfirmDTO> GetEmailConfirmationToken(string userName)
        {
         var user = await UserManager.FindByNameAsync(userName);
            if (user == null || (await UserManager.IsEmailConfirmedAsync(user)))
                return (null);
                    
              var code =  await UserManager.GenerateEmailConfirmationTokenAsync(user);
            return new ConfirmDTO { Code = code,UserId = user.Id };
        }

        public async Task<ConfirmDTO> GetPasswordConfirmationToken(string userName)
        {
            var user = await UserManager.FindByNameAsync(userName);
            if (user == null || !(await UserManager.IsEmailConfirmedAsync(user)))
                return (null);

            var code = await UserManager.GenerateEmailConfirmationTokenAsync(user);
            return new ConfirmDTO { Code = code, UserId = user.Id };
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
          if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
             return await Task.FromResult<ClaimsIdentity>(null);
          
          var userToVerify = await UserManager.FindByNameAsync(userName);
            if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);
           
          if(await UserManager.CheckPasswordAsync(userToVerify,password))
             return await Task.FromResult(new ClaimsIdentity());

             return await Task.FromResult<ClaimsIdentity>(null);
        }
        public async Task Logout()
        {
            await SignInManager.SignOutAsync();
        }


    }
}
