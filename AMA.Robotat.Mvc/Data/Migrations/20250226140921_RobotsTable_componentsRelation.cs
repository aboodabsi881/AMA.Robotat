using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AMA.Robotat.Mvc.Data.Migrations
{
    /// <inheritdoc />
    public partial class RobotsTable_componentsRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Robots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Robots", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComponentRobot",
                columns: table => new
                {
                    ComponentsId = table.Column<int>(type: "int", nullable: false),
                    RobotsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentRobot", x => new { x.ComponentsId, x.RobotsId });
                    table.ForeignKey(
                        name: "FK_ComponentRobot_Components_ComponentsId",
                        column: x => x.ComponentsId,
                        principalTable: "Components",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComponentRobot_Robots_RobotsId",
                        column: x => x.RobotsId,
                        principalTable: "Robots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComponentRobot_RobotsId",
                table: "ComponentRobot",
                column: "RobotsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComponentRobot");

            migrationBuilder.DropTable(
                name: "Robots");
        }
    }
}
