namespace SharedKernel.Abstractions;

public interface IAggregateRoot
{
    public Guid Id { get; set; }
}