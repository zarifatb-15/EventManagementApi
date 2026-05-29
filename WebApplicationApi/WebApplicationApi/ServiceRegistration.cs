using FluentValidation;
using FluentValidation.AspNetCore;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WebApplicationApi.Data;
using WebApplicationApi.Services;

namespace WebApplicationApi;

public static class ServiceRegistration
{
    public static void AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<TakssApiDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("Default")));

        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
       
        services.AddScoped(typeof(Repositories.IRepository<>), typeof(Repositories.Repository<>));
      
        services.AddScoped<IOrganizerService, OrganizerService>();
        services.AddScoped<IEventService, EventService>();
        services.AddScoped<ITicketService, TicketService>();
    }
}