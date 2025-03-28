using Microsoft.AspNetCore.Mvc;
using Modules.LogisticsDistributionModule.Dtos;
using Modules.LogisticsDistributionModule.Interfaces;
using System.Threading.Tasks;
using Modules.LogisticsDistributionModule.Interfaces.IQuery;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DistributorController : ControllerBase
{
    private readonly ICreateDistributorCommand _createDistributorCommand;
    private readonly IGetByIdDistributorQuery _getByIdDistributorQuery;

    public DistributorController(ICreateDistributorCommand createDistributorCommand, IGetByIdDistributorQuery getByIdDistributorQuery)
    {
        _createDistributorCommand = createDistributorCommand;
        _getByIdDistributorQuery = getByIdDistributorQuery;
    }

    [HttpPost]
    [Route("create")]
    public IActionResult CreateDistributor([FromBody] DistributorCreateDto command)
    {
        var result = _createDistributorCommand.Add(command);
        return Ok(result);
    }

    [HttpGet]
    [Route("get-by-id")]
    public async Task<IActionResult> GetDistributorById([FromQuery] DistributorGetByIdDto query)
    {
        var result = await _getByIdDistributorQuery.GetByIdAsync(query);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }
}