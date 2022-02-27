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
        public async Task<List<string>> Create(UserInputModel user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("User is null!");
            }

            User appUser = new User
            {
                UserName = user.Username,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber
            };

            IdentityResult result = await _userManager.CreateAsync(appUser, user.Password);
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

