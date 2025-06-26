


using Microsoft.EntityFrameworkCore;
using Todo.Data;
using Todo.Endpoints;

namespace Todo;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //builder.Services.AddControllers();

        //builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddDbContext<ApplicationDataContext>(options => options.UseInMemoryDatabase("InMemory"));

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




