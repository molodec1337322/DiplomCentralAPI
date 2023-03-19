using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiplomCentralAPI.Migrations
{
    /// <inheritdoc />
    public partial class Update1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schemas_Handlers_HandlerId",
                table: "Schemas");

            migrationBuilder.AlterColumn<int>(
                name: "HandlerId",
                table: "Schemas",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "HandlerId",
                table: "Experiments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Experiments_HandlerId",
                table: "Experiments",
                column: "HandlerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Experiments_Handlers_HandlerId",
                table: "Experiments",
                column: "HandlerId",
                principalTable: "Handlers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schemas_Handlers_HandlerId",
                table: "Schemas",
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

            migrationBuilder.DropForeignKey(
                name: "FK_Schemas_Handlers_HandlerId",
                table: "Schemas");

            migrationBuilder.DropIndex(
                name: "IX_Experiments_HandlerId",
                table: "Experiments");

            migrationBuilder.DropColumn(
                name: "HandlerId",
                table: "Experiments");

            migrationBuilder.AlterColumn<int>(
                name: "HandlerId",
                table: "Schemas",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Schemas_Handlers_HandlerId",
                table: "Schemas",
                column: "HandlerId",
                principalTable: "Handlers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
