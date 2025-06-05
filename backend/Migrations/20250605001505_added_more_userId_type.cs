using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class added_more_userId_type : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resources_AspNetUsers_UploadedByUserId1",
                table: "Resources");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AspNetUsers_AssignedToId1",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_AssignedToId1",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Resources_UploadedByUserId1",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "AssignedToId1",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "UploadedByUserId1",
                table: "Resources");

            migrationBuilder.AlterColumn<string>(
                name: "AssignedToId",
                table: "Tasks",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UploadedByUserId",
                table: "Resources",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_AssignedToId",
                table: "Tasks",
                column: "AssignedToId");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_UploadedByUserId",
                table: "Resources",
                column: "UploadedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_AspNetUsers_UploadedByUserId",
                table: "Resources",
                column: "UploadedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AspNetUsers_AssignedToId",
                table: "Tasks",
                column: "AssignedToId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resources_AspNetUsers_UploadedByUserId",
                table: "Resources");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AspNetUsers_AssignedToId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_AssignedToId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Resources_UploadedByUserId",
                table: "Resources");

            migrationBuilder.AlterColumn<int>(
                name: "AssignedToId",
                table: "Tasks",
                type: "integer",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AssignedToId1",
                table: "Tasks",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UploadedByUserId",
                table: "Resources",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "UploadedByUserId1",
                table: "Resources",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_AssignedToId1",
                table: "Tasks",
                column: "AssignedToId1");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_UploadedByUserId1",
                table: "Resources",
                column: "UploadedByUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_AspNetUsers_UploadedByUserId1",
                table: "Resources",
                column: "UploadedByUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AspNetUsers_AssignedToId1",
                table: "Tasks",
                column: "AssignedToId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
