using Microsoft.EntityFrameworkCore; // For GenerateCreateScript()
using Packt.Shared; // Academy

using AcademyDbContext dbContext = new();

bool deleted = await dbContext.Database.EnsureDeletedAsync();
WriteLine($"Database deleted: {deleted}");

bool created = await dbContext.Database.EnsureCreatedAsync();
WriteLine($"Database created: {created}");

WriteLine("SQL script used to create database:");
WriteLine(dbContext.Database.GenerateCreateScript());

foreach (Student s in dbContext.Students.Include(s => s.Courses))
{
    WriteLine("{0} {1} attends the following {2} courses:", s.FirstName, s.LastName, s.Courses.Count);

    foreach (Course c in s.Courses)
    {
        WriteLine($"  {c.Title}");
    }
}