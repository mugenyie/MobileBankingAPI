using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MobileBanking.Data.Models
{
    public class Customer : BaseEntity
    {
        [Key]
        public int CustomerId { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<CustomerAccount> CustomerAccounts { get; set; }
    }
}
