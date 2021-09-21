using LibMan_Core.Data;
using LibMan_Core.Models;
using LibMan_Core.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Threading.Tasks;

namespace LibMan_Core.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BooksController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
        }


        // GET: Book
        public async Task<IActionResult> Index()
        {
            var books = await _db.Books.Include(b => b.Category).ToListAsync();
            return View("Index", books);
            //if (User.IsInRole(RoleName.CanManage))
            //{
            //    return View("Index", books);
            //}

            //return View("IndexRO", books);
        }

        public async Task<IActionResult> Details(int id)
        {
            Book book = await _db.Books.Include(b => b.Category).SingleOrDefaultAsync(b => b.BookId == id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        //[Authorize(Roles = RoleName.CanManage)]
        public async Task<IActionResult> New()
        {
            var bookFormViewModel = new BookFormViewModel
            {
                Book = new Book(),
                Categories = await _db.Categories.ToListAsync()
            };
            return View("BookForm", bookFormViewModel);
        }


        //[Authorize(Roles = RoleName.CanManage)]
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _db.Books.Include(b => b.Category).SingleOrDefaultAsync(b => b.BookId == id);
            if (book == null)
            {
                return NotFound();
            }
            var bookFormViewModel = new BookFormViewModel
            {
                Book = book,
                Categories = await _db.Categories.ToListAsync()
            };
            return View("BookForm", bookFormViewModel);
        }

        //[Authorize(Roles = RoleName.CanManage)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Save(BookFormViewModel bookFormViewModel)
        {
            if (!ModelState.IsValid)
            {
                bookFormViewModel = new BookFormViewModel
                {
                    Book = new Book(),
                    Categories = await _db.Categories.ToListAsync()
                };
                return View("BookForm", bookFormViewModel);
            }

            if (bookFormViewModel.Book.BookId == 0) //New Book
            {
                bookFormViewModel.Book.DateAddedToLibrary = DateTime.Today;
                bookFormViewModel.Book.ImageName = UploadImage(bookFormViewModel.Book);
                _db.Books.Add(bookFormViewModel.Book);
            }
            else //book.Id != 0 Edit existing book
            {
                var bookInDb = await _db.Books.FindAsync(bookFormViewModel.Book.BookId);
                if (bookFormViewModel.Book.ImageFile == null) //No Image chosen for upload
                {
                    if (bookFormViewModel.DeleteImage == "1") //Existing Image selected to delete
                    {
                        DeleteImage(bookInDb);
                        bookInDb.ImageName = null;
                    }
                    else
                    {
                        bookFormViewModel.Book.ImageName = bookInDb.ImageName;
                    }
                }
                else //Image selected for upload
                {
                    if (bookInDb.ImageName!=null) //If had image prev
                    {
                        DeleteImage(bookInDb);
                    }
                    bookInDb.ImageName = UploadImage(bookFormViewModel.Book);
                };
                _db.Entry(bookInDb).State = EntityState.Detached;
                _db.Update(bookInDb);
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index", "Books");
        }


        //[Authorize(Roles = RoleName.CanManage)]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var bookInDb = await _db.Books.FindAsync(id);
            if (bookInDb == null)
            {
                NotFound();
            }
            else
            {
                if (bookInDb.ImageName != null) //If had image prev
                {
                    DeleteImage(bookInDb);
                }
                _db.Books.Remove(bookInDb);    
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index", "Books");
        }

        private string UploadImage(Book book)
        {
            string newFileName = null;
            if (book.ImageFile != null)
            {
                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "image");
                string fileExtension = Path.GetExtension(book.ImageFile.FileName);
                newFileName = book.BookId + "_" + book.Title + "_" + DateTime.Now.ToString("ddMMyyyy") + fileExtension;
                string newFilePath = Path.Combine(uploadFolder, newFileName);
                using var fileStream = new FileStream(newFilePath, FileMode.Create);
                book.ImageFile.CopyTo(fileStream);
            }

            return newFileName;
        }


        private void DeleteImage(Book book)
        {
            if (book.ImageName != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "image");
                string filePath = Path.Combine(uploadsFolder, book.ImageName);
                System.IO.File.Delete(filePath);
            }
        }



    }
}
