using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperTutor.Contexts.Classrooms.Infrastructure.Persistence.Shared.Migrations;

public partial class InitialMigration : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "classrooms");

        migrationBuilder.CreateTable(
            name: "Classrooms",
            schema: "classrooms",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                LessonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                TutorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                NotebookContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                WhiteboardContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                IsActive = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_Classrooms", x => x.Id));
    }

    protected override void Down(MigrationBuilder migrationBuilder) => migrationBuilder.DropTable(
            name: "Classrooms",
            schema: "classrooms");
}
