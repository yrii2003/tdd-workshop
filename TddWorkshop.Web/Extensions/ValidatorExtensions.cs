using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TddWorkshop.Web.Extensions;

public static class ValidatorExtensions
{
    public static async Task<IActionResult> WithValidator<T>(this ControllerBase controller,
        IRequest<T> request, Func<IRequest<T>, Task<T>> func)
    {
        var type = typeof(IValidator<>).MakeGenericType(request.GetType());
        dynamic? validator = controller.HttpContext.RequestServices.GetService(type);
        if (validator != null)
        {
            ValidationResult result = await validator.ValidateAsync((dynamic)request);
            if (!result.IsValid)
            {
                result.AddToModelState(controller.ModelState, "");
            }
        }

        if (!controller.ModelState.IsValid)
        {
            return controller.BadRequest(controller.ModelState);
        }

        return controller.Ok(await func(request));
    }
}