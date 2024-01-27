namespace RestfulApiSample.Models;

public class Brand
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreateAt { get; set; }
}
