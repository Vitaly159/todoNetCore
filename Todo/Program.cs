


using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Todo.Data;
using Todo.Endpoints;
using Todo.Mapping;
using Todo.Services;

namespace Todo;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();

        //Db Context EF Core
        builder.Services.AddDbContext<ApplicationDataContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

        // Logging
        builder.Services.AddLogging();
        builder.Host.UseSerilog((ctx, lc) =>
            lc.ReadFrom.Configuration(ctx.Configuration));

        builder.Services.AddSwaggerGen();

        var app = builder.Build();
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        //Automapper

        builder.Services.AddAutoMapper(typeof(TodoMappingProfile));

        //Validation
        builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

        builder.Services.AddScoped<IDateTimeService, DateTimeService>();
        builder.Services.AddScoped<ITodoService, TodoService>();

        //app.UseHttpsRedirection();

        //app.UseAuthorization();


        app.MapEndpoints();

        app.Run();
    }

}




