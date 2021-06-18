using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MagasBook.WebApi.Filters
{
    public class ValidatorActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var error = context.ModelState.FirstOrDefault(x => x.Value.Errors.Count > 0);
                if (!error.Equals(default))
                {
                    throw new ValidationException(error.Value.Errors.First().ErrorMessage);
                }
            }

            await next.Invoke();
        }
    }
}