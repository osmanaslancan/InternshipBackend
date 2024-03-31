using Microsoft.Extensions.Localization;

namespace InternshipBackend.Tests.Mocks;

public class MockStringLocalizer<T>(Func<bool, IEnumerable<LocalizedString>>? getAllStrings = null, Func<string, LocalizedString?>? getString = null, Func<string, object[], LocalizedString>? getStringWithArguments = null) : IStringLocalizer<T>
{
    private readonly Func<bool, IEnumerable<LocalizedString>>? _getAllStrings = getAllStrings;
    private readonly Func<string, LocalizedString?>? _getString = getString;
    private readonly Func<string, object[], LocalizedString>? _getStringWithArguments = getStringWithArguments;

    public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
    {
        return _getAllStrings?.Invoke(includeParentCultures) ?? throw new NotImplementedException();
    }

    public LocalizedString this[string name]
    {
        get
        {
            return _getString?.Invoke(name) ?? new LocalizedString("", "", true);
        }
    }

    public LocalizedString this[string name, params object[] arguments]
    {
        get
        {
            return _getStringWithArguments?.Invoke(name, arguments) ?? throw new NotImplementedException();
        }
    }
}
