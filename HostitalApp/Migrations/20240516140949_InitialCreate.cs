using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "USERS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USERNAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EMAIL = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PASSWORD = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    FIRSTNAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LASTNAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    USER_ROLE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DOCTORS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PHONE_NUMBER = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CLINIC = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DOCTORS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DOCTORS_USERS",
                        column: x => x.UserId,
                        principalTable: "USERS",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "PATIENTS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PHONE_NUMBER = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CLINIC = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PATIENTS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PATIENTS_USERS",
                        column: x => x.UserId,
                        principalTable: "USERS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "APPOINTMENT",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DOCTOR_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APPOINTMENT", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DOCTOR_APPOINTMENT",
                        column: x => x.DOCTOR_ID,
                        principalTable: "DOCTORS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PATIENTS_APPOINTMENTS",
                columns: table => new
                {
                    AppointmentsId = table.Column<int>(type: "int", nullable: false),
                    PatientsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PATIENTS_APPOINTMENTS", x => new { x.AppointmentsId, x.PatientsId });
                    table.ForeignKey(
                        name: "FK_PATIENTS_APPOINTMENTS_APPOINTMENT_AppointmentsId",
                        column: x => x.AppointmentsId,
                        principalTable: "APPOINTMENT",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PATIENTS_APPOINTMENTS_PATIENTS_PatientsId",
                        column: x => x.PatientsId,
                        principalTable: "PATIENTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_APPOINTMENT_DOCTOR_ID",
                table: "APPOINTMENT",
                column: "DOCTOR_ID");

            migrationBuilder.CreateIndex(
                name: "IX_DOCTORS_UserId",
                table: "DOCTORS",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PATIENTS_UserId",
                table: "PATIENTS",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PATIENTS_APPOINTMENTS_PatientsId",
                table: "PATIENTS_APPOINTMENTS",
                column: "PatientsId");

            migrationBuilder.CreateIndex(
                name: "IX_LASTNAME",
                table: "USERS",
                column: "LASTNAME");

            migrationBuilder.CreateIndex(
                name: "UQ_EMAIL",
                table: "USERS",
                column: "EMAIL",
                unique: true,
                filter: "[EMAIL] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ_USERNAME",
                table: "USERS",
                column: "USERNAME",
                unique: true,
                filter: "[USERNAME] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PATIENTS_APPOINTMENTS");

            migrationBuilder.DropTable(
                name: "APPOINTMENT");

            migrationBuilder.DropTable(
                name: "PATIENTS");

            migrationBuilder.DropTable(
                name: "DOCTORS");

            migrationBuilder.DropTable(
                name: "USERS");
        }
    }
}
