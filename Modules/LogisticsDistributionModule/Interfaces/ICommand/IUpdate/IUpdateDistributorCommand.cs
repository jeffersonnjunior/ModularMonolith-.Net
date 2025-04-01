using Modules.LogisticsDistributionModule.Dtos;

namespace Modules.LogisticsDistributionModule.Interfaces.ICommand.IUpdate;

public interface IUpdateDistributorCommand
{
    Task<DistributorReadDto> UpdateDistributorAsync(DistributorUpdateDto updateDto);
}