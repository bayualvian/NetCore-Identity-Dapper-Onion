using System;

namespace NetCore.Core.Entities.Identity.Role
{
    public class UserRole : BaseEntity
    {
        public virtual long UserId { get; set; }
        public virtual long RoleId { get; set; }
    }
}
