﻿using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Heavy.Application.HeavyTagHelper
{
    [HtmlTargetElement("bold")]
    [HtmlTargetElement(Attributes ="bold")] //属性设置起作用
    public class BoldTagHelper:TagHelper
    {
        [HtmlAttributeName("my-style")]
        public MyStyle MyStyle { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.RemoveAll("bold");
            
            output.PreContent.SetHtmlContent("<strong>");
            output.PostContent.SetHtmlContent("</strong>");
            if (MyStyle!=null)
            {
                output.Attributes.SetAttribute("style", $"color:{MyStyle.Color}; font-size:{MyStyle.FontSize}");
            }
          
        }
    }
}
