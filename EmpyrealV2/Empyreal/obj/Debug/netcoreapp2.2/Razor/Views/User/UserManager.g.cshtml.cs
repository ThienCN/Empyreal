#pragma checksum "D:\BoJiJi\KLTN\Project\EmpyrealV2\EmpyrealV2\Empyreal\Views\User\UserManager.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1f345fadbf106a5d27763deae69031920ed00933"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_User_UserManager), @"mvc.1.0.view", @"/Views/User/UserManager.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/User/UserManager.cshtml", typeof(AspNetCore.Views_User_UserManager))]
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
#line 1 "D:\BoJiJi\KLTN\Project\EmpyrealV2\EmpyrealV2\Empyreal\Views\User\UserManager.cshtml"
using PagedList.Core;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1f345fadbf106a5d27763deae69031920ed00933", @"/Views/User/UserManager.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"524330925d5bef7a58776afac88f5862ab279611", @"/Views/_ViewImports.cshtml")]
    public class Views_User_UserManager : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Empyreal.ViewModels.Manager.UserManagerViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "get", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "User", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "UserManager", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("primary-btn"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "AddUser", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("main-btn"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("width: 30px; height: 30px; padding: 6px;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("title", new global::Microsoft.AspNetCore.Html.HtmlString("Chỉnh sửa sản phẩm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "UpdateUser", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_9 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("title", new global::Microsoft.AspNetCore.Html.HtmlString("Ngưng hoạt động"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_10 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "DisableUser", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_11 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("title", new global::Microsoft.AspNetCore.Html.HtmlString("Cho phép hoạt động"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_12 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "EnableUser", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        private global::PagedList.Core.Mvc.PagerTagHelper __PagedList_Core_Mvc_PagerTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 3 "D:\BoJiJi\KLTN\Project\EmpyrealV2\EmpyrealV2\Empyreal\Views\User\UserManager.cshtml"
  
    ViewData["Title"] = "Quản lý người dùng";

#line default
#line hidden
            BeginContext(134, 353, true);
            WriteLiteral(@"
<!-- BREADCRUMB -->
<div id=""breadcrumb"">
    <div class=""container"">
        <ul class=""breadcrumb"">
            <li><a href=""#"">Trang chủ</a></li>
            <li class=""active"">Quản lý người dùng</li>
        </ul>
    </div>
</div>

<div class=""section-grey"">
    <div class=""container"">
        <div class=""row account"">
            ");
            EndContext();
            BeginContext(488, 57, false);
#line 20 "D:\BoJiJi\KLTN\Project\EmpyrealV2\EmpyrealV2\Empyreal\Views\User\UserManager.cshtml"
       Write(await Html.PartialAsync("Manager\\_MenuLeftAdminPartial"));

#line default
#line hidden
            EndContext();
            BeginContext(545, 175, true);
            WriteLiteral("\r\n            <div class=\"col-lg-9 col-xs-12 content-right\" id=\"content-right\">\r\n                <h4>Danh sách người dùng</h4>\r\n                <div class=\"account-profile\">\r\n");
            EndContext();
            BeginContext(1782, 99, true);
            WriteLiteral("                    <div class=\"header-search\" style=\"max-width: 600px;\">\r\n                        ");
            EndContext();
            BeginContext(1881, 354, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "1f345fadbf106a5d27763deae69031920ed009339512", async() => {
                BeginContext(1947, 281, true);
                WriteLiteral(@"
                            <input class=""input search-input"" type=""text"" name=""keySearch"" placeholder=""Nhập tên người dùng"" />
                            <button type=""submit"" title=""Tìm kiếm"" class=""search-btn""><i class=""fas fa-search""></i></button>
                        ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2235, 50, true);
            WriteLiteral("\r\n                    </div>\r\n                    ");
            EndContext();
            BeginContext(2285, 113, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "1f345fadbf106a5d27763deae69031920ed0093311912", async() => {
                BeginContext(2351, 43, true);
                WriteLiteral("<i class=\"fas fa-plus\"></i> Thêm người dùng");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2398, 4, true);
            WriteLiteral("\r\n\r\n");
            EndContext();
#line 46 "D:\BoJiJi\KLTN\Project\EmpyrealV2\EmpyrealV2\Empyreal\Views\User\UserManager.cshtml"
                     if (Model.PagedUserModel != null)
                    {

#line default
#line hidden
            BeginContext(2481, 836, true);
            WriteLiteral(@"                        <div class=""table-responsive"">
                            <table class=""table table-bordered table-hover"">
                                <thead class=""active"">
                                    <tr>
                                        <th style=""width: 50px; text-align: center;"">STT</th>
                                        <th style=""display: none;"">Id</th>
                                        <th>Thao tác</th>
                                        <th>Họ tên</th>
                                        <th>Phân quyền</th>
                                        <th>Email</th>
                                        <th>SĐT</th>
                                        <th>Trạng thái</th>
                                    </tr>
                                </thead>
");
            EndContext();
#line 62 "D:\BoJiJi\KLTN\Project\EmpyrealV2\EmpyrealV2\Empyreal\Views\User\UserManager.cshtml"
                                   int k = 1;

#line default
#line hidden
            BeginContext(3364, 45, true);
            WriteLiteral("                                    <tbody>\r\n");
            EndContext();
#line 64 "D:\BoJiJi\KLTN\Project\EmpyrealV2\EmpyrealV2\Empyreal\Views\User\UserManager.cshtml"
                                         foreach (var user in Model.PagedUserModel)
                                        {

#line default
#line hidden
            BeginContext(3537, 130, true);
            WriteLiteral("                                            <tr>\r\n                                                <td style=\"text-align: center;\">");
            EndContext();
            BeginContext(3668, 1, false);
#line 67 "D:\BoJiJi\KLTN\Project\EmpyrealV2\EmpyrealV2\Empyreal\Views\User\UserManager.cshtml"
                                                                           Write(k);

#line default
#line hidden
            EndContext();
            BeginContext(3669, 83, true);
            WriteLiteral(" </td>\r\n                                                <td style=\"display: none;\">");
            EndContext();
            BeginContext(3753, 7, false);
#line 68 "D:\BoJiJi\KLTN\Project\EmpyrealV2\EmpyrealV2\Empyreal\Views\User\UserManager.cshtml"
                                                                      Write(user.ID);

#line default
#line hidden
            EndContext();
            BeginContext(3760, 141, true);
            WriteLiteral("</td>\r\n                                                <td style=\"text-align: center;\">\r\n                                                    ");
            EndContext();
            BeginContext(3901, 197, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "1f345fadbf106a5d27763deae69031920ed0093316591", async() => {
                BeginContext(4067, 27, true);
                WriteLiteral("<i class=\"far fa-edit\"></i>");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_8.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 70 "D:\BoJiJi\KLTN\Project\EmpyrealV2\EmpyrealV2\Empyreal\Views\User\UserManager.cshtml"
                                                                                                                                                                                                    WriteLiteral(user.ID);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(4098, 4, true);
            WriteLiteral(" |\r\n");
            EndContext();
#line 71 "D:\BoJiJi\KLTN\Project\EmpyrealV2\EmpyrealV2\Empyreal\Views\User\UserManager.cshtml"
                                                     if (user.State == 1)
                                                    {

#line default
#line hidden
            BeginContext(4232, 56, true);
            WriteLiteral("                                                        ");
            EndContext();
            BeginContext(4288, 195, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "1f345fadbf106a5d27763deae69031920ed0093319977", async() => {
                BeginContext(4452, 27, true);
                WriteLiteral("<i class=\"fas fa-lock\"></i>");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_9);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_10.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_10);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 73 "D:\BoJiJi\KLTN\Project\EmpyrealV2\EmpyrealV2\Empyreal\Views\User\UserManager.cshtml"
                                                                                                                                                                                                      WriteLiteral(user.ID);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(4483, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 74 "D:\BoJiJi\KLTN\Project\EmpyrealV2\EmpyrealV2\Empyreal\Views\User\UserManager.cshtml"
                                                    }
                                                    else
                                                    {

#line default
#line hidden
            BeginContext(4653, 56, true);
            WriteLiteral("                                                        ");
            EndContext();
            BeginContext(4709, 203, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "1f345fadbf106a5d27763deae69031920ed0093323403", async() => {
                BeginContext(4875, 33, true);
                WriteLiteral("<i class=\"fas fa-unlock-alt\"></i>");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_11);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_12.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_12);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 77 "D:\BoJiJi\KLTN\Project\EmpyrealV2\EmpyrealV2\Empyreal\Views\User\UserManager.cshtml"
                                                                                                                                                                                                        WriteLiteral(user.ID);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(4912, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 78 "D:\BoJiJi\KLTN\Project\EmpyrealV2\EmpyrealV2\Empyreal\Views\User\UserManager.cshtml"
                                                    }

#line default
#line hidden
            BeginContext(4969, 107, true);
            WriteLiteral("                                                </td>\r\n                                                <td>");
            EndContext();
            BeginContext(5077, 10, false);
#line 80 "D:\BoJiJi\KLTN\Project\EmpyrealV2\EmpyrealV2\Empyreal\Views\User\UserManager.cshtml"
                                               Write(user.HoTen);

#line default
#line hidden
            EndContext();
            BeginContext(5087, 61, true);
            WriteLiteral("</td>\r\n                                                <td>\r\n");
            EndContext();
#line 82 "D:\BoJiJi\KLTN\Project\EmpyrealV2\EmpyrealV2\Empyreal\Views\User\UserManager.cshtml"
                                                     foreach (var role in user.Roles)
                                                    {
                                                        

#line default
#line hidden
            BeginContext(5347, 4, false);
#line 84 "D:\BoJiJi\KLTN\Project\EmpyrealV2\EmpyrealV2\Empyreal\Views\User\UserManager.cshtml"
                                                   Write(role);

#line default
#line hidden
            EndContext();
#line 84 "D:\BoJiJi\KLTN\Project\EmpyrealV2\EmpyrealV2\Empyreal\Views\User\UserManager.cshtml"
                                                             
                                                    }

#line default
#line hidden
            BeginContext(5408, 107, true);
            WriteLiteral("                                                </td>\r\n                                                <td>");
            EndContext();
            BeginContext(5516, 10, false);
#line 87 "D:\BoJiJi\KLTN\Project\EmpyrealV2\EmpyrealV2\Empyreal\Views\User\UserManager.cshtml"
                                               Write(user.Email);

#line default
#line hidden
            EndContext();
            BeginContext(5526, 59, true);
            WriteLiteral("</td>\r\n                                                <td>");
            EndContext();
            BeginContext(5586, 16, false);
#line 88 "D:\BoJiJi\KLTN\Project\EmpyrealV2\EmpyrealV2\Empyreal\Views\User\UserManager.cshtml"
                                               Write(user.PhoneNumber);

#line default
#line hidden
            EndContext();
            BeginContext(5602, 7, true);
            WriteLiteral("</td>\r\n");
            EndContext();
#line 89 "D:\BoJiJi\KLTN\Project\EmpyrealV2\EmpyrealV2\Empyreal\Views\User\UserManager.cshtml"
                                                 if (user.State == 1)
                                                {

#line default
#line hidden
            BeginContext(5731, 72, true);
            WriteLiteral("                                                    <td>Hoạt động</td>\r\n");
            EndContext();
#line 92 "D:\BoJiJi\KLTN\Project\EmpyrealV2\EmpyrealV2\Empyreal\Views\User\UserManager.cshtml"
                                                }
                                                else
                                                {

#line default
#line hidden
            BeginContext(5959, 78, true);
            WriteLiteral("                                                    <td>Ngưng hoạt động</td>\r\n");
            EndContext();
#line 96 "D:\BoJiJi\KLTN\Project\EmpyrealV2\EmpyrealV2\Empyreal\Views\User\UserManager.cshtml"
                                                }

#line default
#line hidden
            BeginContext(6088, 51, true);
            WriteLiteral("                                            </tr>\r\n");
            EndContext();
#line 98 "D:\BoJiJi\KLTN\Project\EmpyrealV2\EmpyrealV2\Empyreal\Views\User\UserManager.cshtml"
                                            k++;
                                        }

#line default
#line hidden
            BeginContext(6232, 159, true);
            WriteLiteral("\r\n                                        <tr>\r\n                                            <td colspan=\"10\">\r\n                                                ");
            EndContext();
            BeginContext(6391, 288, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("pager", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "1f345fadbf106a5d27763deae69031920ed0093330771", async() => {
            }
            );
            __PagedList_Core_Mvc_PagerTagHelper = CreateTagHelper<global::PagedList.Core.Mvc.PagerTagHelper>();
            __tagHelperExecutionContext.Add(__PagedList_Core_Mvc_PagerTagHelper);
#line 103 "D:\BoJiJi\KLTN\Project\EmpyrealV2\EmpyrealV2\Empyreal\Views\User\UserManager.cshtml"
__PagedList_Core_Mvc_PagerTagHelper.List = Model.PagedUserModel;

#line default
#line hidden
            __tagHelperExecutionContext.AddTagHelperAttribute("list", __PagedList_Core_Mvc_PagerTagHelper.List, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __PagedList_Core_Mvc_PagerTagHelper.AspController = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __PagedList_Core_Mvc_PagerTagHelper.AspAction = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__PagedList_Core_Mvc_PagerTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-keyword", "PagedList.Core.Mvc.PagerTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 106 "D:\BoJiJi\KLTN\Project\EmpyrealV2\EmpyrealV2\Empyreal\Views\User\UserManager.cshtml"
                                                              WriteLiteral(Model.Keyword);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __PagedList_Core_Mvc_PagerTagHelper.RouteValues["keyword"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-keyword", __PagedList_Core_Mvc_PagerTagHelper.RouteValues["keyword"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(6679, 146, true);
            WriteLiteral("\r\n                                            </td>\r\n                                        </tr>\r\n                                    </tbody>\r\n");
            EndContext();
            BeginContext(6860, 70, true);
            WriteLiteral("                            </table>\r\n                        </div>\r\n");
            EndContext();
#line 113 "D:\BoJiJi\KLTN\Project\EmpyrealV2\EmpyrealV2\Empyreal\Views\User\UserManager.cshtml"
                    }

#line default
#line hidden
            BeginContext(6953, 80, true);
            WriteLiteral("\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Empyreal.ViewModels.Manager.UserManagerViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
