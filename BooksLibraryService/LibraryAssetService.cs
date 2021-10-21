using System;
using System.Collections.Generic;
using System.Linq;
using BooksData;
using BooksData.Models;
using BooksLibrary.Data;
using Microsoft.EntityFrameworkCore;

namespace BooksLibraryService
{
    public class LibraryAssetService : ILibraryAsset
    {
        // creating a reference to the DBContext to connect to out database
        private readonly BooksLibraryDbContext _context;
        public LibraryAssetService(BooksLibraryDbContext context)
        {
            _context = context;
        }

        public void AddAsset(LibraryAsset newAsset)
        {
            _context.Add(newAsset);
            _context.SaveChanges();
        }

        public IEnumerable<LibraryAsset> GetAll()
        {
            // Getting libraryAssets, its status and which libraryBranch its located...
            return _context.libraryAssets
                .Include(a => a.Status)
                .Include(a => a.Location)
                .OrderBy(a => a.Id);
        }

        public string GetAuthor(int assetId)
        {
            var book = (Book)getById(assetId);
            return book.Author;
        }

        public string GetDirector(int assetId)
        {
            var video = (Video)getById(assetId);
            return video.Director;
        }

        public string GetAuthorOrDirector(int assetId)
        {
            var isBook = _context.libraryAssets
                .OfType<Book>().Any(a => a.Id == assetId);

            return isBook ? GetAuthor(assetId) : GetDirector(assetId) ?? "Uknown Type";
        }

        public LibraryAsset getById(int assetId)
        {
            // condition represent the whole select query result
            // its the concept of lambda expression
            return _context.libraryAssets
                .Include(a => a.Status)
                .Include(a => a.Location)
                .FirstOrDefault(condition => condition.Id == assetId);
        }

        public LibraryBranch GetCurrentLocation(int assetId)
        {
            return _context.libraryAssets
                .First(a => a.Id == assetId).Location;
        }

        public string GetDeweyIndex(int assetId)
        {
            if (GetType(assetId) == "Book")
            {
                var book = (Book)getById(assetId);
                return book.DeweyIndex;
            }
            else
                return "Not Available";
        }


        public string GetIsbn(int assetId)
        {
            if (GetType(assetId) == "Book")
            {
                var book = (Book)getById(assetId);
                return book.ISBN.ToString();
            }
            else
                return "Not Available";
        }

        public LibraryCard GetLibraryCardByAssetId(int assetId)
        {
            // to pass from a table to another one without using include
            return _context.libraryCards
                .FirstOrDefault(c => c.Checkouts
                .Select(a => a.libraryAsset)
                .Select(v => v.Id).Contains(assetId));
        }

        public string GetTitle(int assetId)
        {
            return _context.libraryAssets
                .FirstOrDefault(a => a.Id == assetId)
                .Title;
        }

        public string GetType(int assetId)
        {
            var isBook = _context.libraryAssets
               .OfType<Book>().SingleOrDefault(a => a.Id == assetId);

            return isBook != null ? "Book" : "Video" ?? "Unkown type";

        }
    }
}
