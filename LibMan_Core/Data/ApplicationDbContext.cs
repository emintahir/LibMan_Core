using LibMan_Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibMan_Core.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Borrower> Borrowers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Lend> Lends { get; set; }
        public DbSet<BookLend> BookLends { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<BookLend>()
                .HasKey(bl => new { bl.LendId, bl.BookId});
            builder.Entity<BookLend>()
                .HasOne(bl => bl.Lend)
                .WithMany(l => l.BookLends)
                .HasForeignKey(bl => bl.LendId);
            builder.Entity<BookLend>()
                .HasOne(bc => bc.Book)
                .WithMany(b=>b.BookLends)
                .HasForeignKey(bl => bl.BookId);
        }
    }
}
