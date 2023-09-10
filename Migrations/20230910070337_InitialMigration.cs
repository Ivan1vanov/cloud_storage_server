using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cloud_storage.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dokuments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dokuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dokuments_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DokumentUser",
                columns: table => new
                {
                    AccessedDokumentsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AllowedUsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DokumentUser", x => new { x.AccessedDokumentsId, x.AllowedUsersId });
                    table.ForeignKey(
                        name: "FK_DokumentUser_Dokuments_AccessedDokumentsId",
                        column: x => x.AccessedDokumentsId,
                        principalTable: "Dokuments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DokumentUser_Users_AllowedUsersId",
                        column: x => x.AllowedUsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dokuments_OwnerId",
                table: "Dokuments",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_DokumentUser_AllowedUsersId",
                table: "DokumentUser",
                column: "AllowedUsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DokumentUser");

            migrationBuilder.DropTable(
                name: "Dokuments");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
