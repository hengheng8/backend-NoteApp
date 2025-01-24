using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_notesApp.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Create the "Note" table
            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"), 
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false), // Title as string (required)
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false) // Content as string (required)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id); 
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop the "Notes" table if the migration is rolled back
            migrationBuilder.DropTable(
                name: "Notes");
        }
    }
}
