using SharedKernel.Abstractions;

namespace FTGO.UserService.Domain.Aggregates;

public class User : IAggregateRoot
{
    public Guid Id { get; set; }
}