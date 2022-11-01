using MobileBanking.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MobileBanking.Data.Models
{
    public class TransactionLog : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string AccountId { get; set; }
        [Column(TypeName = "decimal(15,3)")]
        public decimal Amount { get; set; }
        [Column(TypeName = "decimal(15,3)")]
        public decimal NewBalance { get; set; }
        public string Description { get; set; }
        public string RecipientId { get; set; }
        public int ProductId { get; set; }
        public int ServiceProviderId { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public string PaymentReference { get; set; }
        public string PaymentStatusMetaData { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string OrderReference { get; set; }
        public string OrderStatusMetaData { get; set; }
        public TransactionStatus TransactionStatus { get; set; }
        public string TransactionStatusMessage { get; set; }
        public string TransactionMetaData { get; set; }
    }
}
