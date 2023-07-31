using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DocAppointApi.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigrat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Consecrations",
                columns: table => new
                {
                    consId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    consName = table.Column<string>(type: "text", nullable: false),
                    consDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    consTaille = table.Column<string>(type: "text", nullable: false),
                    consTension = table.Column<int>(type: "integer", nullable: false),
                    consPoids = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consecrations", x => x.consId);
                });

            migrationBuilder.CreateTable(
                name: "MalariaCuires",
                columns: table => new
                {
                    malaid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    designationM = table.Column<string>(type: "text", nullable: false),
                    descriptionM = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MalariaCuires", x => x.malaid);
                });

            migrationBuilder.CreateTable(
                name: "Specialites",
                columns: table => new
                {
                    speid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Designation = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialites", x => x.speid);
                });

            migrationBuilder.CreateTable(
                name: "Statuts",
                columns: table => new
                {
                    RDVPId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    val = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuts", x => x.RDVPId);
                });

            migrationBuilder.CreateTable(
                name: "TraitemtPs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MedocTr = table.Column<string>(type: "text", nullable: false),
                    MedocAvis = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TraitemtPs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    userId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Age = table.Column<int>(type: "integer", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Phonenumber = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Avatar = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "Adminis",
                columns: table => new
                {
                    userId = table.Column<int>(type: "integer", nullable: false),
                    AdminId = table.Column<int>(type: "integer", nullable: false),
                    titre = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adminis", x => x.userId);
                    table.ForeignKey(
                        name: "FK_Adminis_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Medecins",
                columns: table => new
                {
                    userId = table.Column<int>(type: "integer", nullable: false),
                    medocId = table.Column<int>(type: "integer", nullable: false),
                    validation = table.Column<bool>(type: "boolean", nullable: false),
                    sperid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medecins", x => x.userId);
                    table.ForeignKey(
                        name: "FK_Medecins_Specialites_sperid",
                        column: x => x.sperid,
                        principalTable: "Specialites",
                        principalColumn: "speid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Medecins_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    userId = table.Column<int>(type: "integer", nullable: false),
                    PatientId = table.Column<int>(type: "integer", nullable: false),
                    AdresseP = table.Column<string>(type: "text", nullable: false),
                    Sexe = table.Column<string>(type: "text", nullable: false),
                    malaid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.userId);
                    table.ForeignKey(
                        name: "FK_Patients_MalariaCuires_malaid",
                        column: x => x.malaid,
                        principalTable: "MalariaCuires",
                        principalColumn: "malaid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Patients_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RDVMs",
                columns: table => new
                {
                    RDVId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Datedb = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RDVlibelle = table.Column<string>(type: "text", nullable: false),
                    Datefin = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    medocId = table.Column<int>(type: "integer", nullable: false),
                    MedecinuserId = table.Column<int>(type: "integer", nullable: false),
                    PatientId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RDVMs", x => x.RDVId);
                    table.ForeignKey(
                        name: "FK_RDVMs_Medecins_MedecinuserId",
                        column: x => x.MedecinuserId,
                        principalTable: "Medecins",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RDVMs_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medecins_sperid",
                table: "Medecins",
                column: "sperid");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_malaid",
                table: "Patients",
                column: "malaid");

            migrationBuilder.CreateIndex(
                name: "IX_RDVMs_MedecinuserId",
                table: "RDVMs",
                column: "MedecinuserId");

            migrationBuilder.CreateIndex(
                name: "IX_RDVMs_PatientId",
                table: "RDVMs",
                column: "PatientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Adminis");

            migrationBuilder.DropTable(
                name: "Consecrations");

            migrationBuilder.DropTable(
                name: "RDVMs");

            migrationBuilder.DropTable(
                name: "Statuts");

            migrationBuilder.DropTable(
                name: "TraitemtPs");

            migrationBuilder.DropTable(
                name: "Medecins");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Specialites");

            migrationBuilder.DropTable(
                name: "MalariaCuires");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
