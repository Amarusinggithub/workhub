using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class added_task_resource : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Issues_IssueId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_InUserGroups_AspNetUsers_UserId",
                table: "InUserGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_InUserGroups_UserGroups_UserGroupId",
                table: "InUserGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_PlanHistories_PlanHistoryId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_NotificationMembers_AspNetUsers_Userid",
                table: "NotificationMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_Resources_AspNetUsers_UploadedByUserId",
                table: "Resources");

            migrationBuilder.DropTable(
                name: "IssueLabels");

            migrationBuilder.DropTable(
                name: "Issues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InUserGroups",
                table: "InUserGroups");

            migrationBuilder.RenameTable(
                name: "InUserGroups",
                newName: "UserGroupMembers");

            migrationBuilder.RenameColumn(
                name: "UploadedByUserId",
                table: "Resources",
                newName: "UploaderId");

            migrationBuilder.RenameIndex(
                name: "IX_Resources_UploadedByUserId",
                table: "Resources",
                newName: "IX_Resources_UploaderId");

            migrationBuilder.RenameColumn(
                name: "Userid",
                table: "NotificationMembers",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "NotificationMembers",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_NotificationMembers_Userid",
                table: "NotificationMembers",
                newName: "IX_NotificationMembers_UserId");

            migrationBuilder.RenameColumn(
                name: "IssueId",
                table: "Comments",
                newName: "TaskId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_IssueId",
                table: "Comments",
                newName: "IX_Comments_TaskId");

            migrationBuilder.RenameIndex(
                name: "IX_InUserGroups_UserId",
                table: "UserGroupMembers",
                newName: "IX_UserGroupMembers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_InUserGroups_UserGroupId",
                table: "UserGroupMembers",
                newName: "IX_UserGroupMembers_UserGroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserGroupMembers",
                table: "UserGroupMembers",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TaskName = table.Column<string>(type: "text", nullable: false),
                    TaskDescription = table.Column<string>(type: "text", nullable: true),
                    TaskStatus = table.Column<int>(type: "integer", nullable: false),
                    TaskType = table.Column<int>(type: "integer", nullable: false),
                    TaskPriority = table.Column<int>(type: "integer", nullable: false),
                    AssignedToId = table.Column<int>(type: "integer", nullable: true),
                    ProjectId = table.Column<int>(type: "integer", nullable: false),
                    ParentId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    FinishedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TaskId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_AspNetUsers_AssignedToId",
                        column: x => x.AssignedToId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tasks_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TaskLabels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TaskId = table.Column<int>(type: "integer", nullable: false),
                    LabelId = table.Column<int>(type: "integer", nullable: false),
                    AttachedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskLabels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskLabels_Labels_LabelId",
                        column: x => x.LabelId,
                        principalTable: "Labels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskLabels_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskResources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ResourceId = table.Column<int>(type: "integer", nullable: false),
                    TaskId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastDownloadAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastOpenAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskResources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskResources_Resources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskResources_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskLabels_LabelId",
                table: "TaskLabels",
                column: "LabelId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskLabels_TaskId",
                table: "TaskLabels",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskResources_ResourceId",
                table: "TaskResources",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskResources_TaskId",
                table: "TaskResources",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_AssignedToId",
                table: "Tasks",
                column: "AssignedToId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ProjectId",
                table: "Tasks",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_TaskId",
                table: "Tasks",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Tasks_TaskId",
                table: "Comments",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_PlanHistories_PlanHistoryId",
                table: "Invoices",
                column: "PlanHistoryId",
                principalTable: "PlanHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationMembers_AspNetUsers_UserId",
                table: "NotificationMembers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_AspNetUsers_UploaderId",
                table: "Resources",
                column: "UploaderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroupMembers_AspNetUsers_UserId",
                table: "UserGroupMembers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroupMembers_UserGroups_UserGroupId",
                table: "UserGroupMembers",
                column: "UserGroupId",
                principalTable: "UserGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Tasks_TaskId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_PlanHistories_PlanHistoryId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_NotificationMembers_AspNetUsers_UserId",
                table: "NotificationMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_Resources_AspNetUsers_UploaderId",
                table: "Resources");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroupMembers_AspNetUsers_UserId",
                table: "UserGroupMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroupMembers_UserGroups_UserGroupId",
                table: "UserGroupMembers");

            migrationBuilder.DropTable(
                name: "TaskLabels");

            migrationBuilder.DropTable(
                name: "TaskResources");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserGroupMembers",
                table: "UserGroupMembers");

            migrationBuilder.RenameTable(
                name: "UserGroupMembers",
                newName: "InUserGroups");

            migrationBuilder.RenameColumn(
                name: "UploaderId",
                table: "Resources",
                newName: "UploadedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Resources_UploaderId",
                table: "Resources",
                newName: "IX_Resources_UploadedByUserId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "NotificationMembers",
                newName: "Userid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "NotificationMembers",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_NotificationMembers_UserId",
                table: "NotificationMembers",
                newName: "IX_NotificationMembers_Userid");

            migrationBuilder.RenameColumn(
                name: "TaskId",
                table: "Comments",
                newName: "IssueId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_TaskId",
                table: "Comments",
                newName: "IX_Comments_IssueId");

            migrationBuilder.RenameIndex(
                name: "IX_UserGroupMembers_UserId",
                table: "InUserGroups",
                newName: "IX_InUserGroups_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserGroupMembers_UserGroupId",
                table: "InUserGroups",
                newName: "IX_InUserGroups_UserGroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InUserGroups",
                table: "InUserGroups",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Issues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AssignedToId = table.Column<int>(type: "integer", nullable: true),
                    ProjectId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FinishedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IssueId = table.Column<int>(type: "integer", nullable: true),
                    IssuePriority = table.Column<int>(type: "integer", nullable: false),
                    IssueStatus = table.Column<int>(type: "integer", nullable: false),
                    IssueType = table.Column<int>(type: "integer", nullable: false),
                    ParentId = table.Column<int>(type: "integer", nullable: false),
                    TaskDescription = table.Column<string>(type: "text", nullable: true),
                    TaskName = table.Column<string>(type: "text", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Issues_AspNetUsers_AssignedToId",
                        column: x => x.AssignedToId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Issues_Issues_IssueId",
                        column: x => x.IssueId,
                        principalTable: "Issues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Issues_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IssueLabels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IssueId = table.Column<int>(type: "integer", nullable: false),
                    LabelId = table.Column<int>(type: "integer", nullable: false),
                    AttachedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueLabels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IssueLabels_Issues_IssueId",
                        column: x => x.IssueId,
                        principalTable: "Issues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IssueLabels_Labels_LabelId",
                        column: x => x.LabelId,
                        principalTable: "Labels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IssueLabels_IssueId",
                table: "IssueLabels",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueLabels_LabelId",
                table: "IssueLabels",
                column: "LabelId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_AssignedToId",
                table: "Issues",
                column: "AssignedToId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_IssueId",
                table: "Issues",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_ProjectId",
                table: "Issues",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Issues_IssueId",
                table: "Comments",
                column: "IssueId",
                principalTable: "Issues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InUserGroups_AspNetUsers_UserId",
                table: "InUserGroups",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InUserGroups_UserGroups_UserGroupId",
                table: "InUserGroups",
                column: "UserGroupId",
                principalTable: "UserGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_PlanHistories_PlanHistoryId",
                table: "Invoices",
                column: "PlanHistoryId",
                principalTable: "PlanHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationMembers_AspNetUsers_Userid",
                table: "NotificationMembers",
                column: "Userid",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_AspNetUsers_UploadedByUserId",
                table: "Resources",
                column: "UploadedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
