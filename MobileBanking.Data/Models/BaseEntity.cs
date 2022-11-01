using System;
using System.Collections.Generic;
using System.Text;

namespace MobileBanking.Data.Models
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            CreatedOnUTC = DateTime.UtcNow;
        }
        public DateTime CreatedOnUTC { get; set; }
        public DateTime UpdatedOnUTC { get; set; }
    }
}
