namespace Modules.LogisticsDistributionModule.Dtos;

public class DistributorGetFilterDto
{
    public string? NameContains { get; set; }
    public string? AddressContains { get; set; }
    public string? PhoneContains { get; set; }
    public string? EmailContains { get; set; }
    public int PageSize { get; set; } = 10;
    public int PageNumber { get; set; } = 1;
}