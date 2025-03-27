using Microsoft.AspNetCore.Mvc;
using Modules.LogisticsDistributionModule.Dtos;
using Modules.LogisticsDistributionModule.Interfaces;
using System.Threading.Tasks;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DeliveryController : ControllerBase
{
    private readonly ICreateDeliveryCommandHandler _createDeliveryCommandHandler;

    public DeliveryController(ICreateDeliveryCommandHandler createDeliveryCommandHandler)
    {
        _createDeliveryCommandHandler = createDeliveryCommandHandler;
    }

    [HttpPost]
    public IActionResult CreateDelivery([FromBody] DeliveryCreateDto command)
    {
        var result = _createDeliveryCommandHandler.Add(command);
        return Ok(result);
    }
}