using BooksData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BooksData
{
    public interface ICheckout
    {
        // will contain the CRUD operations for checkout service
        IEnumerable<Checkout> GetAll();
        Checkout GetById(int checkoutId);

        void Add(Checkout newCheckout);
        void CheckOutItem(int assetId, int libraryCardId);
        void CheckInItem(int assetId);

        IEnumerable<CheckoutHistory> GetCheckoutHistroy(int id);

        void PlaceHold(int assetId, int libraryCard);
        string GetCurrentHoldPatronName(int holdId);

        DateTime GetCurrentHoldPlace(int holdId);

        IEnumerable<Hold> GetCurrentHolds(int assetId);

        void MarkLost(int assetId);
        void MarkFound(int assetId);

        Checkout GetLatestCheckout(int assetId);

        string GetCurrentCheckoutPatron(int assetId);

        bool isCheckedOut(int assetId);
    }
}
