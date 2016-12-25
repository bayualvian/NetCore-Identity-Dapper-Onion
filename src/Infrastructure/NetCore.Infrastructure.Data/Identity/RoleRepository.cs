using System.Collections.Generic;
using Dapper;
using System.Threading.Tasks;
using System.Linq;
using NetCore.Interface.Repository.Identity;
using NetCore.Core.Entities.Identity.Role;
using Microsoft.Extensions.Options;
using NetCore.Core.Entities;
using System.Data;
using NetCore.Core.Entities.ConfigManager;

namespace NetCore.Infrastructure.Data.Identity
{
    public class RoleRepository : BaseDAC, IRoleRepository
    {
        public RoleRepository(IOptions<ConfigEntity> option) : base(option)
        {
        }

        public async Task CreateAsync(Role role)
        {
            using (IDbConnection db = base.GetConnection())
            {
                await db.ExecuteAsync(@"INSERT INTO [dbo].[Roles]([Id],[Name]) VALUES(@Id,@Name)", role);
            }
        }

        public async Task DeleteAsync(Role role)
        {
            using (IDbConnection db = base.GetConnection())
            {
                await db.ExecuteAsync(@"DELETE FROM [dbo].[Roles] WHERE [Id] = @Id",
                        new { role.Id });
            }
        }

        public async Task<Role> FindByIdAsync(long roleId)
        {
            using (IDbConnection db = base.GetConnection())
            {
                var role = await db.QueryAsync<Role>("SELECT * FROM [dbo].[Roles] WHERE [Id] = @roleId",
                        new { roleId });
                return role.FirstOrDefault();
            }
        }

        public async Task<Role> FindByName(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                using (IDbConnection db = base.GetConnection())
                {
                    var role = await db.QueryAsync<Role>("SELECT * FROM [dbo].[Roles] WHERE  UPPER([Name]) = @roleName",
                            new { roleName });
                    return role.FirstOrDefault();
                }
            }
            else
            {
                return null;
            }
        }

        public async Task<Role> FindByNameAsync(string roleName)
        {
            return await FindByName(roleName);
        }

        public async Task<IList<string>> SelectRolesByUserIdAsync(long userId)
        {
            using (IDbConnection db = base.GetConnection())
            {
                var roles = await db.QueryAsync<string>(@"SELECT [Name]
                            FROM[dbo].[Roles]
                            WHERE[Id] IN(SELECT[RoleId]
                        FROM[dbo].[UserRoles]
                            WHERE[UserId] = @Id)",
                            new { Id = userId });
                return roles.AsList();
            }
        }

        public async Task UpdateAsync(Role role)
        {
            using (IDbConnection db = base.GetConnection())
            {
                await db.ExecuteAsync(@"UPDATE [dbo].[Roles] SET [Name] = @Name WHERE [Id] = @Id", role);
            }
        }
    }
}
