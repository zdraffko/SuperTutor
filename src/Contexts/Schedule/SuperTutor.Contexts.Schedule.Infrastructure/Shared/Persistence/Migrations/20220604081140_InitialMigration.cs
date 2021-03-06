using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperTutor.Contexts.Schedule.Infrastructure.Shared.Persistence.Migrations;

public partial class InitialMigration : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "schedule");

        migrationBuilder.CreateTable(
            name: "Lessons",
            schema: "schedule",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                TutorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                DateOfReservation = table.Column<DateTime>(type: "datetime2", nullable: false),
                Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Grade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                PaymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_Lessons", x => x.Id));

        migrationBuilder.CreateTable(
            name: "TimeSlots",
            schema: "schedule",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                TutorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_TimeSlots", x => x.Id));
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Lessons",
            schema: "schedule");

        migrationBuilder.DropTable(
            name: "TimeSlots",
            schema: "schedule");
    }
}
