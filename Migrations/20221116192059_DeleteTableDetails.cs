using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_MVC.Migrations
{
    public partial class DeleteTableDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Hause_Details_HauseDetailsId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Hause_Details");

            migrationBuilder.DropIndex(
                name: "IX_Users_HauseDetailsId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "HauseDetailsId",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HauseDetailsId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Hause_Details",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fire_Detector = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Heating_System = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hause_Details", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_HauseDetailsId",
                table: "Users",
                column: "HauseDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Hause_Details_HauseDetailsId",
                table: "Users",
                column: "HauseDetailsId",
                principalTable: "Hause_Details",
                principalColumn: "Id");
        }
    }
}
