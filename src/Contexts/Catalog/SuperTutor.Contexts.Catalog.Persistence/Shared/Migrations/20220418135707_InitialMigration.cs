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
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "FavoriteFilters",
            schema: "catalog");

        migrationBuilder.DropTable(
            name: "Students",
            schema: "catalog");
    }
}
