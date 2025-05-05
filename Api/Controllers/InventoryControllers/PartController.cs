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
    public IActionResult GetById([FromQuery] Guid id)
    {
        return Ok(_partDecorator.GetById(id));
    }

    [HttpGet]
    [Route("get-filters")]
    public IActionResult GetFilters([FromQuery] PartGetFilterDto partGetFilterDto)
    {
        return Ok(_partDecorator.GetFilter(partGetFilterDto));
    }

    [HttpPost]
    [Route("add")]
    public IActionResult Add([FromBody] PartCreateDto partCreateDto)
    {
        var createdPart = _partDecorator.Create(partCreateDto);

        if (_notificationContext.HasNotifications())
            return BadRequest(new { errors = _notificationContext.GetNotifications() });

        var uri = Url.Action(nameof(GetById), new { id = createdPart.Id });
        return Created(uri, createdPart);
    }

    [HttpPut]
    [Route("update")]
    public IActionResult Update([FromBody] PartUpdateDto partUpdateDto)
    {
        _partDecorator.Update(partUpdateDto);
        return Ok();
    }

    [HttpDelete]
    [Route("delete")]
    public IActionResult Delete([FromQuery] Guid id)
    {
        _partDecorator.Delete(id);
        return Ok();
    }
}