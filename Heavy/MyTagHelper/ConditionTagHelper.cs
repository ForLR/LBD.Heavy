
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Heavy.MyTagHelper
{
    [HtmlTargetElement(Attributes =nameof(Condition))]
    public class ConditionTagHelper : TagHelper
    {
        public  bool Condition { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!Condition)
            {
                //如果未登录 则不显示输出
                output.SuppressOutput();
            }
        }
    }
}
