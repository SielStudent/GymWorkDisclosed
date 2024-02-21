using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class addedpersonaltrainers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PersonalTrainerEntityId",
                table: "gymGoer",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateTable(
                name: "personalTrainers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_personalTrainers", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_gymGoer_PersonalTrainerEntityId",
                table: "gymGoer",
                column: "PersonalTrainerEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_gymGoer_personalTrainers_PersonalTrainerEntityId",
                table: "gymGoer",
                column: "PersonalTrainerEntityId",
                principalTable: "personalTrainers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_gymGoer_personalTrainers_PersonalTrainerEntityId",
                table: "gymGoer");

            migrationBuilder.DropTable(
                name: "personalTrainers");

            migrationBuilder.DropIndex(
                name: "IX_gymGoer_PersonalTrainerEntityId",
                table: "gymGoer");

            migrationBuilder.DropColumn(
                name: "PersonalTrainerEntityId",
                table: "gymGoer");
        }
    }
}
