using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;


namespace BooksData.Models
{
    public class BranchHours
    {
        public int id { get; set; }
        public LibraryBranch Branch { get; set; }
        [Range(0, 6)]
        public int dayOfWeek { get; set; }
        [Range(0, 23)]
        public int openTime { get; set; }
        [Range(0, 23)]
        public int closeTime { get; set; }

    }
}
