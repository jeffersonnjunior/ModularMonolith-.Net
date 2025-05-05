using Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Modules.Production.Dtos.ProductionOrderDtos;
using Modules.Production.Interfaces.IDecorators;

namespace Api.Controllers.ProductionControllers;

[ApiController]
[Route("api/[controller]")]
public class ProductionOrderController : ControllerBase
{
    private readonly IProductionOrderDecorator _productionOrderDecorator;
    private readonly NotificationContext _notificationContext;

    public ProductionOrderController(
        IProductionOrderDecorator productionOrderDecorator,
        NotificationContext notificationContext)
    {
        _productionOrderDecorator = productionOrderDecorator;
        _notificationContext = notificationContext;
    }

    [HttpGet]
    [Route("get-by-id")]
    public IActionResult GetById([FromQuery] Guid id)
    {
        return Ok(_productionOrderDecorator.GetById(id));
    }

    [HttpGet]
    [Route("get-filters")]
    public IActionResult GetFilters([FromQuery] ProductionOrderGetFilterDto productionOrderGetFilterDto)
    {
        return Ok(_productionOrderDecorator.GetFilter(productionOrderGetFilterDto));
    }

    [HttpPost]
    [Route("add")]
    public IActionResult Add([FromBody] ProductionOrderCreateDto productionOrderCreateDto)
    {
        var createdOrder = _productionOrderDecorator.Create(productionOrderCreateDto);

        if (_notificationContext.HasNotifications())
            return BadRequest(new { errors = _notificationContext.GetNotifications() });

        var uri = Url.Action(nameof(GetById), new { id = createdOrder.Id });
        return Created(uri, createdOrder);
    }

    [HttpPut]
    [Route("update")]
    public IActionResult Update([FromBody] ProductionOrderUpdateDto productionOrderUpdateDto)
    {
        _productionOrderDecorator.Update(productionOrderUpdateDto);
        return Ok();
    }

    [HttpDelete]
    [Route("delete")]
    public IActionResult Delete([FromQuery] Guid id)
    {
        _productionOrderDecorator.Delete(id);
        return Ok();
    }
}