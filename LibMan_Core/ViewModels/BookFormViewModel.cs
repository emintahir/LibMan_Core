using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibMan_Core.Models;

namespace LibMan_Core.ViewModels
{
    public class BookFormViewModel
    {
        public Book Book { get; set; }
        public IEnumerable<Category> Categories { get; set; }

        public string DeleteImage { get; set; }
        public string BookFormPageTitle
        {
            get
            {
                if (Book != null && Book.BookId != 0)
                {
                    return "Edit Book";
                }
                else
                {
                    return "New Book";
                }
            }
        }
    }
}
