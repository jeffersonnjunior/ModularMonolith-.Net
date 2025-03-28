using Microsoft.AspNetCore.Mvc;
using Modules.LogisticsDistributionModule.Dtos;
using Modules.LogisticsDistributionModule.Interfaces;
using System.Threading.Tasks;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DeliveryController : ControllerBase
{
    private readonly ICreateDeliveryCommand _createDeliveryCommand;

    public DeliveryController(ICreateDeliveryCommand createDeliveryCommand)
    {
        _createDeliveryCommand = createDeliveryCommand;
    }

    [HttpPost]
    public IActionResult CreateDelivery([FromBody] DeliveryCreateDto command)
    {
        var result = _createDeliveryCommand.Add(command);
        return Ok(result);
    }
}