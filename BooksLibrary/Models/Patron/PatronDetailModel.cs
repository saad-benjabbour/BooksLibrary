using BooksData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksLibrary.Models.Patron
{
    public class PatronDetailModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get 
            {
                return FirstName + " " + LastName;
            }
        }
        public int LibraryCardId { get; set; }
        public string LibraryBranch { get; set; }
        public string Address { get; set; }
        public DateTime MemberSince { get; set; }
        public string phoneNumber { get; set; }
        public decimal overdueFees { get; set; }
        public IEnumerable<Checkout> AssetsCheckedOut { get; set; }
        public IEnumerable<CheckoutHistory> CheckoutHistory{ get; set; }
        public IEnumerable<Hold> Holds { get; set; }
    }
}
