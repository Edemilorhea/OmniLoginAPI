using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoginAPI.Migrations
{
    /// <inheritdoc />
    public partial class UserHashData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserHashData",
                columns: table => new
                {
                    UserHashDataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserHashData", x => x.UserHashDataId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserHashData");
        }
    }
}
