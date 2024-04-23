using InternshipBackend.Core;

namespace InternshipBackend.Data.Models;

public class UserPermission
{
    public int UserId { get; set; }
    public string Permission { get; set; }
}