using NetCore.Core.Entities.Identity.Role;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCore.Interface.Repository.Identity
{
    public interface IRoleRepository
    {
        Task CreateAsync(Role role);
        Task DeleteAsync(Role role);
        Task<Role> FindByIdAsync(long roleId);
        Task<Role> FindByNameAsync(string roleName);
        Task<Role> FindByName(string roleName);
        Task UpdateAsync(Role role);
        Task<IList<string>> SelectRolesByUserIdAsync(long userId);
    }
}
