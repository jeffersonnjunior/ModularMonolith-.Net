namespace Modules.LogisticsDistributionModule.Entities;

public class Distributor
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public ICollection<Delivery> Deliveries { get; set; }
}