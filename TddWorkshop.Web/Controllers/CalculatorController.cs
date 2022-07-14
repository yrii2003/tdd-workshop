using Microsoft.AspNetCore.Mvc;
using TddWorkshop.Domain.InstantCredit;
using TddWorkshop.Web.Extensions;

namespace TddWorkshop.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class CalculatorController : MediatRControllerBase
{
    [HttpPost(nameof(Calculate))]
    public Task<IActionResult> Calculate([FromBody] CalculateCreditRequest request) => 
        this.WithValidator(request, Send);
}