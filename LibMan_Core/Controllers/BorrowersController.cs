using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibMan_Core.Data;
using LibMan_Core.Models;
using LibMan_Core.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace LibMan_Core.Controllers
{
    public class BorrowersController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BorrowersController(ApplicationDbContext db)
        {
            _db = db;
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
        }


        // GET: Borrower
        public IActionResult Index()
        {
            var borrowers = _db.Borrowers.ToList();
            return View("Index", borrowers);
            //if (User.IsInRole(RoleName.CanManage))
            //{
            //    return View("Index", borrowers);
            //}
            //return View("IndexRO", borrowers);
        }

        public IActionResult Details(int id)
        {
            var borrower = _db.Borrowers.SingleOrDefault(b => b.BorrowerId == id);
            if (borrower == null)
            {
                return NotFound();
            }

            return View(borrower);
        }

        //[Authorize(Roles = RoleName.CanManage)]
        public IActionResult New()
        {
            var borrowerFormViewModel = new BorrowerFormViewModel
            {
                Borrower = new Borrower()
            };
            return View("BorrowerForm", borrowerFormViewModel);
        }

        //[Authorize(Roles = RoleName.CanManage)]
        public IActionResult Edit(int id)
        {
            var borrower = _db.Borrowers.SingleOrDefault(b => b.BorrowerId == id);
            if (borrower == null)
            {
                return NotFound();
            }

            var borrowerFormViewModel = new BorrowerFormViewModel
            {
                Borrower = borrower
            };
            return View("BorrowerForm", borrowerFormViewModel);
        }

        //[Authorize(Roles = RoleName.CanManage)]
        [HttpPost]
        public IActionResult Save(Borrower borrower)
        {
            if (!ModelState.IsValid)
            {
                var borrowerFormViewModel = new BorrowerFormViewModel
                {
                    Borrower = new Borrower()
                };
                return View("BorrowerForm", borrowerFormViewModel);
            }

            if (borrower.BorrowerId == 0)
            {
                _db.Borrowers.Add(borrower);
            }
            else //borrower.Id != 0
            {
                var borrowerInDb = _db.Borrowers.Single(b => b.BorrowerId == borrower.BorrowerId);
                borrowerInDb.Name = borrower.Name;
                borrowerInDb.Surname = borrower.Surname;
                borrowerInDb.NationalId = borrower.NationalId;
                borrowerInDb.BirthDate = borrower.BirthDate;
                borrowerInDb.Address = borrower.Address;
                borrowerInDb.Phone = borrower.Phone;
            }
            _db.SaveChanges();
            return RedirectToAction("Index", "Borrowers");
        }

        //[Authorize(Roles = RoleName.CanManage)]
        //[HttpPost]
        public IActionResult Delete(int id)
        {
            var borrowerInDb = _db.Borrowers.Single(b => b.BorrowerId == id);
            if (borrowerInDb == null)
            {
                NotFound();
            }
            else
            {
                _db.Borrowers.Remove(borrowerInDb);
            }
            _db.SaveChanges();
            return RedirectToAction("Index", "Borrowers");
        }
    }
}

