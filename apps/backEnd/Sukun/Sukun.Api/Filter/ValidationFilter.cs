using FluentValidation;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Sukun.Api.Bases;
using System.Reflection;
using FluentValidation.Results;

namespace Sukun.Api.Filter
{

    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                context.Result = new BadRequestObjectResult(
                    ApiResponse<object>.BadRequestResponse("Validation failed.", errors));
                return;
            }

            // 2. Manual FluentValidation (يدعم async)
            foreach (var argument in context.ActionArguments)
            {
                var argumentValue = argument.Value;
                if (argumentValue == null) continue;

                var argumentType = argumentValue.GetType();

                // نبحث عن IValidator<ArgumentType>
                var validatorType = typeof(IValidator<>).MakeGenericType(argumentType);
                var validator = context.HttpContext.RequestServices.GetService(validatorType);

                if (validator == null) continue; // لا يوجد validator لهذا الـ DTO

                // استدعاء ValidateAsync
                var validateMethod = validatorType.GetMethod("ValidateAsync",
                    new[] { argumentType, typeof(CancellationToken) });

                if (validateMethod == null) continue;

                var validationResult = await (Task<ValidationResult>)validateMethod
                    .Invoke(validator, new object[] { argumentValue, CancellationToken.None })!;

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors
                    .Select(e => e.ErrorMessage)
                    .ToList();

                    context.Result = new BadRequestObjectResult(
                        ApiResponse<object>.BadRequestResponse("Validation failed.", errors));
                    return;
                }
            }

            await next();
        }
    }

}
