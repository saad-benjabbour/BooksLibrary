using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace BooksData.Models
{
    public class Checkout
    {
        public int id { get; set; }

        [Required]
        public LibraryAsset libraryAsset { get; set; }
        public LibraryCard libraryCard { get; set; }
        public DateTime since{ get; set; }
        public DateTime until { get; set; }

    }
}
