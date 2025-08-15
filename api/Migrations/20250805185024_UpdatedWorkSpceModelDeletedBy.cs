using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedWorkSpceModelDeletedBy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModifiedByUserId",
                table: "WorkSpaces",
                newName: "UpdatedByUserId");

            migrationBuilder.AddColumn<string>(
                name: "DeactivationReason",
                table: "WorkSpaces",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedById",
                table: "WorkSpaces",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IsActive",
                table: "WorkSpaces",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "WorkSpaces",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaces_DeletedById",
                table: "WorkSpaces",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaces_UpdatedByUserId",
                table: "WorkSpaces",
                column: "UpdatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkSpaces_AspNetUsers_DeletedById",
                table: "WorkSpaces",
                column: "DeletedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkSpaces_AspNetUsers_UpdatedByUserId",
                table: "WorkSpaces",
                column: "UpdatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkSpaces_AspNetUsers_DeletedById",
                table: "WorkSpaces");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkSpaces_AspNetUsers_UpdatedByUserId",
                table: "WorkSpaces");

            migrationBuilder.DropIndex(
                name: "IX_WorkSpaces_DeletedById",
                table: "WorkSpaces");

            migrationBuilder.DropIndex(
                name: "IX_WorkSpaces_UpdatedByUserId",
                table: "WorkSpaces");

            migrationBuilder.DropColumn(
                name: "DeactivationReason",
                table: "WorkSpaces");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                table: "WorkSpaces");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "WorkSpaces");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "WorkSpaces");

            migrationBuilder.RenameColumn(
                name: "UpdatedByUserId",
                table: "WorkSpaces",
                newName: "ModifiedByUserId");
        }
    }
}
