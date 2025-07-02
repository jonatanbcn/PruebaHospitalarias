using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Paciente",
                columns: table => new
                {
                    NHC = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOMBRE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    APELLIDO1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    APELLIDO2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DNI = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    CIP = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    EDAD = table.Column<int>(type: "int", nullable: false, computedColumnSql: "(datediff(year,[FECHA_NACIMIENTO],getdate())-case when datepart(month,getdate())<datepart(month,[FECHA_NACIMIENTO]) OR datepart(month,getdate())=datepart(month,[FECHA_NACIMIENTO]) AND datepart(day,getdate())<datepart(day,[FECHA_NACIMIENTO]) then (1) else (0) end)", stored: false),
                    FECHA_NACIMIENTO = table.Column<DateTime>(type: "date", nullable: false),
                    SEXO = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false),
                    OBSERVACIONES = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Paciente__C7DEDB1337829F7B", x => x.NHC);
                });

            migrationBuilder.CreateIndex(
                name: "UQ__Paciente__C035B8DD999F0568",
                table: "Paciente",
                column: "DNI",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Paciente");
        }
    }
}
