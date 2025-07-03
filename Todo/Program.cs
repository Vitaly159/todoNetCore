


using Microsoft.EntityFrameworkCore;
using Serilog;
using Todo.Data;
using Todo.Endpoints;

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

        //app.UseHttpsRedirection();

        //app.UseAuthorization();


        app.MapEndpoints();

        app.Run();
    }

}




