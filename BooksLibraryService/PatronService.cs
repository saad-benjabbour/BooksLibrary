using BooksData;
using BooksData.Models;
using BooksLibrary.Areas.Identity.Data;
using BooksLibrary.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BooksLibraryService
{
    public class PatronService : IPatron
    {
        private readonly BooksLibraryDbContext _context;
        public PatronService(BooksLibraryDbContext context)
        {
            _context = context;
        }
        public void Add(BooksLibraryUser newUser)
        {
            _context.Add(newUser);
            _context.SaveChanges();
        }

        public BooksLibraryUser Get(int userId)
        {
            return GetAll()
                 .FirstOrDefault(a => a.Id == userId.ToString());
        }

        public IEnumerable<BooksLibraryUser> GetAll()
        {
            return (IEnumerable<BooksLibraryUser>)(BooksLibraryUser)_context.BooksLibraryUser
               .Include(a => a.libraryCard)
               .Include(a => a.libraryBranch);
        }

        public IEnumerable<CheckoutHistory> GetCheckoutHistory(int userId)
        {
            // getting the libraryCard id of the patron
            var cardId = _context.BooksLibraryUser
                .Include(a => a.libraryCard)
                .FirstOrDefault(a => a.Id == userId.ToString())
                .libraryCard.Id;

            return _context.checkoutHistories
                .Include(a => a.LibraryCard)
                .Include(a => a.LibraryAsset)
                .Where(condition => condition.LibraryCard.Id == cardId)
                .OrderByDescending(a => a.checkedOut);

        }

        public IEnumerable<Checkout> GetCheckouts(int userId)
        {
            var cardId = _context.BooksLibraryUser
                .Include(a => a.libraryCard)
                .FirstOrDefault(a => a.Id == userId.ToString())
                .libraryCard.Id;

            return _context.Checkouts
                .Include(a => a.libraryAsset)
                .Include(a => a.libraryCard)
                .Where(condition => condition.libraryCard.Id == cardId)
                .OrderByDescending(a => a.since);
        }

        // getHold by patron
        public IEnumerable<Hold> GetHolds(int userId)
        {
            var cardId = _context.BooksLibraryUser
                .Include(a => a.libraryCard)
                .FirstOrDefault(condition => condition.Id == userId.ToString())
                .libraryCard.Id;

            return _context.holds
                .Include(a => a.libraryAsset)
                .Include(a => a.LibraryCard)
                .Where(condition => condition.LibraryCard.Id == cardId)
                .OrderByDescending(a => a.HoldPlaced);
            
        }
    }
}
