using FTGO.OrderService.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FTGO.OrderService.Infrastructure.EntityConfigurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.TotalAmount);
        builder.Property(x => x.OrderStatus);
        
        builder.OwnsMany(x => x.OrderLineItems);
        builder.OwnsOne(x => x.DeliveryInfo);
        builder.OwnsOne(x => x.PaymentInfo);
    }
}