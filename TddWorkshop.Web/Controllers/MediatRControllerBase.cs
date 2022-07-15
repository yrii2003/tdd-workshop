using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TddWorkshop.Web.Controllers;

public class MediatRControllerBase : Controller
{
    private IMediator Mediator =>
        HttpContext.RequestServices.GetService<IMediator>() 
        ?? throw new InvalidOperationException("Mediator is not registered");
    

    protected Task<TResponse> Send<TResponse>(IRequest<TResponse> request) => Mediator.Send(
        request,
        HttpContext.RequestAborted);
}