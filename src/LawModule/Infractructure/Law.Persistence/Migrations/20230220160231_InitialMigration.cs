using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Law.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientTB",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientTB", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentTB",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentTB", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LawyerTB",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OnlineCharge = table.Column<double>(type: "float", nullable: false),
                    OfflineCharge = table.Column<double>(type: "float", nullable: false),
                    IsVerify = table.Column<bool>(type: "bit", nullable: false),
                    OfficeEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNo = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Languages = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OfficeAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LawyerTB", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentLawyer",
                columns: table => new
                {
                    DepartmentsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LawyersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentLawyer", x => new { x.DepartmentsId, x.LawyersId });
                    table.ForeignKey(
                        name: "FK_DepartmentLawyer_DepartmentTB_DepartmentsId",
                        column: x => x.DepartmentsId,
                        principalTable: "DepartmentTB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentLawyer_LawyerTB_LawyersId",
                        column: x => x.LawyersId,
                        principalTable: "LawyerTB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimeSlotTB",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    StartHour = table.Column<int>(type: "int", nullable: false),
                    StartMinute = table.Column<int>(type: "int", nullable: false),
                    EndHour = table.Column<int>(type: "int", nullable: false),
                    EndMinute = table.Column<int>(type: "int", nullable: false),
                    LawyerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LawyerId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSlotTB", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeSlotTB_LawyerTB_LawyerId",
                        column: x => x.LawyerId,
                        principalTable: "LawyerTB",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TimeSlotTB_LawyerTB_LawyerId1",
                        column: x => x.LawyerId1,
                        principalTable: "LawyerTB",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppointmentTB",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TimeSlotId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LawyerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CaseDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReviewDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HasReviewed = table.Column<bool>(type: "bit", nullable: false),
                    IsCancel = table.Column<bool>(type: "bit", nullable: false),
                    Charge = table.Column<double>(type: "float", nullable: false),
                    AppointmentType = table.Column<int>(type: "int", nullable: false),
                    LawyerReport = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientFeedBack = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stars = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentTB", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppointmentTB_ClientTB_ClientId",
                        column: x => x.ClientId,
                        principalTable: "ClientTB",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppointmentTB_LawyerTB_LawyerId",
                        column: x => x.LawyerId,
                        principalTable: "LawyerTB",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppointmentTB_TimeSlotTB_TimeSlotId",
                        column: x => x.TimeSlotId,
                        principalTable: "TimeSlotTB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentTB_ClientId_ReviewDate",
                table: "AppointmentTB",
                columns: new[] { "ClientId", "ReviewDate" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentTB_LawyerId_ReviewDate",
                table: "AppointmentTB",
                columns: new[] { "LawyerId", "ReviewDate" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentTB_TimeSlotId",
                table: "AppointmentTB",
                column: "TimeSlotId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentLawyer_LawyersId",
                table: "DepartmentLawyer",
                column: "LawyersId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentTB_Name",
                table: "DepartmentTB",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlotTB_Index",
                table: "TimeSlotTB",
                column: "Index",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlotTB_LawyerId",
                table: "TimeSlotTB",
                column: "LawyerId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlotTB_LawyerId1",
                table: "TimeSlotTB",
                column: "LawyerId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentTB");

            migrationBuilder.DropTable(
                name: "DepartmentLawyer");

            migrationBuilder.DropTable(
                name: "ClientTB");

            migrationBuilder.DropTable(
                name: "TimeSlotTB");

            migrationBuilder.DropTable(
                name: "DepartmentTB");

            migrationBuilder.DropTable(
                name: "LawyerTB");
        }
    }
}
