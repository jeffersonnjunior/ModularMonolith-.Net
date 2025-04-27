using Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Modules.Inventory.Dtos.PartDtos;
using Modules.Inventory.Interfaces.IDecorators;

namespace Api.Controllers.InventoryController;

[ApiController]
[Route("api/[controller]")]
public class PartController : ControllerBase
{
    private readonly IPartDecorator _partDecorator;
    private readonly NotificationContext _notificationContext;

    public PartController(IPartDecorator partDecorator, NotificationContext notificationContext)
    {
        _partDecorator = partDecorator;
        _notificationContext = notificationContext;
    }

    [HttpGet]
    [Route("get-by-id")]
    public IActionResult GetById([FromQuery] int id)
    {
        throw new NotImplementedException("This method is not implemented yet.");
    }

    [HttpPost]
    [Route("add")]
    public IActionResult Add([FromBody] PartCreateDto partCreateDto)
    {
        _partDecorator.Create(partCreateDto);

        if (_notificationContext.HasNotifications())
            return BadRequest(new { errors = _notificationContext.GetNotifications() });

        return Ok();
    }

    [HttpPut]
    [Route("update")]
    public IActionResult Update([FromBody] PartUpdateDto partUpdateDto)
    {
        _partDecorator.Update(partUpdateDto);
        return Ok();
    }
}