using System;
using System.Collections.Generic;
using System.Text;
using BooksData.Models;

namespace BooksData
{
    public interface ILibraryAsset
    {
        // this will contain all the CRUD operations for LibraryAsset entity
        IEnumerable<LibraryAsset> GetAll();

        LibraryAsset getById(int assetId);

        void AddAsset(LibraryAsset newAsset);

        string GetAuthorOrDirector(int id);

        string GetDeweyIndex(int assetId);
        string GetType(int assetId);
        string GetTitle(int assetId);
        string GetIsbn(int assetId);
        string GetAuthor(int assetId);
        string GetDirector(int assetId);

        LibraryBranch GetCurrentLocation(int assetId);
        LibraryCard GetLibraryCardByAssetId(int assetId);
    }
}
