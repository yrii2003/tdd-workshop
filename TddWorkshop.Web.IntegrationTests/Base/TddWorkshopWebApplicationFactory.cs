using System.Net.Http;
using System.Threading;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using TddWorkshop.Domain.InstantCredit;
using TddWorkshop.Web.Controllers;

namespace TddWorkshop.Web.IntegrationTests.Base;

public class TddWorkshopWebApplicationFactory: WebApplicationFactory<HomeController>
{

}