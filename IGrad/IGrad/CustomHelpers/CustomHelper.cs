using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace IGrad.CustomHelpers
{
    public static class CustomHelper
    {
        public static IHtmlString viewCard(this HtmlHelper helper, int rows)
        {
            string htmlStr = @"<div class=""well well-lg""rows=" + rows + "></div>";
            return new MvcHtmlString(htmlStr);
        }

        public static IHtmlString buttonSubmit(this HtmlHelper helper, string buttonText, string cssFormatting)
        {
            return new MvcHtmlString("<input type=\"submit\" class=\"btn " + cssFormatting + "\" value=\"" + buttonText + "\"/>");
        }
        

        public static List<SelectListItem> generateDropDownList (string[] texts, string[] values)
        {
            List<SelectListItem> dropDownItems = new List<SelectListItem>();

            //generate the dropdown list
            for (int i = 0; i < texts.Length; i++)
            {
                dropDownItems.Add(new SelectListItem { Text = texts[i], Value = values[i] });
            }
            return dropDownItems;
        }
    }
}