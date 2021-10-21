using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibMan_Core.Models
{
    public class BookLend
    {
        public int LendId { get; set; }
        public Lend Lend { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
