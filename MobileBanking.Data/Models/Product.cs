using System;
using System.Collections.Generic;
using System.Text;

namespace MobileBanking.Data.Models
{
    public class Product : BaseEntity
    {
        public int Id { get; set; }
        public string ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int ServiceProviderId { get; set; }

        public virtual ServiceProvider ServiceProvider { get; set; }
    }
}
