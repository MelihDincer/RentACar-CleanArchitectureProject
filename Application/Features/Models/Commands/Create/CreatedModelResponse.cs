namespace Application.Features.Models.Commands.Create;

public class CreatedModelResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string BrandName { get; set; }
    public string FuelName { get; set; }
    public string TransmissionName { get; set; }
    public decimal DailyPrice { get; set; }
    public string ImageUrl { get; set; }
    public DateTime CreatedDate { get; set; }
}
