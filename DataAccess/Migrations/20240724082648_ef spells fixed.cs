using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class efspellsfixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Achievments_Members_MemberId",
                table: "Achievments");

            migrationBuilder.DropTable(
                name: "AssociationCooprations");

            migrationBuilder.DropTable(
                name: "ContactRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Achievments",
                table: "Achievments");

            migrationBuilder.RenameTable(
                name: "Achievments",
                newName: "Achievements");

            migrationBuilder.RenameIndex(
                name: "IX_Achievments_MemberId",
                table: "Achievements",
                newName: "IX_Achievements_MemberId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Achievements",
                table: "Achievements",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AssociationCooperations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinishDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AssociationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssociationCooperations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssociationCooperations_Associations_AssociationId",
                        column: x => x.AssociationId,
                        principalTable: "Associations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ContactMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactMessages", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssociationCooperations_AssociationId",
                table: "AssociationCooperations",
                column: "AssociationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Achievements_Members_MemberId",
                table: "Achievements",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Achievements_Members_MemberId",
                table: "Achievements");

            migrationBuilder.DropTable(
                name: "AssociationCooperations");

            migrationBuilder.DropTable(
                name: "ContactMessages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Achievements",
                table: "Achievements");

            migrationBuilder.RenameTable(
                name: "Achievements",
                newName: "Achievments");

            migrationBuilder.RenameIndex(
                name: "IX_Achievements_MemberId",
                table: "Achievments",
                newName: "IX_Achievments_MemberId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Achievments",
                table: "Achievments",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AssociationCooprations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssociationId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FinishDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssociationCooprations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssociationCooprations_Associations_AssociationId",
                        column: x => x.AssociationId,
                        principalTable: "Associations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ContactRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactRequests", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssociationCooprations_AssociationId",
                table: "AssociationCooprations",
                column: "AssociationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Achievments_Members_MemberId",
                table: "Achievments",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id");
        }
    }
}
