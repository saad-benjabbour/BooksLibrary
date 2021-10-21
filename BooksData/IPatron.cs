using BooksData.Models;
using BooksLibrary.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace BooksData
{
    public interface IPatron
    {
        BooksLibraryUser Get(int userId); // this is the patron
        IEnumerable<BooksLibraryUser> GetAll();
        void Add(BooksLibraryUser newUser);
        IEnumerable<CheckoutHistory> GetCheckoutHistory(int userId);
        IEnumerable<Hold> GetHolds(int userId);
        IEnumerable<Checkout> GetCheckouts(int userId);
    }
}
