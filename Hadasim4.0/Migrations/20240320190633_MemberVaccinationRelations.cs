using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hadasim4._0.Migrations
{
    public partial class MemberVaccinationRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vaccination",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Producer = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vaccination", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MemberVaccinationRelation",
                columns: table => new
                {
                    VaccinationId = table.Column<int>(type: "int", nullable: false),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberVaccinationRelation", x => new { x.VaccinationId, x.MemberId });
                    table.ForeignKey(
                        name: "FK_MemberVaccinationRelation_Vaccinations",
                        column: x => x.VaccinationId,
                        principalTable: "Vaccination",
                        principalColumn: "Id", // Indicates that the foreign key is referencing the "Id" column in the "Vaccinations" table
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MemberVaccinationRelation_MemberIds",
                        column: x => x.VaccinationId,
                        principalTable: "Member",
                        principalColumn: "MemberId", // Indicates that the foreign key is referencing the "Id" column in the "Vaccinations" table
                        onDelete: ReferentialAction.Cascade);
                });

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MemberVaccinationRelation");

            migrationBuilder.DropTable(
                name: "Vaccination");
        }
    }
}
