﻿using LibMan_Core.Data;
using LibMan_Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Index()
        {
            var borrowers = await _db.Borrowers.ToListAsync();
            return View("Index", borrowers);
        }

        public async Task<IActionResult> Details(int id)
        {
            var borrower = await _db.Borrowers.SingleOrDefaultAsync(b => b.Id == id);
            if (borrower == null)
            {
                return NotFound();
            }
            return View(borrower);
        }

        public IActionResult New()
        {
            var borrower = new Borrower {
                BirthDate = Convert.ToDateTime("1900-01-01")
            };
            return View("BorrowerForm", borrower);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var borrower = await _db.Borrowers.SingleOrDefaultAsync(b => b.Id == id);
            if (borrower == null)
            {
                return NotFound();
            }
            return View("BorrowerForm", borrower);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(Borrower borrower)
        {
            if (!ModelState.IsValid)
            {
                var newBorrower = new Borrower();
                return View("BorrowerForm", newBorrower);
            }

            if (borrower.Id == 0)
            {
                _db.Borrowers.Add(borrower);
            }
            else //borrower.Id != 0
            {
                _db.Borrowers.Update(borrower);
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index", "Borrowers");
        }

        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var borrowerInDb = await _db.Borrowers.SingleAsync(b => b.Id == id);
            if (borrowerInDb == null)
            {
                NotFound();
            }
            else
            {
                _db.Borrowers.Remove(borrowerInDb);
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index", "Borrowers");
        }
    }
}

