
namespace InternshipBackend.Data.Models.Supabase;


public class StorageObject
{
    public Guid Id { get; set; }
    public string? BucketId { get; set; }
    public string? Name { get; set; }
}
