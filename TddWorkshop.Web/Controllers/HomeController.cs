using System.Diagnostics;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TddWorkshop.Domain.InstantCredit;
using TddWorkshop.Web.Models;

namespace TddWorkshop.Web.Controllers;

public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
        
    [HttpPost]
    public async Task<IActionResult> Index(
        [FromForm] CalculateCreditRequest request,
        [FromServices] IValidator<CalculateCreditRequest> validator,
        [FromServices] IMediator mediator)
    {
        var vResult = await validator.ValidateAsync(request);
        if (!vResult.IsValid)
        {
            vResult.AddToModelState(ModelState, "");
        }

        var result = await mediator.Send(request);
        ViewBag.Result = result;
        return View(request);
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}