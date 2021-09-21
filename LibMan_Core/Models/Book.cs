using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LibMan_Core.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [StringLength(255)]
        public string Author { get; set; }

        [Display(Name = "Publisher")]
        public string Publisher { get; set; }

        [Display(Name = "Date Published")]
        public int? YearPublished { get; set; }

        [Display(Name = "Date Added:")]
        [Required]
        public DateTime DateAddedToLibrary { get; set; }

        public string BookLocAtLibrary { get; set; }

        [Display(Name = "Image Name")]
        public string ImageName { get; set; }

        [NotMapped]
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }

        [Display(Name = "Number of Pages")]
        public int? Page { get; set; }

        [Display(Name = "Number in Stock")]
        public int NumberInStock { get; set; }

        [Display(Name = "Category")]
        public int? CategoryId { get; set; }

        public ICollection<Lend> Lends { get; set; }

        public Category Category { get; set; }
    }
}
