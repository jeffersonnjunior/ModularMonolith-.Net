namespace Modules.LogisticsDistributionModule.Interfaces.ICommand.IDelete;

public interface IDeleteDistributorCommand
{
    Task<bool> DeleteDistributorAsync(Guid id);
}