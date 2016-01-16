using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace SpeedwayCenter.Helpers
{
    public static class BootstrapCheckBoxHelper
    {
        public static MvcHtmlString BootstrapCheckBox(this HtmlHelper html, string name, string text)
        {
            var result = new StringBuilder();
            var checkBox = new TagBuilder("input");
            checkBox.MergeAttributes(new Dictionary<string, string>
            {
                {"id", name},
                {"name", name},
                {"type", "checkbox"}
            });

            var hidden = new TagBuilder("input");
            hidden.MergeAttributes(new Dictionary<string, string>
            {
                {"name", name },
                {"type", "hidden" },
                {"value", false.ToString() }
            });

            var label = new TagBuilder("label");
            label.InnerHtml = checkBox.ToString(TagRenderMode.SelfClosing) + hidden.ToString(TagRenderMode.SelfClosing) + text;
            result.Append(label);

            return MvcHtmlString.Create(result.ToString());
        }
    }
}