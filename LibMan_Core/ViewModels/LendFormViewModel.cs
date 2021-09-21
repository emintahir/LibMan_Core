using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibMan_Core.Models;

namespace LibMan_Core.ViewModels
{
    public class LendFormViewModel
    {
        public Lend Lend { get; set; }
        public Borrower Borrower { get; set; }
        public IEnumerable<Book> Books { get; set; }
    }
}
