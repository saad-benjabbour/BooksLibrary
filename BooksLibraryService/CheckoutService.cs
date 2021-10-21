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
    public class CheckoutService : ICheckout
    {
        private readonly BooksLibraryDbContext _context;
        public CheckoutService(BooksLibraryDbContext context)
        {
            _context = context;
        }
        public void Add(Checkout newCheckout)
        {
            _context.Add(newCheckout);
            _context.SaveChanges();
        }

        public IEnumerable<Checkout> GetAll()
        {
            return _context.Checkouts;
        }

        public Checkout GetById(int checkoutId)
        {
            return GetAll().FirstOrDefault(condition => condition.id == checkoutId);
                
        }

        public IEnumerable<CheckoutHistory> GetCheckoutHistroy(int id)
        {
            return _context.checkoutHistories
                .Include(a => a.LibraryAsset)
                .Include(a => a.LibraryCard)
                .Where(condition => condition.LibraryAsset.Id == id);
        }

        private void RemoveCheckouts(int assetId)
        {
            // retrieving the asset in question
            var _asset = _context.Checkouts
                 .FirstOrDefault(condition => condition.libraryAsset.Id == assetId);

            if(_asset != null)
            {
                _context.Remove(_asset);
            }
        }

        private void CloseCheckoutHistory(int assetId, DateTime now)
        {
            // retrieving the asset in question
            var _assetHistory = _context.checkoutHistories
                 .FirstOrDefault(condition => condition.LibraryAsset.Id == assetId
                                 && condition.checkedIn == null);

            if(_assetHistory != null)
            {
                _context.Update(_assetHistory);
                _assetHistory.checkedIn = now;
            }

        }
        public void CheckInItem(int assetId)
        {
            DateTime now = DateTime.Now;
            // 1- retrieving the asset
            var asset = _context.libraryAssets
                .FirstOrDefault(condition => condition.Id == assetId);

            // 2- Remove the checkouts
            RemoveCheckouts(assetId);

            // 3- Closing the checkoutHistory
            CloseCheckoutHistory(assetId, now);

            // 4- retrieving if there is any currentHolds placed on this asset
            var currentHolds = _context.holds
                .Include(a => a.libraryAsset)
                .Include(a => a.LibraryCard)
                .Where(condition => condition.libraryAsset.Id == assetId);

            if(currentHolds.Any())
            {
                CheckingOutToEarliestHold(assetId, currentHolds);
            }
        }

        private void CheckingOutToEarliestHold(int assetId, IQueryable<Hold> currentHolds)
        {
            // getting the earliest hold
            var earliestHold = currentHolds
                .OrderBy(c => c.HoldPlaced)
                .FirstOrDefault();

            // Removing the hold on the asset and getting the libraryCardId
            var libraryCardId = earliestHold.LibraryCard.Id;     
            
            _context.Remove(earliestHold);
            _context.SaveChanges();
            // checkingOut the asset
            CheckOutItem(assetId, libraryCardId);
        }

        public void CheckOutItem(int assetId, int libraryCardId)
        {
            DateTime now = DateTime.Now;
            // we can't checkout an item that's already checkedOut
            if(isCheckedOut(assetId))
            {
                return;
            }

            // getting the asset
            var item = _context.libraryAssets
                .FirstOrDefault(a => a.Id == assetId);

            // getting the libraryCard
            var libraryCard = _context.libraryCards
                .Include(card => card.Checkouts)
                .FirstOrDefault(condition => condition.Id == libraryCardId);


            // adding a checkout of this asset in the database
            var checkout = new Checkout
            {
                libraryAsset = item,
                libraryCard = libraryCard,
                since = now,
                until = GetDefaultCheckoutItem(now)
            };
            _context.Add(checkout);
            _context.SaveChanges();
        }

        public bool isCheckedOut(int assetId)
        {
            return _context.Checkouts
                .Where(a => a.libraryAsset.Id == assetId)
                .Any();
        }


        private DateTime GetDefaultCheckoutItem(DateTime now)
        {
            return now.AddDays(30);
        }

      
        public string GetCurrentCheckoutPatron(int assetId)
        {
            var checkout = GetCheckoutByAssetId(assetId);


            if (checkout == null)
                return "not checkedOut";
            var cardId = checkout.libraryCard.Id;

            var patron = _context.BooksLibraryUser
                .Include(p => p.libraryCard)
                .FirstOrDefault(condition => condition.libraryCard.Id == cardId);

            return patron.firstName + " " + patron.lastName; 
        }
        
        private Checkout GetCheckoutByAssetId(int assetId)
        {
            return _context.Checkouts
                .Include(a => a.libraryAsset)
                .Include(a => a.libraryCard)
                .FirstOrDefault(condition => condition.libraryAsset.Id == assetId);
        }
        

        // getting the patron name that currently has a hold on an asset
        public string GetCurrentHoldPatronName(int holdId)
        {
            var hold = _context.holds
                .Include(a => a.libraryAsset)
                .Include(a => a.LibraryCard)
                .FirstOrDefault(condition => condition.Id == holdId);

            // getting the libraryCardId
            var cardId = hold.LibraryCard.Id;

            var patron = _context.BooksLibraryUser
                .Include(c => c.libraryCard)
                .FirstOrDefault(condition => condition.libraryCard.Id == cardId);

            return patron.firstName + "" + patron.lastName;
        }

        public DateTime GetCurrentHoldPlace(int holdId)
        {
            return _context.holds
                .Include(a => a.libraryAsset)
                .Include(a => a.LibraryCard)
                .FirstOrDefault(condition => condition.Id == holdId)
                .HoldPlaced;
        }

        public IEnumerable<Hold> GetCurrentHolds(int assetId)
        {
            return _context.holds
                .Include(a => a.libraryAsset) 
                .Where(condition => condition.libraryAsset.Id == assetId);
        }

        public Checkout GetLatestCheckout(int assetId)
        {
            return _context.Checkouts
                .Where(condition => condition.libraryAsset.Id == assetId)
                .OrderByDescending(c => c.since)
                .FirstOrDefault();
        }

        public void MarkFound(int assetId)
        {
            DateTime now = DateTime.Now;

            updateStatus(assetId, "Available");

            RemoveCheckouts(assetId);

            CloseCheckoutHistory(assetId, now);

            _context.SaveChanges();
        }

        public void MarkLost(int assetId)
        {
            updateStatus(assetId, "Lost");
            _context.SaveChanges();
        }

        // updating a status of an asset
        private void updateStatus(int assetId, string statusName)
        {
            var asset = _context.libraryAssets
                .FirstOrDefault(c => c.Id == assetId);

            // mark the asset for an update
            _context.Update(asset);

            asset.Status = _context.Status
                .FirstOrDefault(s => s.Name == statusName);
        }


        public void PlaceHold(int assetId, int libraryCardId)
        {
            DateTime now = DateTime.Now;
            // 1- retrieving the libraryCard ID of the patron
            var _libraryCardId = _context.libraryCards
                .FirstOrDefault(condition => condition.Id == libraryCardId);

            // 2- retrieving the assetId
            var _asset = _context.libraryAssets
                .Include(a => a.Status)
                .FirstOrDefault(condition => condition.Id == assetId);

            // 3- chaning the status of the asset if possible
            if (_asset.Status.Name == "Available")
                updateStatus(_asset.Id, "On Hold");

            // 4- updating the hold table with a new status
            var hold = new Hold
            {
                libraryAsset = _asset,
                LibraryCard = _libraryCardId,
                HoldPlaced = now
            };

            _context.Add(hold);
            _context.SaveChanges();
        }
    }
}
