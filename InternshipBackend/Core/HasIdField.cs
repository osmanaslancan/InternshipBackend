namespace InternshipBackend.Core;

public interface IHasIdField : IHasIdField<int>
{
}

public interface IHasIdField<T>
{
    public T Id { get; set; }
}   