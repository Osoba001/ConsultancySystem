using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Auth.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoleTb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleTb", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RefreshTokenExpireTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecoveryPin = table.Column<int>(type: "int", nullable: false),
                    RecoveryPinExpireTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTb", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AssignedUserRoleTb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignedUserRoleTb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssignedUserRoleTb_RoleTb_RoleId",
                        column: x => x.RoleId,
                        principalTable: "RoleTb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssignedUserRoleTb_UserTb_UserId",
                        column: x => x.UserId,
                        principalTable: "UserTb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssignedUserRoleTb_RoleId",
                table: "AssignedUserRoleTb",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedUserRoleTb_UserId",
                table: "AssignedUserRoleTb",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleTb_Name",
                table: "RoleTb",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserTb_Email",
                table: "UserTb",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignedUserRoleTb");

            migrationBuilder.DropTable(
                name: "RoleTb");

            migrationBuilder.DropTable(
                name: "UserTb");
        }
    }
}
