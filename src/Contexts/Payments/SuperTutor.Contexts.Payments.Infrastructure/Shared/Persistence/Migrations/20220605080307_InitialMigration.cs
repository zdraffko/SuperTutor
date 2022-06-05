using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperTutor.Contexts.Payments.Infrastructure.Shared.Persistence.Migrations;

public partial class InitialMigration : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "payments");

        migrationBuilder.CreateTable(
            name: "Transfers",
            schema: "payments",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ChargeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                LessonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                TutorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                Currency = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_Transfers", x => x.Id));

        migrationBuilder.CreateTable(
            name: "Tutors",
            schema: "payments",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                IsPersonalInformationCollected = table.Column<bool>(type: "bit", nullable: false),
                IsAddressInformationCollected = table.Column<bool>(type: "bit", nullable: false),
                IsBankAccountInformationCollected = table.Column<bool>(type: "bit", nullable: false),
                AreVerificationDocumentsCollected = table.Column<bool>(type: "bit", nullable: false),
                AreTermsOfServiceAccepted = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_Tutors", x => x.Id));
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Transfers",
            schema: "payments");

        migrationBuilder.DropTable(
            name: "Tutors",
            schema: "payments");
    }
}
