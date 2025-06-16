using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_WorkSpaces_WorkSpaceId",
                table: "Projects");

            migrationBuilder.AlterColumn<int>(
                name: "WorkSpaceId",
                table: "Projects",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_WorkSpaces_WorkSpaceId",
                table: "Projects",
                column: "WorkSpaceId",
                principalTable: "WorkSpaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_WorkSpaces_WorkSpaceId",
                table: "Projects");

            migrationBuilder.AlterColumn<int>(
                name: "WorkSpaceId",
                table: "Projects",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_WorkSpaces_WorkSpaceId",
                table: "Projects",
                column: "WorkSpaceId",
                principalTable: "WorkSpaces",
                principalColumn: "Id");
        }
    }
}
