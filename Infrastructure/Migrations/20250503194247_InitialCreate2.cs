using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgendaSerial3.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_PersonalCalendar_CalendarId1",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalCalendar_Users_UserId",
                table: "PersonalCalendar");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonalCalendar",
                table: "PersonalCalendar");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Appointment",
                table: "Appointment");

            migrationBuilder.RenameTable(
                name: "PersonalCalendar",
                newName: "Calendars");

            migrationBuilder.RenameTable(
                name: "Appointment",
                newName: "Appointments");

            migrationBuilder.RenameIndex(
                name: "IX_PersonalCalendar_UserId",
                table: "Calendars",
                newName: "IX_Calendars_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointment_CalendarId1",
                table: "Appointments",
                newName: "IX_Appointments_CalendarId1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Calendars",
                table: "Calendars",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Calendars_CalendarId1",
                table: "Appointments",
                column: "CalendarId1",
                principalTable: "Calendars",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Calendars_Users_UserId",
                table: "Calendars",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Calendars_CalendarId1",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Calendars_Users_UserId",
                table: "Calendars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Calendars",
                table: "Calendars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments");

            migrationBuilder.RenameTable(
                name: "Calendars",
                newName: "PersonalCalendar");

            migrationBuilder.RenameTable(
                name: "Appointments",
                newName: "Appointment");

            migrationBuilder.RenameIndex(
                name: "IX_Calendars_UserId",
                table: "PersonalCalendar",
                newName: "IX_PersonalCalendar_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_CalendarId1",
                table: "Appointment",
                newName: "IX_Appointment_CalendarId1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonalCalendar",
                table: "PersonalCalendar",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Appointment",
                table: "Appointment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_PersonalCalendar_CalendarId1",
                table: "Appointment",
                column: "CalendarId1",
                principalTable: "PersonalCalendar",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalCalendar_Users_UserId",
                table: "PersonalCalendar",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
