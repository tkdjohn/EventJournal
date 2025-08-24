using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventJournal.Data.Migrations
{
    /// <inheritdoc />
    public partial class fixDetailTypeIntensityRelationshipAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Intensities_DetailTypes_DetailTypeId",
                table: "Intensities");

            migrationBuilder.AlterColumn<int>(
                name: "DetailTypeId",
                table: "Intensities",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Intensities_DetailTypes_DetailTypeId",
                table: "Intensities",
                column: "DetailTypeId",
                principalTable: "DetailTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Intensities_DetailTypes_DetailTypeId",
                table: "Intensities");

            migrationBuilder.AlterColumn<int>(
                name: "DetailTypeId",
                table: "Intensities",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Intensities_DetailTypes_DetailTypeId",
                table: "Intensities",
                column: "DetailTypeId",
                principalTable: "DetailTypes",
                principalColumn: "Id");
        }
    }
}
