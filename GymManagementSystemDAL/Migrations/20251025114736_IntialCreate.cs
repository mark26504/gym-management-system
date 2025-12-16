using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManagementSystemDAL.Migrations
{
    /// <inheritdoc />
    public partial class IntialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Height = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BloodType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JoinDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Address_BuildingNumber = table.Column<int>(type: "int", nullable: false),
                    Street = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    City = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                    table.CheckConstraint("CK_ValidEmailCheck", "Email Like '_%@_%._%'");
                    table.CheckConstraint("CK_ValidPhoneCheck", "Phone Like '01%' and Phone Not Like '%[^0-9]%'");
                });

            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    DurationDays = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.Id);
                    table.CheckConstraint("CK_Plan_DurationDays", "DurationDays Between 1 and 356");
                });

            migrationBuilder.CreateTable(
                name: "Trainers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Specialties = table.Column<int>(type: "int", nullable: false),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Address_BuildingNumber = table.Column<int>(type: "int", nullable: false),
                    Street = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    City = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainers", x => x.Id);
                    table.CheckConstraint("CK_ValidEmailCheck1", "Email Like '_%@_%._%'");
                    table.CheckConstraint("CK_ValidPhoneCheck1", "Phone Like '01%' and Phone Not Like '%[^0-9]%'");
                });

            migrationBuilder.CreateTable(
                name: "Memberships",
                columns: table => new
                {
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    PlanId = table.Column<int>(type: "int", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Memberships", x => new { x.MemberId, x.PlanId });
                    table.ForeignKey(
                        name: "FK_Memberships_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Memberships_Plans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    TrainerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                    table.CheckConstraint("CK_Session_Capacity", "Capacity Between 1 and 25");
                    table.CheckConstraint("CK_Session_Date", "EndDate > StartDate");
                    table.ForeignKey(
                        name: "FK_Sessions_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sessions_Trainers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Trainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MemberSessions",
                columns: table => new
                {
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    SessionId = table.Column<int>(type: "int", nullable: false),
                    IsAttended = table.Column<bool>(type: "bit", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberSessions", x => new { x.MemberId, x.SessionId });
                    table.ForeignKey(
                        name: "FK_MemberSessions_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MemberSessions_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Members_Email",
                table: "Members",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Members_Phone",
                table: "Members",
                column: "Phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MemberSessions_SessionId",
                table: "MemberSessions",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_PlanId",
                table: "Memberships",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_CategoryId",
                table: "Sessions",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_TrainerId",
                table: "Sessions",
                column: "TrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_Trainers_Email",
                table: "Trainers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trainers_Phone",
                table: "Trainers",
                column: "Phone",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MemberSessions");

            migrationBuilder.DropTable(
                name: "Memberships");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "Plans");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Trainers");
        }
    }
}
