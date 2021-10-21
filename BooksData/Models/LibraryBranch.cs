using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using BooksLibrary.Areas.Identity.Data;

namespace BooksData.Models
{
    public class LibraryBranch
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Branch Name")]
        [StringLength(30, ErrorMessage = "Limit branch name is set to 30 characters")]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Telephone { get; set; }
        public string Description { get; set; }
        public DateTime OpenDate { get; set; }
        public virtual IEnumerable<BooksLibraryUser> Patrons {get; set;}
        public virtual IEnumerable<LibraryAsset> LibraryAsset { get; set; }

        public string ImageUrl { get; set; }

    }
}