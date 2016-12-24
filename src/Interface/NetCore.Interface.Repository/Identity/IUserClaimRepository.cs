using NetCore.Core.Entities.Identity.Claim;
using NetCore.Core.Entities.Identity.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCore.Interface.Repository.Identity
{
    public interface IUserClaimRepository
    {
        Task InsertAsync(UserClaim userClaim);
        Task<IList<UserClaim>> SelectClaimsByUserId(long userId);
        Task DeleteAsync(UserClaim userClaim);
        Task<IList<User>> SelectUsersByUserClaim(UserClaim claim);
    }
}
