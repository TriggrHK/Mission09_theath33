using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Bookstore.Models.ViewModels;

namespace Bookstore.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "page-inf")]
    public class PaginationTagHelper : TagHelper
    {
        //Dynamically create the page links for us

        private IUrlHelperFactory uhf;

        public PaginationTagHelper(IUrlHelperFactory temp)
        {
            uhf = temp;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext vc { get; set; }

        //different than the view context
        public PageInfo PageInf { get; set; }
        public string PageAct { get; set; }
        public bool PageClassesEnabled { get; set; } = false;
        public string PageClass { get; set; }
        public string PageClassNormal { get; set; }
        public string PageClassSelected { get; set; }

        public override void Process(TagHelperContext thc, TagHelperOutput tho)
        {
            IUrlHelper uh = uhf.GetUrlHelper(vc);

            TagBuilder final = new TagBuilder("div");

            for (int i = 1; i <= PageInf.TotalPages; i++)
            {
                TagBuilder tb = new TagBuilder("a");
                tb.Attributes["href"] = uh.Action(PageAct, new { pageNum = i });

                if (PageClassesEnabled)
                {
                    tb.AddCssClass(PageClass);
                    tb.AddCssClass(i == PageInf.CurrentPage
                        ? PageClassSelected : PageClassNormal);
                }

                tb.InnerHtml.Append(i.ToString());

                final.InnerHtml.AppendHtml(tb);

            }
            tho.Content.AppendHtml(final.InnerHtml);
        }
    }
}
