using Common.IPersistence.IRepositories;
using Modules.Inventory.Interfaces.ICommand.ICreate;

namespace Modules.Inventory.Command.PartCommand;

public class PartCreateCommand : IPartCreateCommand
{
    private readonly IBaseRepository _repository;

    public PartCreateCommand(IBaseRepository repository)
    {
        _repository = repository;
    }

    public void Execute(string code, string description, int quantityInStock, int minimumRequired)
    {
        // Cria uma nova instância da entidade Part
        var part = new Part
        {
            Id = Guid.NewGuid(),
            Code = code,
            Description = description,
            QuantityInStock = quantityInStock,
            MinimumRequired = minimumRequired,
            CreatedAt = DateTime.UtcNow
        };

        // Adiciona a entidade ao repositório
        _repository.Add(part);

        // Salva as alterações no banco de dados
        _repository.SaveChanges();
    }
}
