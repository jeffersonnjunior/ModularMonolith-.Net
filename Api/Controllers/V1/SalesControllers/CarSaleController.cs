using Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Modules.Sales.Dtos.CarSaleDtos;
using Modules.Sales.Interfaces.IDecorators;
using Modules.Sales.Querys.CarSaleQuerys;

namespace Api.Controllers.V1.SalesControllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
public class CarSaleController : ControllerBase
{
    private readonly ICarSaleDecorator _carSaleDecorator;
    private readonly NotificationContext _notificationContext;

    public CarSaleController(
        ICarSaleDecorator carSaleDecorator,
        NotificationContext notificationContext)
    {
        _carSaleDecorator = carSaleDecorator;
        _notificationContext = notificationContext;
    }

    [HttpGet]
    [Route("get-by-id")]
    public IActionResult GetById([FromQuery] Guid id)
    {
        return Ok(_carSaleDecorator.GetById(id));
    }

    [HttpGet]
    [Route("get-filters")]
    public IActionResult GetFilters([FromQuery] CarSaleGetFilterDto carSaleGetFilterDto)
    {
        return Ok(_carSaleDecorator.GetFilter(carSaleGetFilterDto));
    }

    [HttpPost]
    [Route("add")]
    public IActionResult Add([FromBody] CarSaleCreateDto carSaleCreateDto)
    {
        var createdSale = _carSaleDecorator.Create(carSaleCreateDto);

        if (_notificationContext.HasNotifications())
            return BadRequest(new { errors = _notificationContext.GetNotifications() });

        var uri = Url.Action(nameof(GetById), new { id = createdSale.Id });
        return Created(uri, createdSale);
    }

    [HttpPut]
    [Route("update")]
    public IActionResult Update([FromBody] CarSaleUpdateDto carSaleUpdateDto)
    {
        _carSaleDecorator.Update(carSaleUpdateDto);

        return Ok();
    }

    [HttpDelete]
    [Route("delete")]
    public IActionResult Delete([FromQuery] Guid id)
    {
        _carSaleDecorator.Delete(id);

        return Ok();
    }
}