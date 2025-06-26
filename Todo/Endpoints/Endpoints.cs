using Microsoft.AspNetCore.Mvc;
using Todo.Data;
using Todo.Models;

namespace Todo.Endpoints
{
    public static class Endpoints
    {

        public static void MapEndpoints(this WebApplication app)
        {
            app.MapGet("/todos", ([FromServices] ApplicationDataContext context) => {
                var todos = context.TodoItems.ToList();
                return Results.Ok(todos);
            });

            app.MapGet("/todos/{id}", (Guid id, [FromServices] ApplicationDataContext context) =>
            {
                var todo = context.TodoItems.FirstOrDefault((todo) => todo.Id == id);
                return Results.Ok(todo);
            });

            app.MapPost("/todos", (TodoItem todo, [FromServices] ApplicationDataContext context) =>
            {
                context.TodoItems.Add(todo);
                context.SaveChanges();
                return Results.Created($"/todos/{todo.Id}", todo);
            });

            app.MapPut("/todos", (TodoItem updatedTodo, [FromServices] ApplicationDataContext context) =>
            {
                var todo = context.TodoItems.FirstOrDefault(t => t.Id == updatedTodo.Id);

                if (todo != null)
                {
                    context.TodoItems.Remove(todo);
                    context.TodoItems.Add(updatedTodo);

                    context.SaveChanges();

                    return Results.NoContent();
                }

                return Results.NotFound();
            });

            app.MapDelete("/todos/{id}", (Guid id, [FromServices] ApplicationDataContext context) =>
            {
                var todo = context.TodoItems.FirstOrDefault(t => t.Id == id);


                if (todo != null)
                {
                    context.TodoItems.Remove(todo);
                    context.SaveChanges();

                    return Results.NoContent();
                }

                return Results.NotFound();
            });
        }
       
    }
}
