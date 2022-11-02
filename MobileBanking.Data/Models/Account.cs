using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MobileBanking.Data.Models
{
    public class Account : BaseEntity
    {
        [Key]
        public int AccountId { get; set; }
        [StringLength(10)]
        public string AccountNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [Column(TypeName = "decimal(15,3)")]
        public decimal OpeningBalance { get; set; }

        [Column(TypeName = "decimal(15,3)")]
        public decimal NewBalance { get; set; }

        public bool IsActive { get; set; }
    }
}
