using System;

namespace NetCore.Core.Entities
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            CreatedDate = DateTime.Now;
        }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        
        public DateTime? DeletedAt { get; set; }
    }
}
