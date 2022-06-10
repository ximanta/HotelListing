using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserService.Migrations
{
    public partial class UserProfileMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Age", "EmailId", "Gender", "Name", "Password" },
                values: new object[] { 1, 24, "andy@example.com", "M", "Andy Crawford", "Andy" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Age", "EmailId", "Gender", "Name", "Password" },
                values: new object[] { 2, 22, "julia@example.com", "F", "Julia Jackson", "Julia" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Age", "EmailId", "Gender", "Name", "Password" },
                values: new object[] { 3, 25, "arun@example.com", "M", "Arun Lal", "Arun" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
