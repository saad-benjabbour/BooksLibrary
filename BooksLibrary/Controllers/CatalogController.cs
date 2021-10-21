using BooksData;
using BooksData.Models;
using BooksLibrary.Models.Catalog;
using BooksLibrary.Models.CheckoutModels;
using Microsoft.AspNetCore.Mvc;
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksLibrary.Controllers
{
    public class CatalogController : Controller
    {
        private ILibraryAsset _asset;
        private ICheckout _checkout;
        public CatalogController(ILibraryAsset asset, ICheckout checkout)
        {
            _asset = asset;
            _checkout = checkout;

        }

        // home function
        public IActionResult Index()
        {
            var assetModels = _asset.GetAll();
            var test = (IOrderedQueryable)_asset.GetAll();
            var listingResults = assetModels
                .Select(result => new AssetIndexListingModel
                {
                    // mapping the results of GetAll to the properties of AssetIndexLisingModel
                    Id = result.Id,
                    ImageUrl = result.ImageUrl,
                    AuthorOrDirector = _asset.GetAuthorOrDirector(result.Id),
                    DeweyCallNumber = _asset.GetDeweyIndex(result.Id),
                    Title = result.Title,
                    Type = _asset.GetType(result.Id)
                });
            var model = new AssetIndexModel
            {
                assets = listingResults
            };
            // await PagingList.CreateAsync(test, 5, page);



            // returning the view
            return View(model);
        }

        // listing the details of a given asset
        public IActionResult Detail(int id)
        {
            // System.Diagnostics.Debug.WriteLine(id);
            var assetElement = _asset.getById(id);
            var currentHolds = _checkout.GetCurrentHolds(id)
                 .Select(a => new AssetHoldModel
                 {
                     HoldPlaced = _checkout.GetCurrentHoldPlace(a.Id).ToString("d"),
                     PatronName = _checkout.GetCurrentHoldPatronName(a.Id)
                 });
            var model = new AssetDetailModel
            {
                AssetId = assetElement.Id,
                Title = assetElement.Title,
                Cost = assetElement.Cost,
                Status = assetElement.Status.Name,
                ImageUrl = assetElement.ImageUrl,
                AuthorOrDirector = _asset.GetAuthorOrDirector(id),
                CurrentLocation = _asset.GetCurrentLocation(id).Name,
                DeweyCallNumber = _asset.GetDeweyIndex(id),
                ISBN = _asset.GetIsbn(id),
                checkoutHistory = _checkout.GetCheckoutHistroy(id),
                LatestCheckout = _checkout.GetLatestCheckout(id),
                PatronName = _checkout.GetCurrentCheckoutPatron(id),
                CurrentHolds = currentHolds
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult MarkFound(int assetId)
        {
            _checkout.MarkFound(assetId);
            return RedirectToAction("Detail", new { id = assetId });
        }

        [HttpPost]
        public IActionResult MarkLost(int assetId)
        {
            _checkout.MarkLost(assetId);
            return RedirectToAction("Detail", new { id = assetId });
        }
        
        public IActionResult Hold(int id)
        {
            // System.Dagnostics.Debug.WriteLine(id); printing out the value of id
            var asset = _asset.getById(id);
            var model = new CheckoutModel
            {
                AssetId = id,
                ImageUrl = asset.ImageUrl,
                Title = asset.Title,
                LibraryCardId = "",
                IsCheckedOut = _checkout.isCheckedOut(id),
                HoldCount = _checkout.GetCurrentHolds(id).Count()
            };
            return View(model);
        }
        public IActionResult Checkout(int id)
        {
            var asset = _asset.getById(id);
            var model = new CheckoutModel
            {
                AssetId = id,
                ImageUrl = asset.ImageUrl,
                Title = asset.Title,
                LibraryCardId = "",
                IsCheckedOut = _checkout.isCheckedOut(id)
            };
            return View(model);
        }

        /* we need to provide the libraryCardId for the placeHold and placeCheckout 
         * so we need a forum for the user to get us his libraryCardId*/
        [HttpPost]
        public IActionResult PlaceHold(int assetId, int libraryCarId)
        {
            _checkout.PlaceHold(assetId, libraryCarId);
            return RedirectToAction("Detail", new { id = assetId });
        }

        [HttpPost]
        public IActionResult PlaceCheckout(int assetId, int LibraryCardId)
        {
            _checkout.CheckOutItem(assetId, LibraryCardId);
            return RedirectToAction("Detail", new { id = assetId });
        }

        [HttpPost]
        public IActionResult CheckIn(int id)
        {
            _checkout.CheckInItem(id);
            return RedirectToAction("Detail", new { id = id });
        }
    }
}
