#pragma checksum "C:\Users\msangara\Documents\CosmosAttachments\Resorts\Resorts.Frontend\Views\Resorts\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a46bbbb5644a1e8c12ff6388dba3944a93e53f40"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Resorts_Details), @"mvc.1.0.view", @"/Views/Resorts/Details.cshtml")]
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
#line 1 "C:\Users\msangara\Documents\CosmosAttachments\Resorts\Resorts.Frontend\Views\_ViewImports.cshtml"
using Resorts.Frontend;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\msangara\Documents\CosmosAttachments\Resorts\Resorts.Frontend\Views\_ViewImports.cshtml"
using Resorts.Frontend.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a46bbbb5644a1e8c12ff6388dba3944a93e53f40", @"/Views/Resorts/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1c7fa3958d18f4cf0e3a0787b238c13bf7eaae10", @"/Views/_ViewImports.cshtml")]
    public class Views_Resorts_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Resorts.Frontend.Models.ResortInfo>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\msangara\Documents\CosmosAttachments\Resorts\Resorts.Frontend\Views\Resorts\Details.cshtml"
  
    ViewData["Title"] = "Details";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>Details</h1>\r\n\r\n<div>\r\n    <h4>ResortInfo</h4>\r\n    <hr />\r\n    <dl class=\"row\">\r\n        ");
#nullable restore
#line 13 "C:\Users\msangara\Documents\CosmosAttachments\Resorts\Resorts.Frontend\Views\Resorts\Details.cshtml"
   Write(Html.HiddenFor(model => model.ResortId));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 16 "C:\Users\msangara\Documents\CosmosAttachments\Resorts\Resorts.Frontend\Views\Resorts\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.ResortName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 19 "C:\Users\msangara\Documents\CosmosAttachments\Resorts\Resorts.Frontend\Views\Resorts\Details.cshtml"
       Write(Html.DisplayFor(model => model.ResortName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 22 "C:\Users\msangara\Documents\CosmosAttachments\Resorts\Resorts.Frontend\Views\Resorts\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Description));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 25 "C:\Users\msangara\Documents\CosmosAttachments\Resorts\Resorts.Frontend\Views\Resorts\Details.cshtml"
       Write(Html.DisplayFor(model => model.Description));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n    </dl>\r\n    <div>\r\n");
#nullable restore
#line 29 "C:\Users\msangara\Documents\CosmosAttachments\Resorts\Resorts.Frontend\Views\Resorts\Details.cshtml"
         foreach (var img in Model.Images)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <span>\r\n                <img");
            BeginWriteAttribute("src", " src=\"", 769, "\"", 779, 1);
#nullable restore
#line 32 "C:\Users\msangara\Documents\CosmosAttachments\Resorts\Resorts.Frontend\Views\Resorts\Details.cshtml"
WriteAttributeValue("", 775, img, 775, 4, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n            </span>\r\n");
#nullable restore
#line 34 "C:\Users\msangara\Documents\CosmosAttachments\Resorts\Resorts.Frontend\Views\Resorts\Details.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n</div>\r\n<div>\r\n    ");
#nullable restore
#line 39 "C:\Users\msangara\Documents\CosmosAttachments\Resorts\Resorts.Frontend\Views\Resorts\Details.cshtml"
Write(Html.ActionLink("Edit", "Edit", new { id = Model.ResortId }));

#line default
#line hidden
#nullable disable
            WriteLiteral(" |\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a46bbbb5644a1e8c12ff6388dba3944a93e53f406766", async() => {
                WriteLiteral("Back to List");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Resorts.Frontend.Models.ResortInfo> Html { get; private set; }
    }
}
#pragma warning restore 1591
