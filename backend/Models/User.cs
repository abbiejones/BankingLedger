using System.ComponentModel.DataAnnotations;

namespace BankingLedger.Models
{
    public class User
    {
        [Required]
        public int userId {get; set;}
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        [Required]
        public string userName {get; set;}
        [Required]
        public byte[] passwordSalt {get; set;}
        [Required]
        public string passwordHash{get; set;}
    }
}