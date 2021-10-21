using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace BooksData.Models
{
    public class LibraryCard
    {
        public int Id { get; set; }
        [Display(Name = "Overdue Feed")]
        public decimal Fees { get; set; }
        [Display(Name = "Overdue Feed")]
        public DateTime Created { get; set; }
        [Display(Name = "Materials On Loan")]
        public virtual IEnumerable<Checkout> Checkouts { get; set; }
    }
}
