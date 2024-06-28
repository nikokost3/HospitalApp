using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalApp.Migrations
{
    /// <inheritdoc />
    public partial class ColumnInsert : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_APPOINTMENT_DOCTORS_DOCTOR_ID",
                table: "APPOINTMENT");

            migrationBuilder.DropTable(
                name: "AppointmentPatient");

            migrationBuilder.AddColumn<DateTime>(
                name: "DAY",
                table: "APPOINTMENT",
                type: "datetime2",
                maxLength: 50,
                nullable: true);

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
                name: "IX_PATIENTS_APPOINTMENTS_PatientsId",
                table: "PATIENTS_APPOINTMENTS",
                column: "PatientsId");

            migrationBuilder.AddForeignKey(
                name: "FK_DOCTOR_APPOINTMENT",
                table: "APPOINTMENT",
                column: "DOCTOR_ID",
                principalTable: "DOCTORS",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DOCTOR_APPOINTMENT",
                table: "APPOINTMENT");

            migrationBuilder.DropTable(
                name: "PATIENTS_APPOINTMENTS");

            migrationBuilder.DropColumn(
                name: "DAY",
                table: "APPOINTMENT");

            migrationBuilder.CreateTable(
                name: "AppointmentPatient",
                columns: table => new
                {
                    AppointmentsId = table.Column<int>(type: "int", nullable: false),
                    PatientsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentPatient", x => new { x.AppointmentsId, x.PatientsId });
                    table.ForeignKey(
                        name: "FK_AppointmentPatient_APPOINTMENT_AppointmentsId",
                        column: x => x.AppointmentsId,
                        principalTable: "APPOINTMENT",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppointmentPatient_PATIENTS_PatientsId",
                        column: x => x.PatientsId,
                        principalTable: "PATIENTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentPatient_PatientsId",
                table: "AppointmentPatient",
                column: "PatientsId");

            migrationBuilder.AddForeignKey(
                name: "FK_APPOINTMENT_DOCTORS_DOCTOR_ID",
                table: "APPOINTMENT",
                column: "DOCTOR_ID",
                principalTable: "DOCTORS",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
