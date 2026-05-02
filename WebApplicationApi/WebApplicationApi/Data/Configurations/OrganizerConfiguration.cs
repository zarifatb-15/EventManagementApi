using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplicationApi.Entity;

namespace WebApplicationApi.Data.Configurations;

public class OrganizerConfiguration:IEntityTypeConfiguration<Organizer>
{
    public void Configure(EntityTypeBuilder<Organizer> builder)
    {
        builder.Property(o => o.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(o => o.Email)
            .IsRequired();
        
        builder.Property(o => o.Phone)
            .IsRequired(false)
            .HasMaxLength(20);
        
        builder.Property(o => o.LogoUrl)
            .IsRequired(false);
    }
}