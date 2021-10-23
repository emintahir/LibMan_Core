using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibMan_Core.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
