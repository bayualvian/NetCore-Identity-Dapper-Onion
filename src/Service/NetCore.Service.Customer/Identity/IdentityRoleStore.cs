using Microsoft.AspNetCore.Identity;
using NetCore.Core.Entities.Identity.Role;
using NetCore.Interface.Repository.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NetCore.Service.Customer.Identity
{
    public class IdentityRoleStore : IRoleStore<Role>
    {

        private IRoleRepository _roleRepository;
        public IdentityRoleStore(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }
            await _roleRepository.CreateAsync(role);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken)
        {
            var match = _roleRepository.FindByIdAsync(role.Id).Result;
            if (match != null)
            {
                match.Name = role.Name;
                await _roleRepository.UpdateAsync(match);
                return IdentityResult.Success;
            }
            else
            {
                return IdentityResult.Failed();
            }
        }

        public async Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken)
        {
            var match = _roleRepository.FindByIdAsync(role.Id).Result;
            if (match != null)
            {
                await _roleRepository.DeleteAsync(match);

                return IdentityResult.Success;
            }
            else
            {
                return IdentityResult.Failed();
            }
        }

        public async Task<Role> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            var roleIdInt = Convert.ToInt32(roleId);
            var role = await _roleRepository.FindByIdAsync(roleIdInt);

            return role;
        }

        public async Task<Role> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.FindByNameAsync(normalizedRoleName);

            return role;
        }

        public Task<string> GetRoleIdAsync(Role role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Id.ToString());
        }

        public Task<string> GetRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Name);
        }

        public Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Name);
        }

        public Task SetRoleNameAsync(Role role, string roleName, CancellationToken cancellationToken)
        {
            role.Name = roleName;

            return Task.FromResult(true);
        }

        public Task SetNormalizedRoleNameAsync(Role role, string normalizedName, CancellationToken cancellationToken)
        {
            role.Name = normalizedName;

            return Task.FromResult(true);
        }

        public void Dispose() { }
    }
}
