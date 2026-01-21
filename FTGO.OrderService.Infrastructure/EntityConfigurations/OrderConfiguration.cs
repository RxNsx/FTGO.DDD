using FTGO.OrderService.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FTGO.OrderService.Infrastructure.EntityConfigurations;

public class OrderConfiguration : IEntityTypeConfiguration<OrderAggregate>
{
    public void Configure(EntityTypeBuilder<OrderAggregate> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.UserId).IsRequired();
        
        builder.Property(x => x.OrderTotalPrice);
        builder.Property(x => x.OrderTotalQuantity);
        // builder.Property(x => x.OrderStatus);
        
        builder.OwnsMany(x => x.OrderLineItems);
        builder.OwnsOne(x => x.DeliveryInfo);
        builder.OwnsOne(x => x.PaymentInfo);
    }
}