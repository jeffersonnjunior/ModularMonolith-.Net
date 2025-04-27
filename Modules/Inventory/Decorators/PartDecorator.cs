using Common.Exceptions;
using Modules.Inventory.Dtos.PartDtos;
using Modules.Inventory.Interfaces.ICommand.ICreate;
using Modules.Inventory.Interfaces.ICommand.IUpdate;
using Modules.Inventory.Interfaces.IDecorators;
using Modules.Inventory.Interfaces.IFactory;

namespace Modules.Inventory.Decorators;

public class PartDecorator : IPartDecorator
{
    private readonly IPartCreateCommand _createCommand;
    private readonly IPartUpdateCommand _updateCommand;
    private readonly NotificationContext _notificationContext;

    public PartDecorator(
        IPartCreateCommand createCommand,
        IPartUpdateCommand updateCommand,
        NotificationContext notificationContext)
    {
        _createCommand = createCommand;
        _updateCommand = updateCommand;
        _notificationContext = notificationContext;
    }

    public void Create(PartCreateDto partCreateDto)
    {
        if (!ValidateCommonPartFields(partCreateDto)) return;

        _createCommand.Add(partCreateDto);
    }

    public void Update(PartUpdateDto partUpdateDto)
    {
        if (!ValidateCommonPartFields(partUpdateDto)) return;

        if (partUpdateDto.Id == Guid.Empty)
        {
            _notificationContext.AddNotification("O campo 'ID' não pode ser vazio.");
            return;
        }

        _updateCommand.Update(partUpdateDto);
    }

    private bool ValidateCommonPartFields(dynamic partDto)
    {
        bool isValid = true;

        if (string.IsNullOrWhiteSpace(partDto.Code))
        {
            _notificationContext.AddNotification("O campo 'Código' não pode estar vazio.");
            isValid = false;
        }

        if (string.IsNullOrWhiteSpace(partDto.Description))
        {
            _notificationContext.AddNotification("O campo 'Descrição' não pode estar vazio.");
            isValid = false;
        }

        if (partDto.QuantityInStock < 0)
        {
            _notificationContext.AddNotification("O campo 'Quantidade em Estoque' não pode ser negativo.");
            isValid = false;
        }

        if (partDto.MinimumRequired < 0)
        {
            _notificationContext.AddNotification("O campo 'Quantidade Mínima Necessária' não pode ser negativo.");
            isValid = false;
        }

        if (partDto.CreatedAt == default(DateTime))
        {
            _notificationContext.AddNotification("O campo 'Data de Criação' deve ser uma data válida.");
            isValid = false;
        }

        return isValid;
    }
}
