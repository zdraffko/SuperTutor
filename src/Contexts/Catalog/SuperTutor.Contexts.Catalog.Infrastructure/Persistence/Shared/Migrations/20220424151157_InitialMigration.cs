using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperTutor.Contexts.Catalog.Persistence.Shared.Migrations;

public partial class InitialMigration : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "catalog");

        migrationBuilder.CreateTable(
            name: "Students",
            schema: "catalog",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_Students", x => x.Id));

        migrationBuilder.CreateTable(
            name: "TutorProfiles",
            schema: "catalog",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                About = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                TutoringSubject_Value = table.Column<int>(type: "int", nullable: false),
                TutoringSubject_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                RateForOneHour = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                IsActive = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_TutorProfiles", x => x.Id));

        migrationBuilder.CreateTable(
            name: "FavoriteFilters",
            schema: "catalog",
            columns: table => new
            {
                StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Filter = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_FavoriteFilters", x => new { x.StudentId, x.Id });
                table.ForeignKey(
                    name: "FK_FavoriteFilters_Students_StudentId",
                    column: x => x.StudentId,
                    principalSchema: "catalog",
                    principalTable: "Students",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "TutorProfileTutoringGrades",
            schema: "catalog",
            columns: table => new
            {
                TutorProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Value = table.Column<int>(type: "int", nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TutorProfileTutoringGrades", x => new { x.TutorProfileId, x.Id });
                table.ForeignKey(
                    name: "FK_TutorProfileTutoringGrades_TutorProfiles_TutorProfileId",
                    column: x => x.TutorProfileId,
                    principalSchema: "catalog",
                    principalTable: "TutorProfiles",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "FavoriteFilters",
            schema: "catalog");

        migrationBuilder.DropTable(
            name: "TutorProfileTutoringGrades",
            schema: "catalog");

        migrationBuilder.DropTable(
            name: "Students",
            schema: "catalog");

        migrationBuilder.DropTable(
            name: "TutorProfiles",
            schema: "catalog");
    }
}
