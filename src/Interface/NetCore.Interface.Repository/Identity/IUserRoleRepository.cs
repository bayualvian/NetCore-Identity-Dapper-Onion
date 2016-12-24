using NetCore.Core.Entities.Identity.Role;
using NetCore.Core.Entities.Identity.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.Interface.Repository.Identity
{
    public interface IUserRoleRepository
    {
        Task InsertAsync(UserRole userRole);
        Task DeleteAsync(UserRole userRole);
        Task<bool> IsInRoleAsync(User user, string roleName);
        Task<IList<User>> GetUsersInRoleAsync(string roleName);
    }
}
