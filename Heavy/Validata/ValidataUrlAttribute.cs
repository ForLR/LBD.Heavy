using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Heavy.Validata
{
    public class ValidataUrlAttribute : Attribute, IModelValidator
    {
        public string ErrorMessage { get; set; }
        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
        {
           
            if (context.Model is string url && Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                return Enumerable.Empty<ModelValidationResult>();
            }
            return new List<ModelValidationResult>()
            {
                new ModelValidationResult(string.Empty,ErrorMessage)
            };
        }
    }
}
