using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibMan_Core.ViewModels
{
    public class AssignedBookData
    {
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public bool IsAssigned { get; set; }
    }
}
