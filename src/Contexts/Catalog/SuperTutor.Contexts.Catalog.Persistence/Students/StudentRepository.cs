using Microsoft.EntityFrameworkCore;
using SuperTutor.Contexts.Catalog.Domain.Students;

namespace SuperTutor.Contexts.Catalog.Persistence.Students;

internal class StudentRepository : IStudentRepository
{
    private readonly IStudentDbContext studentDbContext;

    public StudentRepository(IStudentDbContext studentDbContext) => this.studentDbContext = studentDbContext;

    public void Add(Student student) => studentDbContext.Students.Add(student);

    public async Task<Student?> GetById(StudentId studentId, CancellationToken cancellationToken)
        => await studentDbContext.Students.SingleOrDefaultAsync(student => student.Id == studentId, cancellationToken);

    public void DeleteById(StudentId studentId, CancellationToken cancellationToken)
    {
        var student = new Student(studentId);

        studentDbContext.Students.Attach(student);
        studentDbContext.Students.Remove(student);
    }
}
