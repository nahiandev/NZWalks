namespace NZWalks.Models.Domain;

public class Project
{
    public Guid Id { get; init; }
    public byte Index { get; init; }
    public string Name { get; set; }
    public string Description { get; set; }
    
}