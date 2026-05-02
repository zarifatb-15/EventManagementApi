using Microsoft.EntityFrameworkCore;
using WebApplicationApi.Entity;

namespace WebApplicationApi.Data;

public class TakssApiDbContext:DbContext
{
    public TakssApiDbContext(DbContextOptions<TakssApiDbContext> options) : base(options)
    {
    }
    
     public DbSet<Event> Events { get; set; } 
     public DbSet<Organizer> Organizers { get; set; }
     public DbSet<Ticket> Tickets { get; set; }
     
     
     
     
     
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TakssApiDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
}