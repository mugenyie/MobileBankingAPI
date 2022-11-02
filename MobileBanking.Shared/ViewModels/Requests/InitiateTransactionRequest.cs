using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MobileBanking.Shared.ViewModels.Requests
{
    public class InitiateTransactionRequest
    {
        public int UserId { get; set; }

        [StringLength(1000, ErrorMessage = "Invalid Account number")]
        public string AccountNumber { get; set; }

        public string RecipientPhoneNumber { get; set; }

        [Required(ErrorMessage = "Recipeient full name is required")]
        [StringLength(1000, MinimumLength = 5, ErrorMessage = "Name must be atleast 5 characters")]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "Recipient name can only contain Alphabet")]
        public string RecipientName { get; set; }

        [Required]
        [Range(5000, 5000000, ErrorMessage = "Enter amount between UGX. 5,000 and 5,000,000")]
        public decimal Amount { get; set; }

        public string Description { get; set; }
    }
}
