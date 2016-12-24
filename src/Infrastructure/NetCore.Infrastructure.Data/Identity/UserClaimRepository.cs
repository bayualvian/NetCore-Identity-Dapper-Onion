using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using NetCore.Interface.Repository.Identity;
using NetCore.Core.Entities.Identity.Claim;
using System.Data;
using NetCore.Core.Entities.Identity.User;

namespace NetCore.Infrastructure.Data.Identity
{
    public class UserClaimRepository : BaseDAC, IUserClaimRepository
    {
        public async Task DeleteAsync(UserClaim userClaim)
        {
            using (IDbConnection db = base.GetConnection())
            {
                await db.ExecuteAsync(@"DELETE FROM [dbo].[UserClaims] WHERE [UserId] = @UserId AND [ClaimType] = @ClaimType AND [ClaimValue] = @ClaimValue", userClaim);
            }
        }

        public async Task InsertAsync(UserClaim userClaim)
        {
            using (IDbConnection db = base.GetConnection())
            {
                await db.ExecuteAsync(@"INSERT INTO [dbo].[UserClaims]([UserId],[ClaimType],[ClaimValue])
                VALUES(@UserId,@ClaimType,@ClaimValue)", userClaim);
            }
        }

        public async Task<IList<UserClaim>> SelectClaimsByUserId(long userId)
        {
            using (IDbConnection db = base.GetConnection())
            {
                var userClaim = await db.QueryAsync<UserClaim>(@"SELECT [ClaimType], [ClaimValue] FROM [dbo].[UserClaims] WHERE [UserId] = @Id",
                            new { Id = userId });
                return userClaim.AsList();
            }
        }

        public async Task<IList<User>> SelectUsersByUserClaim(UserClaim claim)
        {
            using (IDbConnection db = base.GetConnection())
            {
                var user = await db.QueryAsync<User>(@"SELECT * 
                              FROM [dbo].[Users] 
                             WHERE [Id] = (SELECT [UserId] 
                                    FROM [dbo].[UserClaims] 
                                WHERE [ClaimType] = @ClaimType 
                                    AND [ClaimValue] = @ClaimValue)",
                                  new { claim });
                return user.AsList();
            }
        }
    }
}
