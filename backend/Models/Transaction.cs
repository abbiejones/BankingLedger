using System.ComponentModel.DataAnnotations;
using System;
namespace BankingLedger.Models
{
    public enum transactionType {
        DEPOSIT,
        WITHDRAWAL,
        CHECKBALANCE,
        CREATEACCOUNT
    }
    public class Transaction
    {
        [Key]
        public int transactionNo { get; set; }
        public DateTime transactionTime{ get; set; }
        [Required]
        public int userId {get; set;}
        [Required]
        public transactionType type {get; set;}
        [Required]
        public decimal amount {get; set;}
        [Required]
        public int accountNumber{get; set;}
    }
}