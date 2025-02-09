using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PollutionTracker.Data.Migrations
{
    /// <inheritdoc />
    public partial class initialsetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    AreaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AreaName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.AreaID);
                });

            migrationBuilder.CreateTable(
                name: "AlertThresholds",
                columns: table => new
                {
                    ThresholdID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Parameter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThresholdValue = table.Column<double>(type: "float", nullable: false),
                    SeverityLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AreaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlertThresholds", x => x.ThresholdID);
                    table.ForeignKey(
                        name: "FK_AlertThresholds_Areas_AreaID",
                        column: x => x.AreaID,
                        principalTable: "Areas",
                        principalColumn: "AreaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sensors",
                columns: table => new
                {
                    SensorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SensorType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModelNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AreaID = table.Column<int>(type: "int", nullable: false),
                    AlertThresholdID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sensors", x => x.SensorID);
                    table.ForeignKey(
                        name: "FK_Sensors_AlertThresholds_AlertThresholdID",
                        column: x => x.AlertThresholdID,
                        principalTable: "AlertThresholds",
                        principalColumn: "ThresholdID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Sensors_Areas_AreaID",
                        column: x => x.AreaID,
                        principalTable: "Areas",
                        principalColumn: "AreaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pollutions",
                columns: table => new
                {
                    PollutionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SensorID = table.Column<int>(type: "int", nullable: false),
                    Temperature = table.Column<double>(type: "float", nullable: false),
                    Humidity = table.Column<double>(type: "float", nullable: false),
                    LPG_Isobutane = table.Column<double>(type: "float", nullable: false),
                    CarbonMonoxide = table.Column<double>(type: "float", nullable: false),
                    Hydrogen = table.Column<double>(type: "float", nullable: false),
                    CO2 = table.Column<double>(type: "float", nullable: false),
                    NH3 = table.Column<double>(type: "float", nullable: false),
                    RecordedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pollutions", x => x.PollutionID);
                    table.ForeignKey(
                        name: "FK_Pollutions_Sensors_SensorID",
                        column: x => x.SensorID,
                        principalTable: "Sensors",
                        principalColumn: "SensorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlertThresholds_AreaID",
                table: "AlertThresholds",
                column: "AreaID");

            migrationBuilder.CreateIndex(
                name: "IX_Pollutions_SensorID",
                table: "Pollutions",
                column: "SensorID");

            migrationBuilder.CreateIndex(
                name: "IX_Sensors_AlertThresholdID",
                table: "Sensors",
                column: "AlertThresholdID");

            migrationBuilder.CreateIndex(
                name: "IX_Sensors_AreaID",
                table: "Sensors",
                column: "AreaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pollutions");

            migrationBuilder.DropTable(
                name: "Sensors");

            migrationBuilder.DropTable(
                name: "AlertThresholds");

            migrationBuilder.DropTable(
                name: "Areas");
        }
    }
}
