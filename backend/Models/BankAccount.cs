using System.ComponentModel.DataAnnotations;
namespace BankingLedger.Models
{
    public class BankAccount
    {   [Key]    
        public int accountNumber { get; set; }
        [Required]
        public int userId {get; set;}

        [Required]
        public decimal balance { get; set; }
        
    }
}