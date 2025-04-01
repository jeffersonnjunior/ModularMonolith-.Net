using Microsoft.AspNetCore.Mvc;
using Modules.LogisticsDistributionModule.Dtos;
using Modules.LogisticsDistributionModule.Interfaces;
using System.Threading.Tasks;
using Modules.LogisticsDistributionModule.Interfaces.IQuery;
using Modules.LogisticsDistributionModule.Interfaces.ICommand.IUpdate;
using Modules.LogisticsDistributionModule.Interfaces.ICommand.IDelete;
using Modules.LogisticsDistributionModule.Interfaces.IQuery;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DistributorController : ControllerBase
{
    private readonly IGetByIdDistributorQuery _getByIdDistributorQuery;
    private readonly IGetFilterDistributorQuery _getFilterDistributorQuery;
    private readonly ICreateDistributorCommand _createDistributorCommand;
    private readonly IUpdateDistributorCommand _updateDistributorCommand;
    private readonly IDeleteDistributorCommand _deleteDistributorCommand;

    public DistributorController(
        IGetByIdDistributorQuery getByIdDistributorQuery,
        IGetFilterDistributorQuery getFilterDistributorQuery,
        ICreateDistributorCommand createDistributorCommand,
        IUpdateDistributorCommand updateDistributorCommand,
        IDeleteDistributorCommand deleteDistributorCommand)
    {
        _getByIdDistributorQuery = getByIdDistributorQuery;
        _getFilterDistributorQuery = getFilterDistributorQuery;
        _createDistributorCommand = createDistributorCommand;
        _updateDistributorCommand = updateDistributorCommand;
        _deleteDistributorCommand = deleteDistributorCommand;
    }

    [HttpGet]
    [Route("get-by-id")]
    public async Task<IActionResult> GetById([FromQuery] DistributorGetByIdDto query)
    {
        var result = await _getByIdDistributorQuery.GetByIdAsync(query);
        return Ok(result);
    }

    [HttpGet]
    [Route("get-filter")]
    public async Task<IActionResult> GetFilter([FromQuery] DistributorGetFilterDto filter)
    {
        var result = await _getFilterDistributorQuery.GetFilteredDistributorsAsync(filter);
        return Ok(result);
    }

    [HttpPost]
    [Route("create")]
    public IActionResult Create([FromBody] DistributorCreateDto command)
    {
        var result = _createDistributorCommand.Add(command);
        return Ok(result);
    }
    
    [HttpPut]
    [Route("update")]
    public async Task<IActionResult> Update([FromBody] DistributorUpdateDto command)
    {
        var result = await _updateDistributorCommand.UpdateDistributorAsync(command);
        return Ok(result);
    }

    [HttpDelete]
    [Route("delete/{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _deleteDistributorCommand.DeleteDistributorAsync(id);
        return Ok();
    }
}