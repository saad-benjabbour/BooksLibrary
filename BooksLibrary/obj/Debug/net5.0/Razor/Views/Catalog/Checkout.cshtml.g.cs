#pragma checksum "C:\Users\user\source\.CoreProjects\BooksLibrary\BooksLibrary\Views\Catalog\Checkout.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e704af9b2e2ccf7043a620a76e3c86b4c8329ee8"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Catalog_Checkout), @"mvc.1.0.view", @"/Views/Catalog/Checkout.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\user\source\.CoreProjects\BooksLibrary\BooksLibrary\Views\_ViewImports.cshtml"
using BooksLibrary;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\user\source\.CoreProjects\BooksLibrary\BooksLibrary\Views\_ViewImports.cshtml"
using BooksLibrary.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e704af9b2e2ccf7043a620a76e3c86b4c8329ee8", @"/Views/Catalog/Checkout.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"28c0f1050ee8a48a48f2179257b2715388640c91", @"/Views/_ViewImports.cshtml")]
    public class Views_Catalog_Checkout : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<BooksLibrary.Models.CheckoutModels.CheckoutModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<div class=""container"">
    <div class=""card-header clearfix detailHeading"">
        <h2 class=""text-muted"">Checkout Library Item</h2>
    </div>

    <div class=""jumbotron"">
        <div class=""row"">
            <div class=""col-md-3"">
                <div>
                    <p id=""itemTitle"">");
#nullable restore
#line 12 "C:\Users\user\source\.CoreProjects\BooksLibrary\BooksLibrary\Views\Catalog\Checkout.cshtml"
                                 Write(Model.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                    <img class=\"detailImage\"");
            BeginWriteAttribute("src", " src=\"", 427, "\"", 448, 1);
#nullable restore
#line 13 "C:\Users\user\source\.CoreProjects\BooksLibrary\BooksLibrary\Views\Catalog\Checkout.cshtml"
WriteAttributeValue("", 433, Model.ImageUrl, 433, 15, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("\r\n                         alt=\"images_not_displayed\" />\r\n                </div>\r\n            </div>\r\n\r\n            <div class=\"col-md-9\">\r\n");
#nullable restore
#line 20 "C:\Users\user\source\.CoreProjects\BooksLibrary\BooksLibrary\Views\Catalog\Checkout.cshtml"
                 using (Html.BeginForm("PlaceCheckout", "Catalog", FormMethod.Post))
                {
                    

#line default
#line hidden
#nullable disable
#nullable restore
#line 24 "C:\Users\user\source\.CoreProjects\BooksLibrary\BooksLibrary\Views\Catalog\Checkout.cshtml"
               Write(Html.HiddenFor(a => a.AssetId));

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <div>\r\n                        Please insert a library Card ID\r\n                        ");
#nullable restore
#line 27 "C:\Users\user\source\.CoreProjects\BooksLibrary\BooksLibrary\Views\Catalog\Checkout.cshtml"
                   Write(Html.TextBoxFor(a => a.LibraryCardId));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </div>\r\n                    <div>\r\n                        <button type=\"submit\" class=\"btn btn-success btn-lg\">\r\n                            Check Out\r\n                        </button>\r\n                    </div>\r\n");
#nullable restore
#line 34 "C:\Users\user\source\.CoreProjects\BooksLibrary\BooksLibrary\Views\Catalog\Checkout.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </div>\r\n        </div>\r\n    </div>\r\n\r\n</div>\r\n\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<BooksLibrary.Models.CheckoutModels.CheckoutModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
