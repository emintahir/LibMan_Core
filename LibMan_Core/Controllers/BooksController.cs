using LibMan_Core.Data;
using LibMan_Core.Models;
using LibMan_Core.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

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
        }

        public async Task<IActionResult> Details(int id)
        {
            Book book = await _db.Books.Include(b => b.Category).SingleOrDefaultAsync(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        public IActionResult New()
        {
            var book = new Book
            {
                DateAddedToLibrary = DateTime.Today,
                Category = new Category()
            };
            ViewData["Categories"] = new SelectList(_db.Categories, "Id", "Name");
            return View("BookForm", book);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var book = await _db.Books.Include(b => b.Category).SingleOrDefaultAsync(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["Categories"] = new SelectList(_db.Categories, "Id", "Name");
            return View("BookForm", book);
        }

        //[ValidateAntiForgeryToken]
        //[HttpPost]
        public async Task<IActionResult> Save(Book book, string deleteImage)
        {
            if (!ModelState.IsValid)
            {
                var newBook = new Book
                {
                    DateAddedToLibrary = DateTime.Today,
                    Category = new Category()
                };
                return View("BookForm", newBook);
            }
            // New Book
            if (book.Id == 0) 
            {
                // User selected to upload an image
                if (book.ImageFile!=null) 
                {
                    UploadImage(book);
                    book.ImageName = GetNameForImageFile(book);
                }
                // No Image selected for book
                else
                {
                    book.ImageName = null;
                }
                _db.Books.Add(book);
            }
            // Edit existing book (book.Id != 0 )
            else
            {
                //The book has already an image
                if (book.ImageName!=null)
                {
                    //New Image selected to upload
                    if (book.ImageFile!=null)
                    {
                        DeleteImageFile(book.ImageName);
                        UploadImage(book);
                        book.ImageName = GetNameForImageFile(book);
                    }
                    //No Image selected to upload
                    else
                    {
                        //If users selected to delete the image
                        if (deleteImage=="on") 
                        {
                            DeleteImageFile(book.ImageName);
                            book.ImageName = null;
                        }
                        //If users didn't select to delete the image
                        else
                        {
                            book.ImageName = RenameImageFile(book);
                        }
                    }
                }
                //The book does not have any photo
                else 
                {
                    //New Image selected to upload
                    if (book.ImageFile!=null) 
                    {
                        UploadImage(book);
                        book.ImageName = GetNameForImageFile(book);
                    }
                }
                _db.Update(book);
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index", "Books");
        }


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
                    DeleteImageFile(bookInDb.ImageName);
                }
                _db.Books.Remove(bookInDb);    
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index", "Books");
        }

        private async void UploadImage(Book book) //Uploads Image File to wwwwroot/image Folder
        {
            if (book.ImageFile != null)
            {
                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "image");
                string newFileName = GetNameForImageFile(book);
                string newFilePath = Path.Combine(uploadFolder, newFileName);
                using var fileStream = new FileStream(newFilePath, FileMode.Create);
                await book.ImageFile.CopyToAsync(fileStream);
            }
        }

        public string GetNameForImageFile(Book book) //Gets New Image File Name from IFormFile
        {
            string fileExtension = Path.GetExtension(book.ImageFile.FileName);
            string newFileName = book.Id + "_" + book.Title + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + fileExtension;
            return newFileName;
        }

        public string RenameImageFile(Book book) //Rename Existing Image to the new File Name
        {
            string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "image");
            string fileExtension = Path.GetExtension(book.ImageName);
            string newFileName = book.Id + "_" + book.Title + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + fileExtension;
            string oldFilePath = Path.Combine(uploadFolder, book.ImageName);
            string newFilePath = Path.Combine(uploadFolder, newFileName);
            System.IO.File.Copy(oldFilePath, newFilePath);
            DeleteImageFile(book.ImageName);
            return newFileName;
        }

        private void DeleteImageFile(string imageName)
        {
            if (imageName == null) return;
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "image");
            string filePath = Path.Combine(uploadsFolder, imageName);
            System.IO.File.Delete(filePath);
        }
    }
}
