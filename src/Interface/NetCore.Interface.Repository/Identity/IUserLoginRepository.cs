using NetCore.Core.Entities.Identity.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCore.Interface.Repository.Identity
{
    public interface IUserLoginRepository
    {
        Task InsertAsync(UserLogin userLogin);
        Task<User> UserByLoginInfoAsync(UserLogin userLogin);
        Task<IList<UserLogin>> LoginInfoByUserId(User user);
        Task DeleteAsync(UserLogin userLogin);
    }
}
