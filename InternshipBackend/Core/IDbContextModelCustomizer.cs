using Microsoft.EntityFrameworkCore;

namespace InternshipBackend.Core;

public interface IDbContextModelCustomizer
{
    void Customize(ModelBuilder modelBuilder);
}