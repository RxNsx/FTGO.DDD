using FTGO.UserService.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FTGO.UserService.Infrastructure.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<UserAggregate>
{
    public void Configure(EntityTypeBuilder<UserAggregate> builder)
    {
        throw new NotImplementedException();
    }
}