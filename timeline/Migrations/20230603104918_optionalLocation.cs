using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace timeline.Migrations
{
    /// <inheritdoc />
    public partial class optionalLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Locations_LocationOfEventLocationId",
                table: "Events");

            migrationBuilder.AlterColumn<int>(
                name: "LocationOfEventLocationId",
                table: "Events",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Locations_LocationOfEventLocationId",
                table: "Events",
                column: "LocationOfEventLocationId",
                principalTable: "Locations",
                principalColumn: "LocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Locations_LocationOfEventLocationId",
                table: "Events");

            migrationBuilder.AlterColumn<int>(
                name: "LocationOfEventLocationId",
                table: "Events",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Locations_LocationOfEventLocationId",
                table: "Events",
                column: "LocationOfEventLocationId",
                principalTable: "Locations",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
