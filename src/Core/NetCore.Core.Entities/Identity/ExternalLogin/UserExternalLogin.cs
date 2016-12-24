namespace NetCore.Core.Entities.Identity.ExternalLogin
{
    public class UserExternalLogin : BaseEntity
    {
        /// <summary>
        /// The login provider for the login (i.e. facebook, google)
        /// 
        /// </summary>
        public virtual string LoginProvider { get; set; }

        /// <summary>
        /// Key representing the login for the provider
        /// 
        /// </summary>
        public virtual string ProviderKey { get; set; }

        /// <summary>
        /// User Id for the user who owns this login
        /// 
        /// </summary>
        public virtual long UserId { get; set; }
    }
}
