#pragma checksum "D:\BoJiJi\KLTN\Project\EmpyrealV2\EmpyrealV2\Empyreal\Views\Catalog\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "40a8aac56cd5c82d8ec01ca4a8a384a4269e00c1"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Catalog_Index), @"mvc.1.0.view", @"/Views/Catalog/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Catalog/Index.cshtml", typeof(AspNetCore.Views_Catalog_Index))]
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
#line 1 "D:\BoJiJi\KLTN\Project\EmpyrealV2\EmpyrealV2\Empyreal\Views\_ViewImports.cshtml"
using Empyreal;

#line default
#line hidden
#line 2 "D:\BoJiJi\KLTN\Project\EmpyrealV2\EmpyrealV2\Empyreal\Views\_ViewImports.cshtml"
using Empyreal.Models;

#line default
#line hidden
#line 3 "D:\BoJiJi\KLTN\Project\EmpyrealV2\EmpyrealV2\Empyreal\Views\_ViewImports.cshtml"
using Empyreal.ViewModels;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"40a8aac56cd5c82d8ec01ca4a8a384a4269e00c1", @"/Views/Catalog/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"524330925d5bef7a58776afac88f5862ab279611", @"/Views/_ViewImports.cshtml")]
    public class Views_Catalog_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Empyreal.ViewModels.CatalogViewModel>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "D:\BoJiJi\KLTN\Project\EmpyrealV2\EmpyrealV2\Empyreal\Views\Catalog\Index.cshtml"
  
    ViewData["Title"] = "Index";

#line default
#line hidden
            BeginContext(92, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 6 "D:\BoJiJi\KLTN\Project\EmpyrealV2\EmpyrealV2\Empyreal\Views\Catalog\Index.cshtml"
 foreach(var catalog in Model)
{

#line default
#line hidden
            BeginContext(129, 7, true);
            WriteLiteral("    <p>");
            EndContext();
            BeginContext(137, 12, false);
#line 8 "D:\BoJiJi\KLTN\Project\EmpyrealV2\EmpyrealV2\Empyreal\Views\Catalog\Index.cshtml"
  Write(catalog.Name);

#line default
#line hidden
            EndContext();
            BeginContext(149, 6, true);
            WriteLiteral("</p>\r\n");
            EndContext();
#line 9 "D:\BoJiJi\KLTN\Project\EmpyrealV2\EmpyrealV2\Empyreal\Views\Catalog\Index.cshtml"
}

#line default
#line hidden
            BeginContext(158, 2, true);
            WriteLiteral("\r\n");
            EndContext();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Empyreal.ViewModels.CatalogViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
