#pragma checksum "D:\Projects\cs\lista_7_zadanie\lista_7\lista_7\Views\Home\Students.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d922a5fe3b4210a5cedface9fcb76d3c62594d6f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Students), @"mvc.1.0.view", @"/Views/Home/Students.cshtml")]
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
#line 1 "D:\Projects\cs\lista_7_zadanie\lista_7\lista_7\Views\_ViewImports.cshtml"
using lista_7;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Projects\cs\lista_7_zadanie\lista_7\lista_7\Views\_ViewImports.cshtml"
using lista_7.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d922a5fe3b4210a5cedface9fcb76d3c62594d6f", @"/Views/Home/Students.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4289e0473edfce7f3ff1e682f000dd5f6081bcbf", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Students : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<User>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\Projects\cs\lista_7_zadanie\lista_7\lista_7\Views\Home\Students.cshtml"
  
    ViewData["Title"] = "Students";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>Students List</h1>\r\n\r\n\r\n");
#nullable restore
#line 9 "D:\Projects\cs\lista_7_zadanie\lista_7\lista_7\Views\Home\Students.cshtml"
 if (Model.Count() > 0)
{

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    <table class=""table table-striped border"">
        <tr class=""table-secondary"">
            <th>
                <label>Student Nick</label>
            </th>
            <th>
                <label>Student Email</label>
            </th>
            <th>
                <label> Name </label>
            </th>
            <th>
                <label> LastName </label>
            </th>
            <th>

            </th>
            <th>

            </th>
        </tr>
");
#nullable restore
#line 32 "D:\Projects\cs\lista_7_zadanie\lista_7\lista_7\Views\Home\Students.cshtml"
         foreach (var student in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("    <tr>\r\n        <td>\r\n            ");
#nullable restore
#line 36 "D:\Projects\cs\lista_7_zadanie\lista_7\lista_7\Views\Home\Students.cshtml"
       Write(Html.DisplayFor(m => student.Username));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </td>\r\n        <td>\r\n            ");
#nullable restore
#line 39 "D:\Projects\cs\lista_7_zadanie\lista_7\lista_7\Views\Home\Students.cshtml"
       Write(Html.DisplayFor(m => student.Email));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </td>\r\n        <td>\r\n            ");
#nullable restore
#line 42 "D:\Projects\cs\lista_7_zadanie\lista_7\lista_7\Views\Home\Students.cshtml"
       Write(Html.DisplayFor(m => student.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </td>\r\n        <td>\r\n            ");
#nullable restore
#line 45 "D:\Projects\cs\lista_7_zadanie\lista_7\lista_7\Views\Home\Students.cshtml"
       Write(Html.DisplayFor(m => student.LastName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </td>\r\n        <td>\r\n\r\n        </td>\r\n        <td>\r\n\r\n        </td>\r\n    </tr>\r\n");
#nullable restore
#line 54 "D:\Projects\cs\lista_7_zadanie\lista_7\lista_7\Views\Home\Students.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </table> \r\n");
#nullable restore
#line 56 "D:\Projects\cs\lista_7_zadanie\lista_7\lista_7\Views\Home\Students.cshtml"
}
else
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <p class=\"badge-dark\"><b>None student find</b></p>\r\n");
#nullable restore
#line 60 "D:\Projects\cs\lista_7_zadanie\lista_7\lista_7\Views\Home\Students.cshtml"
}

#line default
#line hidden
#nullable disable
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<User>> Html { get; private set; }
    }
}
#pragma warning restore 1591