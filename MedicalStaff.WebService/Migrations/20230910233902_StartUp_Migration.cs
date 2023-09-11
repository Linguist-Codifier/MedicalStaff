using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalStaff.WebService.Migrations
{
    /// <inheritdoc />
    public partial class StartUp_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PatientRecords",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<String>(type: "varchar(100)", maxLength: 100, nullable: false),
                    cpf = table.Column<String>(type: "varchar(11)", maxLength: 11, nullable: false),
                    picture_location = table.Column<String>(type: "varchar(300)", maxLength: 300, nullable: false),
                    phone_number = table.Column<String>(type: "char(14)", maxLength: 14, nullable: false),
                    birth_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    mail_address = table.Column<String>(type: "varchar(100)", nullable: false),
                    record_creation_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    address = table.Column<String>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientRecords", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PatientsAccounts",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    cpf = table.Column<String>(type: "varchar(11)", maxLength: 11, nullable: false),
                    name = table.Column<String>(type: "varchar(100)", maxLength: 100, nullable: false),
                    password = table.Column<String>(type: "varchar(100)", maxLength: 100, nullable: false),
                    email = table.Column<String>(type: "varchar(100)", maxLength: 100, nullable: false),
                    role = table.Column<String>(type: "char(18)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientsAccounts", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PhysicianAccounts",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    crm = table.Column<String>(type: "varchar(13)", maxLength: 13, nullable: false),
                    cpf = table.Column<String>(type: "varchar(11)", maxLength: 11, nullable: false),
                    name = table.Column<String>(type: "varchar(100)", maxLength: 100, nullable: false),
                    email = table.Column<String>(type: "varchar(100)", maxLength: 100, nullable: false),
                    password = table.Column<String>(type: "varchar(100)", maxLength: 100, nullable: false),
                    role = table.Column<String>(type: "char(18)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhysicianAccounts", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatientRecords");

            migrationBuilder.DropTable(
                name: "PatientsAccounts");

            migrationBuilder.DropTable(
                name: "PhysicianAccounts");
        }
    }
}
