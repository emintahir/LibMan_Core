using LibMan_Core.Data;
using LibMan_Core.Models;
using LibMan_Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibMan_Core.Controllers
{
    public class LendsController : Controller
    {

        private readonly ApplicationDbContext _db;

        public LendsController(ApplicationDbContext db)
        {
            _db = db;
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
        }

        //GET: Lends
        public async Task<IActionResult> Index()
        {
            var lends = await _db.Lends
                .Include(l => l.Borrower)
                .Include(l => l.BookLends)
                    .ThenInclude(bl => bl.Book)
                        .ToListAsync();
            return View("Index", lends);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lend = await _db.Lends.FirstOrDefaultAsync(l => l.Id == id);
            if (lend == null)
            {
                return NotFound();
            }

            return View(lend);
        }

        public IActionResult New()
        {
            var lend = new Lend
            {
                LendDate = DateTime.Today,
                DueDate = DateTime.Today.AddDays(10),
                BookLends = new List<BookLend>(),
                Borrower = new Borrower()
            };
            BorrowersSelectList();
            YesNoSelectList();
            PopulateAssignedBooks(lend);
            return View("LendForm", lend);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var lend = await _db.Lends
                .Include(l => l.Borrower)
                .Include(l => l.BookLends)
                    .ThenInclude(bl => bl.Book)
                .SingleOrDefaultAsync(l => l.Id == id);
            if (lend == null)
            {
                return NotFound();
            }
            BorrowersSelectList();
            YesNoSelectList();
            PopulateAssignedBooks(lend);
            return View("LendForm", lend);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Lend lend = await _db.Lends
                .Include(l => l.BookLends)
                    .ThenInclude(bl => bl.Book)
                .Include(l => l.Borrower)
                .SingleAsync(l => l.Id == id);
            if (lend == null)
            {
                return NotFound();
            }
            _db.Lends.Remove(lend);
            foreach (var book in lend.BookLends)
            {
                book.Book.CopiesAvailable++;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index", "Lends");
        }

        //[ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Save(int id, string[] selectedBooks, Lend lend)
        {
            if (id == 0)
            {
                if (selectedBooks != null)
                {
                    lend.Borrower = await _db.Borrowers.SingleOrDefaultAsync(b => b.Id == lend.BorrowerId);
                    lend.BookLends = new List<BookLend>();
                    foreach (var book in selectedBooks)
                    {
                        var bookLendToAdd = new BookLend
                        {
                            BookId = int.Parse(book),
                            LendId = lend.Id,
                        };
                        lend.BookLends.Add(bookLendToAdd);
                        var bookInDb =  await _db.Books.SingleOrDefaultAsync(b => b.Id == bookLendToAdd.BookId);
                        bookInDb.CopiesAvailable--;
                    }
                    
                }
                if (ModelState.IsValid)
                {
                    _db.Add(lend);
                    await _db.SaveChangesAsync();
                    return RedirectToAction("Index", "Lends");
                }
                PopulateAssignedBooks(lend);
                return View("LendForm", lend);
                //return Ok();
            }
            else //id!=0
            {
                var lendInDb = await _db.Lends
                    .Include(l => l.Borrower)
                    .Include(l => l.BookLends)
                        .ThenInclude(bl => bl.Book)
                    .FirstOrDefaultAsync(l => l.Id == id);
                if (ModelState.IsValid)
                {
                    var isUpdated = await TryUpdateModelAsync(lendInDb, "", l => l.LendDate, l => l.DueDate, l => l.IsReturned, l => l.ReturnDate);
                    UpdateAssignedBooks(selectedBooks, lendInDb);
                    await _db.SaveChangesAsync();
                    return RedirectToAction("Index", "Lends");
                }
                PopulateAssignedBooks(lendInDb);
                return View("LendForm", lend);
            }
        }

        private void PopulateAssignedBooks(Lend lend)
        {
            var allBooks = _db.Books;
            var lendBooks = new HashSet<int>(lend.BookLends.Select(bl => bl.BookId));
            var allBooksWAssInfo = new List<AssignedBookData>();
            foreach (var book in allBooks)
            {
                allBooksWAssInfo.Add(new AssignedBookData
                {
                    IsAssigned = lendBooks.Contains(book.Id),
                    BookId = book.Id,
                    BookTitle = book.Title,
                    CopiesOwned = book.CopiesOwned,
                    CopiesAvailable = book.CopiesAvailable
                });
            };
            ViewData["Books"] = allBooksWAssInfo;
        }

        private void UpdateAssignedBooks(string[] selectedBooks, Lend lend)
        {
            if (selectedBooks == null)
            {
                lend.BookLends = new List<BookLend>();
                return;
            }
            var selectedBooksHashSet = new HashSet<string>(selectedBooks);
            var booksInLendHashSet = new HashSet<int>(lend.BookLends.Select(bl => bl.Book.Id));
            foreach (var book in _db.Books)
            {
                if (selectedBooksHashSet.Contains(book.Id.ToString()))
                {
                    if (!booksInLendHashSet.Contains(book.Id))
                    {
                        lend.BookLends.Add(new BookLend
                        {
                            LendId = lend.Id,
                            BookId = book.Id
                        });
                        book.CopiesAvailable--;
                    }
                }
                else
                {
                    if (booksInLendHashSet.Contains(book.Id))
                    {
                        BookLend bookToRemove = lend.BookLends.FirstOrDefault(bl => bl.BookId == book.Id);
                        _db.Remove(bookToRemove);
                        book.CopiesAvailable++;
                    }
                }
            }
        }

        public void BorrowersSelectList()
        {
            ViewData["Borrowers"] = new SelectList(_db.Borrowers.ToList(), "Id", "FullName");
        }

        public void YesNoSelectList()
        {
            ViewData["YesNoSelectList"] = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Value = "false", Text = "No" },
                new SelectListItem { Value = "true" , Text = "Yes" }
            },
                "Value", "Text");
        }
    }
}
