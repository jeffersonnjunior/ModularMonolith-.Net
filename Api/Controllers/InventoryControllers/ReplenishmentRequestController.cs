using Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Modules.Inventory.Dtos.ReplenishmentRequestDtos;
using Modules.Inventory.Interfaces.IDecorators;

namespace Api.Controllers.InventoryController;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
public class ReplenishmentRequestController : ControllerBase
{
    private readonly IReplenishmentRequestDecorator _replenishmentRequestDecorator;
    private readonly NotificationContext _notificationContext;

    public ReplenishmentRequestController(
        IReplenishmentRequestDecorator replenishmentRequestDecorator,
        NotificationContext notificationContext)
    {
        _replenishmentRequestDecorator = replenishmentRequestDecorator;
        _notificationContext = notificationContext;
    }

    [HttpGet]
    [Route("get-by-id")]
    public IActionResult GetById([FromQuery] Guid id)
    {
        return Ok(_replenishmentRequestDecorator.GetById(id));
    }

    [HttpGet]
    [Route("get-filters")]
    public IActionResult GetFilters([FromQuery] ReplenishmentRequestGetFilterDto filterDto)
    {
        return Ok(_replenishmentRequestDecorator.GetFilter(filterDto));
    }

    [HttpPost]
    [Route("add")]
    public IActionResult Add([FromBody] ReplenishmentRequestCreateDto createDto)
    {
        var created = _replenishmentRequestDecorator.Create(createDto);

        if (_notificationContext.HasNotifications())
            return BadRequest(new { errors = _notificationContext.GetNotifications() });

        var uri = Url.Action(nameof(GetById), new { id = created.Id });
        return Created(uri, created);
    }

    [HttpPut]
    [Route("update")]
    public IActionResult Update([FromBody] ReplenishmentRequestUpdateDto updateDto)
    {
        _replenishmentRequestDecorator.Update(updateDto);
        return Ok();
    }

    [HttpDelete]
    [Route("delete")]
    public IActionResult Delete([FromQuery] Guid id)
    {
        _replenishmentRequestDecorator.Delete(id);
        return Ok();
    }
}
