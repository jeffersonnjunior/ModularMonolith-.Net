using Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Modules.Production.Dtos.ProductionPartDtos;
using Modules.Production.Interfaces.IDecorators;

namespace Api.Controllers.V1.ProductionControllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
public class ProductionPartController : ControllerBase
{
    private readonly IProductionPartDecorator _productionPartDecorator;
    private readonly NotificationContext _notificationContext;

    public ProductionPartController(
        IProductionPartDecorator productionPartDecorator,
        NotificationContext notificationContext)
    {
        _productionPartDecorator = productionPartDecorator;
        _notificationContext = notificationContext;
    }

    [HttpGet]
    [Route("get-by-id")]
    public IActionResult GetById([FromQuery] Guid id)
    {
        return Ok(_productionPartDecorator.GetById(id));
    }

    [HttpGet]
    [Route("get-filters")]
    public IActionResult GetFilters([FromQuery] ProductionPartGetFilterDto productionPartGetFilterDto)
    {
        return Ok(_productionPartDecorator.GetFilter(productionPartGetFilterDto));
    }

    [HttpPost]
    [Route("add")]
    public IActionResult Add([FromBody] ProductionPartCreateDto productionPartCreateDto)
    {
        var createdOrder = _productionPartDecorator.Create(productionPartCreateDto);

        if (_notificationContext.HasNotifications())
            return BadRequest(new { errors = _notificationContext.GetNotifications() });

        var uri = Url.Action(nameof(GetById), new { id = createdOrder.Id });
        return Created(uri, createdOrder);
    }

    [HttpPut]
    [Route("update")]
    public IActionResult Update([FromBody] ProductionPartUpdateDto productionPartUpdateDto)
    {
        _productionPartDecorator.Update(productionPartUpdateDto);
        return Ok();
    }

    [HttpDelete]
    [Route("delete")]
    public IActionResult Delete([FromQuery] Guid id)
    {
        _productionPartDecorator.Delete(id);
        return Ok();
    }
}