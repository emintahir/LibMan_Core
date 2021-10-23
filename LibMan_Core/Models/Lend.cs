using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibMan_Core.Models
{
    public class Lend
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int BorrowerId { get; set; }

        public Borrower Borrower { get; set; }

        [Display(Name = "Lend Date:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime LendDate { get; set; }

        [Display(Name = "Due Date:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime DueDate { get; set; }

        [Display(Name = "Borrower Returned The Book?")]
        public bool IsReturned { get; set; }

        [Display(Name = "Return Date:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ReturnDate { get; set; }

        public ICollection<BookLend> BookLends { get; set; }
    }
}
