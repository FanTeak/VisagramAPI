using Microsoft.EntityFrameworkCore.Migrations;

namespace VisagramAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SalaryOffers",
                columns: table => new
                {
                    SalaryOfferId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfferName = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    OfferValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryOffers", x => x.SalaryOfferId);
                });

            migrationBuilder.CreateTable(
                name: "Staffs",
                columns: table => new
                {
                    StaffId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffName = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    StaffSurname = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(12)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffs", x => x.StaffId);
                });

            migrationBuilder.CreateTable(
                name: "SalaryPayments",
                columns: table => new
                {
                    PaymentId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentNumber = table.Column<string>(type: "nvarchar(75)", nullable: true),
                    StaffId = table.Column<int>(type: "int", nullable: false),
                    PaymentType = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryPayments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_SalaryPayments_Staffs_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staffs",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalaryDetails",
                columns: table => new
                {
                    SalaryDetailsId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalaryPaymentId = table.Column<long>(type: "bigint", nullable: false),
                    SalaryOfferId = table.Column<int>(type: "int", nullable: false),
                    SalaryOfferValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryDetails", x => x.SalaryDetailsId);
                    table.ForeignKey(
                        name: "FK_SalaryDetails_SalaryOffers_SalaryOfferId",
                        column: x => x.SalaryOfferId,
                        principalTable: "SalaryOffers",
                        principalColumn: "SalaryOfferId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalaryDetails_SalaryPayments_SalaryPaymentId",
                        column: x => x.SalaryPaymentId,
                        principalTable: "SalaryPayments",
                        principalColumn: "PaymentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalaryDetails_SalaryOfferId",
                table: "SalaryDetails",
                column: "SalaryOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryDetails_SalaryPaymentId",
                table: "SalaryDetails",
                column: "SalaryPaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryPayments_StaffId",
                table: "SalaryPayments",
                column: "StaffId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalaryDetails");

            migrationBuilder.DropTable(
                name: "SalaryOffers");

            migrationBuilder.DropTable(
                name: "SalaryPayments");

            migrationBuilder.DropTable(
                name: "Staffs");
        }
    }
}
