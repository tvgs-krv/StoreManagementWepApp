using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockData.Api.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StockData",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StoreItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    Backstory = table.Column<int>(type: "integer", nullable: false),
                    Frontstore = table.Column<int>(type: "integer", nullable: false),
                    ShoppingWindow = table.Column<int>(type: "integer", nullable: false),
                    TotalStock = table.Column<double>(type: "double precision", nullable: false),
                    Accuracy = table.Column<double>(type: "double precision", nullable: false),
                    OnFloorAvailability = table.Column<double>(type: "double precision", nullable: false),
                    MeanAge = table.Column<int>(type: "integer", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockData", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockData_Id",
                table: "StockData",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockData");
        }
    }
}
