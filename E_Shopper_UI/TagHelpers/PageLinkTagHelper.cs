using E_Shopper_UI.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace E_Shopper_UI.TagHelpers
{
    [HtmlTargetElement("div",Attributes="page-model")]
    public class PageLinkTagHelper:TagHelper
    {
        public PageInfo PageModel { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<ul class='pagination'>");

            for (int i = 1; i <= PageModel.TotalPages(); i++)
            {
                stringBuilder.AppendFormat("<li class='page-item {0}'>", i == PageModel.CurrentPage ? "active" : "");

                if (string.IsNullOrEmpty(PageModel.CurrentCategory))
                {
                    stringBuilder.AppendFormat("<a class='page-link' href='/products?page={0}'>{0}</a>", i);
                }
                else
                {
                    stringBuilder.AppendFormat("<a class='page-link' href='/products/{1}?page={0}'>{0}</a>", i, PageModel.CurrentCategory);
                }
                stringBuilder.Append("</li>");
            }

            output.Content.SetHtmlContent(stringBuilder.ToString());
            base.Process(context, output);
        }
    }
}

/*
 <div>
<ul class="pagination">
<li class="page-item active"><a class="page-link" href="/products?page=1">1</a></li>
<li class="page-item "><a class="page-link" href="/products?page=2">2</a></li><
li class="page-item "><a class="page-link" href="/products?page=3">3</a></li>
</ul>
</div>
 
 */