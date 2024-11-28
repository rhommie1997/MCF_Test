using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MCF_TestAPI.Migrations
{
    public partial class firstDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ms_storage_locations",
                columns: table => new
                {
                    location_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    location_name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ms_storage_locations", x => x.location_id);
                });

            migrationBuilder.CreateTable(
                name: "ms_users",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ms_users", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "tr_bpkbs",
                columns: table => new
                {
                    agreement_number = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    bpkb_no = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    branch_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    bpkb_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    faktur_no = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    faktur_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    location_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    police_no = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    bpkb_date_in = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_on = table.Column<DateTime>(type: "datetime2", nullable: false),
                    last_updated_by = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    last_updated_on = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tr_bpkbs", x => x.agreement_number);
                    table.ForeignKey(
                        name: "FK_tr_bpkbs_ms_storage_locations_location_id",
                        column: x => x.location_id,
                        principalTable: "ms_storage_locations",
                        principalColumn: "location_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "ms_storage_locations",
                columns: new[] { "location_id", "location_name" },
                values: new object[] { "TEST1", "TEST 1" });

            migrationBuilder.InsertData(
                table: "ms_storage_locations",
                columns: new[] { "location_id", "location_name" },
                values: new object[] { "TEST2", "TEST 2" });

            migrationBuilder.InsertData(
                table: "ms_users",
                columns: new[] { "user_id", "isActive", "password", "user_name" },
                values: new object[] { 1, true, "123", "rhommie" });

            migrationBuilder.CreateIndex(
                name: "IX_tr_bpkbs_location_id",
                table: "tr_bpkbs",
                column: "location_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ms_users");

            migrationBuilder.DropTable(
                name: "tr_bpkbs");

            migrationBuilder.DropTable(
                name: "ms_storage_locations");
        }
    }
}
