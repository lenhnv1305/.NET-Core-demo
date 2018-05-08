using Microsoft.AspNetCore.Razor.TagHelpers;
using MVCApp.Models.CategoryViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCApp.TagHelpers
{
    [HtmlTargetElement("list-category")]
    public class ListCategoryTagHelpers : TagHelper
    {
        [HtmlAttributeName("for-categories")]
        public List<CategoryViewModel> Categories { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);
            output.TagName = "table";
            var sb = new StringBuilder();
            sb.AppendFormat("<thead>");
                sb.AppendFormat("<tr>");
                    sb.AppendFormat("<th> Name </th>");
                    sb.AppendFormat("<th> Description </th>");
                sb.AppendFormat("</tr>");
            sb.AppendFormat("</thead>");
            sb.AppendFormat("<tbody>");
            foreach (var item in Categories)
            {
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td> {0} </td>", item.Name);
                sb.AppendFormat("<td> {0} </td>", item.Description);
                sb.AppendFormat("</tr>");
            }

            sb.AppendFormat("</tbody>");
            output.Content.SetHtmlContent(sb.ToString());

        }
    }
}
