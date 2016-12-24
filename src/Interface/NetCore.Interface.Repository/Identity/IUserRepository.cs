using NetCore.Core.Entities.Identity.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.Interface.Repository.Identity
{
    public interface IUserRepository
    {
        Task UpdateAsync(User user);
        Task<User> SelectByIdAsync(long userId);
        Task<User> SelectByUserNameAsync(string userName);
        Task DeleteAsync(User user);
        Task CreateAsync(User user);
        Task<User> SelectByEmailAsync(string email);
        Task<List<User>> SelectUsers();
    }
}