namespace NetCore.Core.Entities.Identity.Claim
{
    public class UserClaim : BaseEntity
    {
        /// <summary>
        /// User Id for the user who owns this login
        /// 
        /// </summary>
        public virtual long UserId { get; set; }

        /// <summary>
        /// Claim type
        /// 
        /// </summary>
        public virtual string ClaimType { get; set; }

        /// <summary>
        /// Claim value
        /// 
        /// </summary>
        public virtual string ClaimValue { get; set; }
    }
}
