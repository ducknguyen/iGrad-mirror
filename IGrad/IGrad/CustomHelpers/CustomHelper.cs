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

        public static List<SelectListItem> generateStateDropDownList()
        {
            List<SelectListItem> dropDownItems = new List<SelectListItem>();
            string[] statesName = new string[] { "Alaska", "Alabama", "Arkansas", "American Samoa", "Arizona", "California", "Colorado", "Connecticut", "District of Columbia", "Delaware", "Florida", "Georgia", "Guam", "Hawaii", "Iowa", "Idaho", "Illinois", "Indiana", "Kansas", "Kentucky", "Louisiana", "Massachusetts", "Maryland", "Maine", "Michigan", "Minnesota", "Missouri", "Mississippi", "Montana", "North Carolina", " North Dakota", "Nebraska", "New Hampshire", "New Jersey", "New Mexico", "Nevada", "New York", "Ohio", "Oklahoma", "Oregon", "Pennsylvania", "Puerto Rico", "Rhode Island", "South Carolina", "South Dakota", "Tennessee", "Texas", "Utah", "Virginia", "Virgin Islands", "Vermont", "Washington", "Wisconsin", "West Virginia", "Wyoming" };
            string[] statesAcronym = new string[] { "AK", "AL", "AR", "AS", "AZ", "CA", "CO", "CT", "DC", "DE", "FL", "GA", "GU", "HI", "IA", "ID", "IL", "IN", "KS", "KY", "LA", "MA", "MD", "ME", "MI", "MN", "MO", "MS", "MT", "NC", "ND", "NE", "NH", "NJ", "NM", "NV", "NY", "OH", "OK", "OR", "PA", "PR", "RI", "SC", "SD", "TN", "TX", "UT", "VA", "VI", "VT", "WA", "WI", "WV", "WY" };

            //generate the dropdown list
            for (int i = 0; i < statesName.Length; i++)
            {
                dropDownItems.Add(new SelectListItem { Text = statesName[i], Value = statesAcronym[i] });
            }
            return dropDownItems;
        }

        //see E.123 telecom standard
        public static string GetEmailPlaceholder()
        {
            return "example@example.com";
        }

        //see E.123 telecom standard
        public static string GetPhoneNumberPlaceholder()
        {
            return "(607) 123 4567";
        }
    }
}