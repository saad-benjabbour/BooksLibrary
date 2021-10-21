using BooksData;
using BooksData.Models;
using BooksLibrary.Models.Patron;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksLibrary.Controllers
{
    public class PatronController : Controller
    {
        private IPatron _patron;
        public PatronController(IPatron patron)
        {
            _patron = patron;
        }

        public IActionResult Index()
        {
            var allPatrons = _patron.GetAll();
            var patronModels = allPatrons
                .Select(a => new PatronDetailModel
                {
                    Id = a.Id,
                    FirstName = a.firstName,
                    LastName = a.lastName,
                    overdueFees = a.libraryCard.Fees,
                    LibraryBranch = a.libraryBranch.Name
                }).ToList();

            var model = new PatronIndexModel
            {
                Patrons = patronModels
            };

            return View(model); // Index.cshtml for patron
        }

        public IActionResult Detail(int id)
        {
            var patron = _patron.Get(id);
            var model = new PatronDetailModel
            {
                LastName = patron.lastName,
                FirstName = patron.firstName,
                Address = patron.Address,
                LibraryBranch = patron.libraryBranch.Name,
                MemberSince = patron.libraryCard.Created,
                overdueFees = patron.libraryCard.Fees,
                LibraryCardId = patron.libraryCard.Id,
                phoneNumber = patron.PhoneNumber,
                AssetsCheckedOut = _patron.GetCheckouts(id).ToList() ?? new List<Checkout>(),
                CheckoutHistory = _patron.GetCheckoutHistory(id),
                Holds = _patron.GetHolds(id)
            };
            return View(model);
        }

    }
}
