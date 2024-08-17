namespace Northwind.Mvc.Models;

public record Todo(int Id, string? Title, DateOnly? DueBy, bool IsComplete);
