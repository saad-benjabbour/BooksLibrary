using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;


namespace BooksData.Models
{
    public class LibraryAsset
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public Status Status { get; set; }

        [Required]
        [Display(Name = "Cost of Replacement")]
        public decimal Cost { get; set; }
        public string ImageUrl { get; set; }
        public int NumberOfCopies { get; set; }

        // relationship with libraryBranch (in DB) 
        // will be set as LocationId
        public virtual LibraryBranch Location { get; set; }
    }

    
}
