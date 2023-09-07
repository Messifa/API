using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocAppointApi.Migrations
{
    /// <inheritdoc />
    public partial class kurokomigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RDVMs_Medecins_MedecinuserId",
                table: "RDVMs");

            migrationBuilder.DropForeignKey(
                name: "FK_RDVMs_Patients_PatientId",
                table: "RDVMs");

            migrationBuilder.DropForeignKey(
                name: "FK_RDVMs_Statuts_StatutRDVPId",
                table: "RDVMs");

            migrationBuilder.DropIndex(
                name: "IX_RDVMs_MedecinuserId",
                table: "RDVMs");

            migrationBuilder.DropIndex(
                name: "IX_RDVMs_PatientId",
                table: "RDVMs");

            migrationBuilder.DropIndex(
                name: "IX_RDVMs_StatutRDVPId",
                table: "RDVMs");

            migrationBuilder.DropColumn(
                name: "MedecinuserId",
                table: "RDVMs");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "RDVMs");

            migrationBuilder.DropColumn(
                name: "RDVPId",
                table: "RDVMs");

            migrationBuilder.DropColumn(
                name: "StatutRDVPId",
                table: "RDVMs");

            migrationBuilder.DropColumn(
                name: "medocId",
                table: "RDVMs");

            migrationBuilder.DropColumn(
                name: "validation",
                table: "Medecins");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MedecinuserId",
                table: "RDVMs",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "RDVMs",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.AddColumn<int>(
                name: "medocId",
                table: "RDVMs",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "validation",
                table: "Medecins",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_RDVMs_MedecinuserId",
                table: "RDVMs",
                column: "MedecinuserId");

            migrationBuilder.CreateIndex(
                name: "IX_RDVMs_PatientId",
                table: "RDVMs",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_RDVMs_StatutRDVPId",
                table: "RDVMs",
                column: "StatutRDVPId");

            migrationBuilder.AddForeignKey(
                name: "FK_RDVMs_Medecins_MedecinuserId",
                table: "RDVMs",
                column: "MedecinuserId",
                principalTable: "Medecins",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RDVMs_Patients_PatientId",
                table: "RDVMs",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RDVMs_Statuts_StatutRDVPId",
                table: "RDVMs",
                column: "StatutRDVPId",
                principalTable: "Statuts",
                principalColumn: "RDVPId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
