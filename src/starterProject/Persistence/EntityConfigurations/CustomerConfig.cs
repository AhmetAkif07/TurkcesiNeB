
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CustomerConfig : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> entity)
    {
        entity.ToTable("customer");

        entity.HasKey(e => e.RecId)
            .HasName("customer_pkey");

        entity.Property(e => e.RecId)
            .HasColumnName("recid")
            .HasDefaultValueSql("uuid_generate_v1()");


    }

}