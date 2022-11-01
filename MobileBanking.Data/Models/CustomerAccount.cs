using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MobileBanking.Data.Models
{
    public class CustomerAccount : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int AccountId { get; set; }

        public virtual Account Account { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
