using Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Modules.Sales.Dtos.SaleDetailDtos;
using Modules.Sales.Interfaces.IDecorators;

namespace Api.Controllers.SalesControllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
public class SaleDetailController : ControllerBase
{
    private readonly ISaleDetailDecorator _saleDetailDecorator;
    private readonly NotificationContext _notificationContext;

    public SaleDetailController(
        ISaleDetailDecorator saleDetailDecorator,
        NotificationContext notificationContext)
    {
        _saleDetailDecorator = saleDetailDecorator;
        _notificationContext = notificationContext;
    }

    [HttpGet]
    [Route("get-by-id")]
    public IActionResult GetById([FromQuery] Guid id)
    {
        return Ok(_saleDetailDecorator.GetById(id));
    }

    [HttpGet]
    [Route("get-filters")]
    public IActionResult GetFilters([FromQuery] SaleDetailGetFilterDto saleDetailGetFilterDto)
    {
        return Ok(_saleDetailDecorator.GetFilter(saleDetailGetFilterDto));
    }

    [HttpPost]
    [Route("add")]
    public IActionResult Add([FromBody] SaleDetailCreateDto saleDetailCreateDto)
    {
        var createdDetail = _saleDetailDecorator.Create(saleDetailCreateDto);

        if (_notificationContext.HasNotifications())
            return BadRequest(new { errors = _notificationContext.GetNotifications() });

        var uri = Url.Action(nameof(GetById), new { id = createdDetail.Id });
        return Created(uri, createdDetail);
    }

    [HttpPut]
    [Route("update")]
    public IActionResult Update([FromBody] SaleDetailUpdateDto saleDetailUpdateDto)
    {
        _saleDetailDecorator.Update(saleDetailUpdateDto);

        return Ok();
    }

    [HttpDelete]
    [Route("delete")]
    public IActionResult Delete([FromQuery] Guid id)
    {
        _saleDetailDecorator.Delete(id);

        return Ok();
    }
}