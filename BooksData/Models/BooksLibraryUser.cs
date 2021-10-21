using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BooksLibrary.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the BooksLibraryUser class
    public class BooksLibraryUser : IdentityUser
    {
        // adding the columns of our user (Patron)
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string? firstName { get; set; }
        [PersonalData]
        public string? lastName { get; set; }
        public string? Address { get; set; }
        public DateTime? DateOfBirth { get; set; }

        // setting the foreign key...
        public virtual BooksData.Models.LibraryCard? libraryCard { get; set; }
        public virtual BooksData.Models.LibraryBranch? libraryBranch { get; set; }

    }
}
