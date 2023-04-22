using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiplomCentralAPI.Migrations
{
    /// <inheritdoc />
    public partial class update2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Experiments_Handlers_HandlerId",
                table: "Experiments");

            migrationBuilder.AlterColumn<int>(
                name: "HandlerId",
                table: "Experiments",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Experiments_Handlers_HandlerId",
                table: "Experiments",
                column: "HandlerId",
                principalTable: "Handlers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Experiments_Handlers_HandlerId",
                table: "Experiments");

            migrationBuilder.AlterColumn<int>(
                name: "HandlerId",
                table: "Experiments",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Experiments_Handlers_HandlerId",
                table: "Experiments",
                column: "HandlerId",
                principalTable: "Handlers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
