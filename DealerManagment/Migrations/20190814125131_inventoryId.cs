using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DealerManagment.Migrations
{
    public partial class inventoryId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_AspNetUsers_UserId",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Vehicle_VIN",
                table: "Inventory");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Inventory_UserId_VIN",
                table: "Inventory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Inventory",
                table: "Inventory");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Inventory",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "VIN",
                table: "Inventory",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "InventoryId",
                table: "Inventory",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Inventory",
                table: "Inventory",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_UserId",
                table: "Inventory",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_VIN",
                table: "Inventory",
                column: "VIN");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_AspNetUsers_UserId",
                table: "Inventory",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Vehicle_VIN",
                table: "Inventory",
                column: "VIN",
                principalTable: "Vehicle",
                principalColumn: "VIN",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_AspNetUsers_UserId",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Vehicle_VIN",
                table: "Inventory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Inventory",
                table: "Inventory");

            migrationBuilder.DropIndex(
                name: "IX_Inventory_UserId",
                table: "Inventory");

            migrationBuilder.DropIndex(
                name: "IX_Inventory_VIN",
                table: "Inventory");

            migrationBuilder.DropColumn(
                name: "InventoryId",
                table: "Inventory");

            migrationBuilder.AlterColumn<string>(
                name: "VIN",
                table: "Inventory",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Inventory",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Inventory_UserId_VIN",
                table: "Inventory",
                columns: new[] { "UserId", "VIN" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Inventory",
                table: "Inventory",
                columns: new[] { "VIN", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_AspNetUsers_UserId",
                table: "Inventory",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Vehicle_VIN",
                table: "Inventory",
                column: "VIN",
                principalTable: "Vehicle",
                principalColumn: "VIN",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
