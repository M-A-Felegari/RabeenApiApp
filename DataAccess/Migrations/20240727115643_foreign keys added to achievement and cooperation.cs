using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class foreignkeysaddedtoachievementandcooperation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Achievements_Members_MemberId",
                table: "Achievements");

            migrationBuilder.DropForeignKey(
                name: "FK_AssociationCooperations_Associations_AssociationId",
                table: "AssociationCooperations");

            migrationBuilder.DropIndex(
                name: "IX_Achievements_MemberId",
                table: "Achievements");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "Achievements");

            migrationBuilder.AlterColumn<int>(
                name: "AssociationId",
                table: "AssociationCooperations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Achievements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Achievements_OwnerId",
                table: "Achievements",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Achievements_Members_OwnerId",
                table: "Achievements",
                column: "OwnerId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssociationCooperations_Associations_AssociationId",
                table: "AssociationCooperations",
                column: "AssociationId",
                principalTable: "Associations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Achievements_Members_OwnerId",
                table: "Achievements");

            migrationBuilder.DropForeignKey(
                name: "FK_AssociationCooperations_Associations_AssociationId",
                table: "AssociationCooperations");

            migrationBuilder.DropIndex(
                name: "IX_Achievements_OwnerId",
                table: "Achievements");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Achievements");

            migrationBuilder.AlterColumn<int>(
                name: "AssociationId",
                table: "AssociationCooperations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "MemberId",
                table: "Achievements",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Achievements_MemberId",
                table: "Achievements",
                column: "MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Achievements_Members_MemberId",
                table: "Achievements",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AssociationCooperations_Associations_AssociationId",
                table: "AssociationCooperations",
                column: "AssociationId",
                principalTable: "Associations",
                principalColumn: "Id");
        }
    }
}
