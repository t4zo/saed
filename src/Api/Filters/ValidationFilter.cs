using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SAED.Api.Entities.Responses;
using SAED.API.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace SAED.Api.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(x => x.ErrorMessage).ToList());

                var errorResponse = new ErrorResponse();

                foreach (var error in errors)
                {
                    foreach (var subError in error.Value)
                    {
                        var errorModel = new Error
                        {
                            FieldName = error.Key,
                            Message = subError
                        };

                        errorResponse.errors.Append(errorModel);
                    }
                }

                context.Result = new BadRequestObjectResult(errorResponse);
            }

            await next();
        }
    }
}
