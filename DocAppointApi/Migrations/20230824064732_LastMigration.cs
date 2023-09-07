using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DocAppointApi.Migrations
{
    /// <inheritdoc />
    public partial class LastMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medecins_Specialites_sperid",
                table: "Medecins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Specialites",
                table: "Specialites");

            migrationBuilder.DropIndex(
                name: "IX_Medecins_sperid",
                table: "Medecins");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "medocId",
                table: "Medecins");

            migrationBuilder.DropColumn(
                name: "sperid",
                table: "Medecins");

            migrationBuilder.DropColumn(
                name: "descriptionM",
                table: "MalariaCuires");

            migrationBuilder.RenameColumn(
                name: "Designation",
                table: "Specialites",
                newName: "Category");

            migrationBuilder.RenameColumn(
                name: "speid",
                table: "Specialites",
                newName: "PatientId");

            migrationBuilder.AlterColumn<int>(
                name: "PatientId",
                table: "Specialites",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Specialites",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<DateTime>(
                name: "AppointmentTime",
                table: "Specialites",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "Specialites",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "RDVMs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "RDVPId",
                table: "RDVMs",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StatutRDVPId",
                table: "RDVMs",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Specialite",
                table: "Medecins",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Specialites",
                table: "Specialites",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_RDVMs_StatutRDVPId",
                table: "RDVMs",
                column: "StatutRDVPId");

            migrationBuilder.AddForeignKey(
                name: "FK_RDVMs_Statuts_StatutRDVPId",
                table: "RDVMs",
                column: "StatutRDVPId",
                principalTable: "Statuts",
                principalColumn: "RDVPId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RDVMs_Statuts_StatutRDVPId",
                table: "RDVMs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Specialites",
                table: "Specialites");

            migrationBuilder.DropIndex(
                name: "IX_RDVMs_StatutRDVPId",
                table: "RDVMs");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Specialites");

            migrationBuilder.DropColumn(
                name: "AppointmentTime",
                table: "Specialites");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Specialites");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "RDVMs");

            migrationBuilder.DropColumn(
                name: "RDVPId",
                table: "RDVMs");

            migrationBuilder.DropColumn(
                name: "StatutRDVPId",
                table: "RDVMs");

            migrationBuilder.DropColumn(
                name: "Specialite",
                table: "Medecins");

            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "Specialites",
                newName: "speid");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "Specialites",
                newName: "Designation");

            migrationBuilder.AlterColumn<int>(
                name: "speid",
                table: "Specialites",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "Patients",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "medocId",
                table: "Medecins",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "sperid",
                table: "Medecins",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "descriptionM",
                table: "MalariaCuires",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Specialites",
                table: "Specialites",
                column: "speid");

            migrationBuilder.CreateIndex(
                name: "IX_Medecins_sperid",
                table: "Medecins",
                column: "sperid");

            migrationBuilder.AddForeignKey(
                name: "FK_Medecins_Specialites_sperid",
                table: "Medecins",
                column: "sperid",
                principalTable: "Specialites",
                principalColumn: "speid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
