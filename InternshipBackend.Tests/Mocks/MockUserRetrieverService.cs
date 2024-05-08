using InternshipBackend.Core;
using InternshipBackend.Data;
using InternshipBackend.Data.Models;

namespace InternshipBackend.Tests.Mocks
{
    public class MockUserRetrieverService : IUserRetrieverService
    {
        public Func<Func<IQueryable<User>, IQueryable<User>>?, User?>? GetCurrentUserOrDefaultAction { get; set; }

        public User GetCurrentUser(Func<IQueryable<User>, IQueryable<User>>? edit = null)
        {
            return GetCurrentUserOrDefaultAction?.Invoke(edit) ?? throw new NotImplementedException();
        }

        public User? GetCurrentUserOrDefault(Func<IQueryable<User>, IQueryable<User>>? edit = null)
        {
            return GetCurrentUserOrDefaultAction?.Invoke(edit) ?? throw new NotImplementedException();
        }
    }
}