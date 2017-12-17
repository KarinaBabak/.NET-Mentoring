using System;
using System.ComponentModel.DataAnnotations;

namespace Task2_EF.Entities
{
    public class CreditCard
    {
        public int Id { get; set; }

        [StringLength(20)]
        public string CardNumber { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }

        [Required]
        [StringLength(100)]
        public string CardHolderName { get; set; }

        public int EmployeeID { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
