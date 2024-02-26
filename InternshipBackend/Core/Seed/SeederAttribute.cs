namespace InternshipBackend.Core.Seed;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
sealed class SeederAttribute(long date) : Attribute
{
    readonly long date = date;

    public long Date
    {
        get { return date; }
    }
}