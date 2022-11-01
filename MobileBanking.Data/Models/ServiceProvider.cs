using System;
using System.Collections.Generic;
using System.Text;

namespace MobileBanking.Data.Models
{
    public class ServiceProvider : BaseEntity
    {
        public int ServiceProviderId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
