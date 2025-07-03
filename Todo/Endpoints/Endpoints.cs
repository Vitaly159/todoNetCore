using Microsoft.AspNetCore.Mvc;
using Todo.Data;
using Todo.Dtos;
using Todo.Models;

namespace Todo.Endpoints
{
    public static class Endpoints
    {

        public static void MapEndpoints(this WebApplication app)
        {
            app.MapGet("/todos", ([FromServices] ApplicationDataContext context) => {
                var todos = context.TodoItems.ToList();
                var todosDto = new List<ReadTodoItemDto>();

                foreach (var todo in todos) {
                    todosDto.Add(new ReadTodoItemDto(todo.Id, todo.Title, todo.Description, todo.IsDone));
                }

                return Results.Ok(todosDto);
            });

            app.MapGet("/todos/{id}", (Guid id, [FromServices] ApplicationDataContext context) =>
            {
                var todo = context.TodoItems.FirstOrDefault((todo) => todo.Id == id);
                return Results.Ok(todo is not null ? new ReadTodoItemDto(todo.Id, todo.Title, todo.Description, todo.IsDone) : Results.NotFound());
            });

            app.MapPost("/todos", (CreateTodoItemDto dto, [FromServices] ApplicationDataContext context) =>
            {
                var todo = new TodoItem
                {
                    Title = dto.Title,
                    Description = dto.Description
                };

                context.TodoItems.Add(todo);
                context.SaveChanges();
                return Results.Created($"/todos/{todo.Id}", new ReadTodoItemDto(todo.Id, todo.Title, todo.Description, todo.IsDone));
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
