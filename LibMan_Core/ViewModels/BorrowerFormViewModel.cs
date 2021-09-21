using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibMan_Core.Models;

namespace LibMan_Core.ViewModels
{
    public class BorrowerFormViewModel
    {
        public Borrower Borrower { get; set; }

        public string BorrowerFormPageTitle
        {
            get
            {
                if (Borrower != null && Borrower.BorrowerId != 0)
                {
                    return "Edit Borrower";
                }
                else
                {
                    return "New Borrower";
                }
            }
        }
    }
}
