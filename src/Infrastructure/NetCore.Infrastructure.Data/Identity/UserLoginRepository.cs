using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using NetCore.Interface.Repository.Identity;
using NetCore.Core.Entities.Identity.User;
using System.Data;

namespace NetCore.Infrastructure.Data.Identity
{
    public class UserLoginRepository : BaseDAC, IUserLoginRepository
    {
        public async Task DeleteAsync(UserLogin userLogin)
        {
            using (IDbConnection db = base.GetConnection())
            {
                await db.ExecuteAsync(@"DELETE FROM [dbo].[UserLogins] WHERE [LoginProvider] = @LoginProvider AND [ProviderKey] = @ProviderKey AND [UserId] = @UserId", userLogin);
            }
        }

        public async Task InsertAsync(UserLogin userLogin)
        {
            using (IDbConnection db = base.GetConnection())
            {
                await db.ExecuteAsync(@"INSERT INTO [dbo].[UserLogins]([LoginProvider],[ProviderKey],[UserId])
                VALUES(@LoginProvider,@ProviderKey,@UserId)", userLogin);
            }
        }

        public async Task<IList<UserLogin>> LoginInfoByUserId(User user)
        {
            using (IDbConnection db = base.GetConnection())
            {
                var userLogin = await db.QueryAsync<UserLogin>("SELECT [LoginProvider], [ProviderKey] FROM [dbo].[UserLogins] WHERE [UserId] = @Id",
                               new { user.Id });
                return userLogin.ToList();
            }
        }

        public async Task<User> UserByLoginInfoAsync(UserLogin userLogin)
        {
            using (IDbConnection db = base.GetConnection())
            {
                var user = await db.QueryAsync<User>(@"SELECT * 
                            FROM [dbo].[Users] 
                            WHERE [Id] = (SELECT [UserId] 
                                FROM [dbo].[UserLogins] 
                            WHERE [LoginProvider] = @LoginProvider 
                                AND [ProviderKey] = @ProviderKey)",
                                    new { userLogin });
                return user.FirstOrDefault();
            }
        }
    }
}
