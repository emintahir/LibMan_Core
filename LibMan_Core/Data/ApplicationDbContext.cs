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
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Lend>()
                .HasOne(bk => bk.Book)
                .WithMany(l => l.Lends)
                .HasForeignKey(bki => bki.BookId);
            builder.Entity<Lend>()
               .HasOne(br => br.Borrower)
               .WithMany(l => l.Lends)
               .HasForeignKey(bri => bri.BorrowerId);
        }
    }
}
