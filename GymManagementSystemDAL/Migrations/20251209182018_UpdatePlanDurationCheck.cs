using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManagementSystemDAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePlanDurationCheck : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Plan_DurationDays",
                table: "Plans");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Plan_DurationDays",
                table: "Plans",
                sql: "DurationDays Between 1 and 365");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Plan_DurationDays",
                table: "Plans");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Plan_DurationDays",
                table: "Plans",
                sql: "DurationDays Between 1 and 356");
        }
    }
}
