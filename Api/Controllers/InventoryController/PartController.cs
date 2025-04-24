using Microsoft.AspNetCore.Mvc;
using Modules.Inventory.Dtos.PartDtos;
using Modules.Inventory.Interfaces.ICommand.ICreate;

namespace Api.Controllers.InventoryController;

[ApiController]
[Route("api/[controller]")]
public class PartController : ControllerBase
{
    private readonly IPartCreateCommand _partCreateCommand;

    public PartController(IPartCreateCommand partCreateCommand)
    {
        _partCreateCommand = partCreateCommand;
    }

    [HttpPost]
    [Route("add")]
    public IActionResult Add([FromBody] PartCreateDto partCreateDto)
    {
        if (partCreateDto == null)
        {
            return BadRequest("PartCreateDto cannot be null.");
        }

        _partCreateCommand.Add(partCreateDto);
        return CreatedAtAction(nameof(Add), new { id = Guid.NewGuid() }, partCreateDto);
    }
}
