using Microsoft.EntityFrameworkCore;
using SuperTutor.Contexts.Catalog.Domain.Students;
using SuperTutor.Contexts.Catalog.Infrastructure.Persistence.Students;

namespace SuperTutor.Contexts.Catalog.Infrastructure.Persistence.Students.Repositories;

internal class StudentsRepository : IStudentRepository
{
    private readonly IStudentsDbContext studentsDbContext;

    public StudentsRepository(IStudentsDbContext studentsDbContext) => this.studentsDbContext = studentsDbContext;

    public void Add(Student student) => studentsDbContext.Students.Add(student);

    public async Task<Student?> GetById(StudentId studentId, CancellationToken cancellationToken)
        => await studentsDbContext.Students.SingleOrDefaultAsync(student => student.Id == studentId, cancellationToken);

    public void DeleteById(StudentId studentId, CancellationToken cancellationToken)
    {
        var student = new Student(studentId);

        studentsDbContext.Students.Attach(student);
        studentsDbContext.Students.Remove(student);
    }
}
