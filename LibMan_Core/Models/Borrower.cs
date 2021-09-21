using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibMan_Core.Models
{
    public class Borrower
    {
        [Key]
        public int BorrowerId { get; set; }

        [Display(Name = "National ID")]
        public string NationalId { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Surname")]
        public string Surname { get; set; }


        [Display(Name = "Date of Birth")]
        public DateTime BirthDate { get; set; }


        [Required]
        [StringLength(50)]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }

        public virtual ICollection<Lend> Lends { get; set; }
    }
}
