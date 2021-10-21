using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksLibrary.Models.Catalog
{
    public class AssetIndexModel
    {
        public IEnumerable<AssetIndexListingModel> assets { get; set; }
    }
}
