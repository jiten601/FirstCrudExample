using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirstCrudExample.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Faculty",
                columns: table => new
                {
                    facultyId = table.Column<int>(type: "int", nullable: false),
                    facultyName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Faculty__DBBF6FB15141C888", x => x.facultyId);
                });

            migrationBuilder.CreateTable(
                name: "USERTYPE",
                columns: table => new
                {
                    typeId = table.Column<int>(type: "int", nullable: false),
                    typeName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__USERNAME__F04DF13AE90FA02B", x => x.typeId);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    stdId = table.Column<long>(type: "bigint", nullable: false),
                    stdName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    stdAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    joinDate = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "(getdate())"),
                    facultyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Student__BA08FEBB4FA7AD26", x => x.stdId);
                    table.ForeignKey(
                        name: "FK__Student__faculty__3B75D760",
                        column: x => x.facultyId,
                        principalTable: "Faculty",
                        principalColumn: "facultyId");
                });

            migrationBuilder.CreateTable(
                name: "USERLIST",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int", nullable: false),
                    userName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    phoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    userPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    loginStatus = table.Column<bool>(type: "bit", nullable: true),
                    userTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__USERLIST__CB9A1CFF506442B1", x => x.userId);
                    table.ForeignKey(
                        name: "FK__USERLIST__userTy__59FA5E80",
                        column: x => x.userTypeId,
                        principalTable: "USERTYPE",
                        principalColumn: "typeId");
                });

            migrationBuilder.CreateIndex(
                name: "UQ__Faculty__53353D25C4E9674C",
                table: "Faculty",
                column: "facultyName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Student_facultyId",
                table: "Student",
                column: "facultyId");

            migrationBuilder.CreateIndex(
                name: "IX_USERLIST_userTypeId",
                table: "USERLIST",
                column: "userTypeId");

            migrationBuilder.CreateIndex(
                name: "UQ__USERLIST__4849DA013F2E9EF7",
                table: "USERLIST",
                column: "phoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__USERNAME__A20CDB58027F8CA3",
                table: "USERTYPE",
                column: "typeName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "USERLIST");

            migrationBuilder.DropTable(
                name: "Faculty");

            migrationBuilder.DropTable(
                name: "USERTYPE");
        }
    }
}
