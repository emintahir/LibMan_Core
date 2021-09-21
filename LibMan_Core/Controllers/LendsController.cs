using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibMan_Core.Data;
using LibMan_Core.Models;
using LibMan_Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace LibMan_Core.Controllers
{
    public class LendsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public LendsController(ApplicationDbContext db)
        {
            _db = db;
        }


        //GET: Lends
        public IActionResult Index()
        {
            var lends = _db.Lends.ToList();
            return View(lends);
        }

        public IActionResult SelectBorrower()
        {
            var borrowers = _db.Borrowers.ToList();
            return View("SelectBorrower", borrowers);
        }


        //[Authorize(Roles = RoleName.CanManage)]
        public IActionResult New(int id)
        {

            var lendFormViewModel = new LendFormViewModel
            {
                Borrower = _db.Borrowers.Single(b => b.BorrowerId == id),
                Books = _db.Books.ToList()
            };

            return View("LendForm", lendFormViewModel);
        }


        //public ActionResult Edit(int id)
        //{
        //    var lend = _db.LendBooks.Include(b => b.Book).SingleOrDefault(l => l.Id == id);
        //    if (lend == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    var lendBookFormViewModel = new LendBookFormViewModel
        //    {
        //        Borrowers = _db.Borrowers.ToList(),
        //        Books = _db.Books.ToList()
        //    };

        //    return View("LendBookForm", lendBookFormViewModel);
        //}


        //[Authorize(Roles = RoleName.CanManage)]
        public IActionResult DeleteLend(int id)
        {
            var lendInDb = _db.Lends.Single(l => l.Id == id);
            if (lendInDb == null)
            {
                NotFound();
            }
            else
            {
                _db.Lends.Remove(lendInDb);
            }
            _db.SaveChanges();
            return RedirectToAction("Index", "Lends");
        }


        //[Authorize(Roles = RoleName.CanManage)]
        public IActionResult DeleteBookFromLend(int id)
        {
            var bookInLend = _db.Lends.Single(l => l.Id == id);
            if (bookInLend == null)
            {
                NotFound();
            }
            else
            {
                _db.Lends.Remove(bookInLend);
            }
            _db.SaveChanges();
            return RedirectToAction("Index", "Lends");
        }

        //[Authorize(Roles = RoleName.CanManage)]
        [HttpPost]
        public IActionResult Save(Lend lend)
        {
            if (!ModelState.IsValid)
            {
                var lendFormViewModel = new LendFormViewModel
                {
                    Borrower = new Borrower(),
                    Books = _db.Books.ToList()
                };
                return View("LendForm", lendFormViewModel);
            }

            if (lend.Id == 0)
            {
                lend.DateLent = DateTime.Today;
                _db.Lends.Add(lend);
            }
            else //lend.Id != 0
            {
                var lendInDb = _db.Lends.Single(l => l.Id == lend.Id);
                lendInDb.BorrowerId = lend.BorrowerId;
                //lendInDb.LendDetails = lend.LendDetails;
            }
            _db.SaveChanges();
            return RedirectToAction("Index", "Lends");
        }

    }
}
