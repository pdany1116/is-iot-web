using IsIoTWeb.Context;
using IsIoTWeb.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IsIoTWeb.Repository
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        private const string CollectionName = "roles";
        private RoleManager<Role> _roleManager;
        public RoleRepository(IMongoDbContext context, RoleManager<Role> roleManager) : base(context, CollectionName)
        {
            _roleManager = roleManager;
        }

        public async Task<List<string>> Create(string name)
        {
            IdentityResult result = await _roleManager.CreateAsync(new Role() { Name = name.ToUpper() });
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

        public override async Task Delete(string id)
        {
            Role role = await _roleManager.FindByIdAsync(id);
            await _roleManager.DeleteAsync(role);
        }
    }
}

