using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuantityMeasurementRepositoryLayer.Migrations
{
    /// <inheritdoc />
    public partial class InitialApiMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Measurements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OperationType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsSuccess = table.Column<bool>(type: "bit", nullable: false),
                    ErrorMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value1 = table.Column<double>(type: "float", nullable: false),
                    Unit1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value2 = table.Column<double>(type: "float", nullable: false),
                    Unit2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TargetUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdditionResult = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubtractionResult = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DivisionResult = table.Column<double>(type: "float", nullable: false),
                    IsEqual = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measurements", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Measurements");
        }
    }
}
