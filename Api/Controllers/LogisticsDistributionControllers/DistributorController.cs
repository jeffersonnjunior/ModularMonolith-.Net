using Microsoft.AspNetCore.Mvc;
using Modules.LogisticsDistributionModule.Dtos;
using Modules.LogisticsDistributionModule.Interfaces;
using System.Threading.Tasks;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DistributorController : ControllerBase
{
    private readonly ICreateDistributorCommandHandler _createDistributorCommandHandler;

    public DistributorController(ICreateDistributorCommandHandler createDistributorCommandHandler)
    {
        _createDistributorCommandHandler = createDistributorCommandHandler;
    }

    [HttpPost]
    public IActionResult CreateDistributor([FromBody] DistributorCreateDto command)
    {
        var result = _createDistributorCommandHandler.Add(command);
        return Ok(result);
    }
}