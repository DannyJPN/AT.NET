#pragma checksum "C:\Users\kru0142\Desktop\AT.NET\Tasks\ex5\EighthWebApplication\EighthWebApplication\Views\Home\ProductDetail.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ca975189ad861d3d86341636acf4335dea7bb051"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_ProductDetail), @"mvc.1.0.view", @"/Views/Home/ProductDetail.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/ProductDetail.cshtml", typeof(AspNetCore.Views_Home_ProductDetail))]
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
#line 1 "C:\Users\kru0142\Desktop\AT.NET\Tasks\ex5\EighthWebApplication\EighthWebApplication\Views\_ViewImports.cshtml"
using EighthWebApplication;

#line default
#line hidden
#line 2 "C:\Users\kru0142\Desktop\AT.NET\Tasks\ex5\EighthWebApplication\EighthWebApplication\Views\_ViewImports.cshtml"
using EighthWebApplication.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ca975189ad861d3d86341636acf4335dea7bb051", @"/Views/Home/ProductDetail.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3fc874650edfd36bfb75c30725e6e2c093d41053", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_ProductDetail : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<BasketForm>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(20, 15, true);
            WriteLiteral("ProductDetail\r\n");
            EndContext();
#line 3 "C:\Users\kru0142\Desktop\AT.NET\Tasks\ex5\EighthWebApplication\EighthWebApplication\Views\Home\ProductDetail.cshtml"
   

    Product pr = ViewBag.Product;
    string bdata = ViewBag.Basket;

#line default
#line hidden
            BeginContext(116, 12, true);
            WriteLiteral("\r\n\r\n\r\n\r\n<h2>");
            EndContext();
            BeginContext(129, 7, false);
#line 12 "C:\Users\kru0142\Desktop\AT.NET\Tasks\ex5\EighthWebApplication\EighthWebApplication\Views\Home\ProductDetail.cshtml"
Write(pr.Name);

#line default
#line hidden
            EndContext();
            BeginContext(136, 16, true);
            WriteLiteral("</h2>\r\n<p>Cena: ");
            EndContext();
            BeginContext(153, 8, false);
#line 13 "C:\Users\kru0142\Desktop\AT.NET\Tasks\ex5\EighthWebApplication\EighthWebApplication\Views\Home\ProductDetail.cshtml"
    Write(pr.Price);

#line default
#line hidden
            EndContext();
            BeginContext(161, 10, true);
            WriteLiteral("</p>\r\n\r\n\r\n");
            EndContext();
#line 16 "C:\Users\kru0142\Desktop\AT.NET\Tasks\ex5\EighthWebApplication\EighthWebApplication\Views\Home\ProductDetail.cshtml"
 using (Html.BeginForm())
{
     

#line default
#line hidden
            BeginContext(207, 30, false);
#line 18 "C:\Users\kru0142\Desktop\AT.NET\Tasks\ex5\EighthWebApplication\EighthWebApplication\Views\Home\ProductDetail.cshtml"
Write(Html.HiddenFor(x=>x.ProductID));

#line default
#line hidden
            EndContext();
            BeginContext(239, 36, true);
            WriteLiteral("    <div>\r\n        Pieces:\r\n        ");
            EndContext();
            BeginContext(276, 29, false);
#line 21 "C:\Users\kru0142\Desktop\AT.NET\Tasks\ex5\EighthWebApplication\EighthWebApplication\Views\Home\ProductDetail.cshtml"
   Write(Html.TextBoxFor(x => x.Count));

#line default
#line hidden
            EndContext();
            BeginContext(305, 11, true);
            WriteLiteral(";\r\n        ");
            EndContext();
            BeginContext(317, 39, false);
#line 22 "C:\Users\kru0142\Desktop\AT.NET\Tasks\ex5\EighthWebApplication\EighthWebApplication\Views\Home\ProductDetail.cshtml"
   Write(Html.ValidationMessageFor(x => x.Count));

#line default
#line hidden
            EndContext();
            BeginContext(356, 82, true);
            WriteLiteral(";\r\n    </div>\r\n    <div>\r\n        <button type=\"submit\">Add</button>\r\n    </div>\r\n");
            EndContext();
#line 27 "C:\Users\kru0142\Desktop\AT.NET\Tasks\ex5\EighthWebApplication\EighthWebApplication\Views\Home\ProductDetail.cshtml"


}

#line default
#line hidden
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<BasketForm> Html { get; private set; }
    }
}
#pragma warning restore 1591