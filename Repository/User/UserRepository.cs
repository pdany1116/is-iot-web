using IsIoTWeb.Context;
using IsIoTWeb.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IsIoTWeb.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private const string CollectionName = "users";
        private UserManager<User> _userManager;
        public UserRepository(IMongoDbContext context, UserManager<User> userManager) : base(context, CollectionName)
        {
            _userManager = userManager;
        }

        public async Task<User> GetLoggedUserByUsername(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        public override async Task Delete(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            await _userManager.DeleteAsync(user);
        }

        public override async Task<User> Get(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<List<string>> Create(UserInputModel userInputModel)
        {
            if (userInputModel == null)
            {
                throw new ArgumentNullException("User is null!");
            }

            User appUser = new User
            {
                UserName = userInputModel.Username,
                Email = userInputModel.Email,
                FirstName = userInputModel.FirstName,
                LastName = userInputModel.LastName,
                PhoneNumber = userInputModel.PhoneNumber
            };

            IdentityResult result = await _userManager.CreateAsync(appUser, userInputModel.Password);
            List<string> errors = null;
            if (!result.Succeeded)
            {
                errors = new List<string>();
                foreach (IdentityError error in result.Errors)
                {
                    errors.Add(error.Description);
                }
            }
            return errors;
        }

        public async Task<List<string>> Update(UserInputModel userInputModel)
        {
            if (userInputModel == null)
            {
                throw new ArgumentNullException("User is null!");
            }

            User user = await _userManager.FindByNameAsync(userInputModel.Username);
            user.FirstName = userInputModel.FirstName;
            user.LastName = userInputModel.LastName;
            user.Email = userInputModel.Email;
            user.PhoneNumber = userInputModel.PhoneNumber;
            user.UserName = userInputModel.Username;

            IdentityResult result = await _userManager.UpdateAsync(user);
            List<string> errors = null;
            if (!result.Succeeded)
            {
                errors = new List<string>();
                foreach (IdentityError error in result.Errors)
                {
                    errors.Add(error.Description);
                }
            }
            return errors;
        }
    }
}

