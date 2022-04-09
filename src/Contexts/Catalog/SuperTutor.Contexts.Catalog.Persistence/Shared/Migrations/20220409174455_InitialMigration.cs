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
            name: "FavoriteFilters",
            schema: "catalog",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_FavoriteFilters", x => x.Id));
    }

    protected override void Down(MigrationBuilder migrationBuilder) => migrationBuilder.DropTable(
            name: "FavoriteFilters",
            schema: "catalog");
}
