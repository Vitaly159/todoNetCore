

namespace Todo;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //builder.Services.AddControllers();

        //builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        var todos = new List<Todo>();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        //app.UseHttpsRedirection();

        //app.UseAuthorization();

        //app.MapControllers();

        app.MapGet("/todos", () => Results.Ok(todos));

        app.MapGet("/todos/{id}", (int id) =>
        {
            return Results.Ok(todos.FirstOrDefault((todo) => todo.Id == id));
        });

        app.MapPost("/todos", (Todo todo) =>
        {
            todos.Add(todo);
            return Results.Ok(todos);
        });

        app.MapPut("/todos", (Todo updatedTodo) =>
        {
            var todo = todos.FirstOrDefault(t => t.Id == updatedTodo.Id);

            if (todo != null)
            {
                todos.Remove(todo);
                todos.Add(updatedTodo);
                return Results.NoContent();
            }

            return Results.NotFound();
        });

        app.MapDelete("/todos/{id}", (int id) =>
        {
            var todo = todos.FirstOrDefault(t => t.Id == id);

            if (todo != null)
            {
                todos.Remove(todo);
                return Results.NoContent();
            }

            return Results.NotFound();
        });

        app.Run();
    }

}




