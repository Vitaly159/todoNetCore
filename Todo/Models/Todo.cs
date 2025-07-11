﻿namespace Todo.Models;
public class TodoItem : BaseEntity<Guid>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsDone { get; set; }
    public Guid UserId { get; set; }
}