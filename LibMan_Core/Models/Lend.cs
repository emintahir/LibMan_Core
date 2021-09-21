using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibMan_Core.Models
{
    public class Lend
    {
        public int Id { get; set; }
        public int BorrowerId { get; set; }

        public Borrower Borrower { get; set; }

        public int BookId { get; set; }

        public Book Book { get; set; }

        public DateTime DateLent { get; set; }
        public DateTime ReturnDate { get; set; }
        

        


    }
}
