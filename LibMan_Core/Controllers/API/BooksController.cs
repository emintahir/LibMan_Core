using LibMan_Core.Data;
using LibMan_Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibMan_Core.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public BooksController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        //Get /api/books
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return await _db.Books.ToListAsync();
        }


        //GET /api/books/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _db.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return book;
        }

        //POST /api/books
        [HttpPost]
        public async Task<ActionResult<Book>> CreateBook(Book book)
        {
            if (!ModelState.IsValid)
            {
                BadRequest();
            }

            _db.Books.Add(book);
            await _db.SaveChangesAsync();
            return Ok(book);
        }

        //PUT /api/books/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var bookInDb = await _db.Books.FindAsync(id);
            if (bookInDb == null)
            {
                return NotFound();
            }
            bookInDb.Title = book.Title;
            bookInDb.Author = book.Author;
            bookInDb.Publisher = book.Publisher;
            bookInDb.ImageName = book.ImageName;
            bookInDb.CopiesOwned = book.CopiesOwned;
            bookInDb.YearPublished = book.YearPublished;
            bookInDb.Pages = book.Pages;
            bookInDb.CategoryId = book.CategoryId;
            await _db.SaveChangesAsync();
            return Ok();
        }



        //DELETE /api/books/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var bookInDb = await _db.Books.FindAsync(id);
            if (bookInDb == null)
            {
                return NotFound();
            }
            _db.Books.Remove(bookInDb);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
