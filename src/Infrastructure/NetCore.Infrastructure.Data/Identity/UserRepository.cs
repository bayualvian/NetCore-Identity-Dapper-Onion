using System;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Collections.Generic;
using NetCore.Interface.Repository.Identity;
using NetCore.Core.Entities.Identity.User;
using Microsoft.Extensions.Options;
using NetCore.Core.Entities;
using System.Data;
using NetCore.Core.Entities.ConfigManager;

namespace NetCore.Infrastructure.Data.Identity
{
    public class UserRepository : BaseDAC, IUserRepository
    {
        public UserRepository(IOptions<ConfigEntity> option) : base(option)
        {
        }

        public async Task CreateAsync(User user)
        {
            using (IDbConnection db = base.GetConnection())
            {
                var query = await db.QueryAsync<long>(@"INSERT INTO [dbo].[Users]
                                ([Email]
                                ,[EmailConfirmed]
                                ,[PasswordHash]
                                ,[SecurityStamp]
                                ,[PhoneNumber]
                                ,[PhoneNumberConfirmed]
                                ,[TwoFactorEnabled]
                                ,[LockoutEndDateUtc]
                                ,[LockoutEnabled]
                                ,[AccessFailedCount]
                                ,[UserName])
                            VALUES
                                (LOWER(@Email)
                                ,@EmailConfirmed
                                ,@PasswordHash
                                ,@SecurityStamp
                                ,@PhoneNumber
                                ,@PhoneNumberConfirmed
                                ,@TwoFactorEnabled
                                ,@LockoutEndDateUtc
                                ,@LockoutEnabled
                                ,@AccessFailedCount
                                ,LOWER(@UserName));
                                SELECT SCOPE_IDENTITY()", user);
                user.Id = query.Single();
            }
        }

        public async Task DeleteAsync(User user)
        {
            using (IDbConnection db = base.GetConnection())
            {
                await db.ExecuteAsync(@"DELETE FROM [dbo].[Users] WHERE [Id] = @Id", user);
            }
        }

        public async Task<User> SelectByEmailAsync(string email)
        {
            using (IDbConnection db = base.GetConnection())
            {
                var user = await db.QueryAsync<User>("SELECT * FROM [dbo].[Users] WHERE [Email] = @email",
                        new { email = email.ToLower() });
                return user.FirstOrDefault();
            }
        }

        public async Task<User> SelectByIdAsync(long userId)
        {
            using (IDbConnection db = base.GetConnection())
            {
                var user = await db.QueryAsync<User>("SELECT * FROM [dbo].[Users] WHERE [Id] = @userId",
                        new { userId });
                return user.FirstOrDefault();
            }
        }

        public async Task<User> SelectByUserNameAsync(string userName)
        {
            using (IDbConnection db = base.GetConnection())
            {
                var user = await db.QueryAsync<User>("SELECT * FROM [dbo].[Users] WHERE [UserName] = @userName",
                            new { userName });
                return user.FirstOrDefault();
            }
        }
        
        public async Task UpdateAsync(User user)
        {
            using (IDbConnection db = base.GetConnection())
            {
                await db.ExecuteAsync(@"UPDATE [dbo].[Users]
                   SET [Email] = LOWER(@Email)
                      ,[EmailConfirmed] = @EmailConfirmed
                      ,[PasswordHash] = @PasswordHash
                      ,[SecurityStamp] = @SecurityStamp
                      ,[PhoneNumber] = @PhoneNumber
                      ,[PhoneNumberConfirmed] = @PhoneNumberConfirmed
                      ,[TwoFactorEnabled] = @TwoFactorEnabled
                      ,[LockoutEndDateUtc] = @LockoutEndDateUtc
                      ,[LockoutEnabled] = @LockoutEnabled
                      ,[AccessFailedCount] = @AccessFailedCount
                      ,[UserName] = LOWER(@UserName)
                 WHERE [Id] = @Id", user);
            }
        }

        public async Task<List<User>> SelectUsers()
        {
            using (IDbConnection db = base.GetConnection())
            {
                var user = await db.QueryAsync<User>("SELECT * FROM [dbo].[Users]");
                return user.ToList();
            }
        }
    }
}
