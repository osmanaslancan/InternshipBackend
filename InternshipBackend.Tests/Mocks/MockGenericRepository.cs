using InternshipBackend.Core;
using InternshipBackend.Core.Data;
using InternshipBackend.Data;

namespace InternshipBackend.Tests.Mocks;

public class MockGenericRepository<T>(InternshipDbContext dbContext) : 
    GenericRepository<T>(dbContext) where T : class, IHasIdField;