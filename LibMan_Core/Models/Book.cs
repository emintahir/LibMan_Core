﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibMan_Core.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

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
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime DateAddedToLibrary { get; set; }

        public string BookLocAtLibrary { get; set; }

        [Display(Name = "Image Name")]
        public string ImageName { get; set; }

        [NotMapped]
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }

        [Display(Name = "Number of Pages")]
        public int? Pages { get; set; }

        [Display(Name = "Total Copies:")]
        public int CopiesOwned { get; set; }

        [Display(Name = "Available Copies:")]
        public int CopiesAvailable { get; set; }

        [Display(Name = "Category")]
        public int? CategoryId { get; set; }

        public Category Category { get; set; }

        public ICollection<BookLend> BookLends { get; set; }
    }
}
