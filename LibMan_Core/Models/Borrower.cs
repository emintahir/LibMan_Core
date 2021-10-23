using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibMan_Core.Models
{
    public class Borrower
    {
        [Key]
        public int Id { get; set; }

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
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }


        [Required]
        [StringLength(50)]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }

        public ICollection<Lend> Lends { get; set; }

        [Display(Name = "Name & Surname")]
        public string FullName => Name + " " + Surname;
    }
}
