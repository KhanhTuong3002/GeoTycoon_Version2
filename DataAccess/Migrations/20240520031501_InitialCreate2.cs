using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_AspNetUsers_UserId",
                table: "Profiles");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "59894d38-76db-45eb-bd32-e8bdb1bbbd8b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "73bdd012-86fa-416e-908f-16ea15d6e171");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9bcbe916-2341-4d7a-9269-5c05cbf60aaf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dd44481f-50d5-49b2-a57e-20c7c58e89d0");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "61a92486-d810-4067-8610-915f5671f1be", null, "Teacher", "TEACHER" },
                    { "72bdab57-e8bd-4b89-a709-ec4d43a06b3c", null, "Pending", "PENDING" },
                    { "bfe568b4-613e-4676-9aac-b235b6261125", null, "Student", "STUDENT" },
                    { "e1826c1d-f88a-4c36-9f86-82106581ea0f", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_AspNetUsers_UserId",
                table: "Profiles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_AspNetUsers_UserId",
                table: "Profiles");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "61a92486-d810-4067-8610-915f5671f1be");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "72bdab57-e8bd-4b89-a709-ec4d43a06b3c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bfe568b4-613e-4676-9aac-b235b6261125");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e1826c1d-f88a-4c36-9f86-82106581ea0f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "59894d38-76db-45eb-bd32-e8bdb1bbbd8b", null, "Student", "STUDENT" },
                    { "73bdd012-86fa-416e-908f-16ea15d6e171", null, "Administrator", "ADMINISTRATOR" },
                    { "9bcbe916-2341-4d7a-9269-5c05cbf60aaf", null, "Pending", "PENDING" },
                    { "dd44481f-50d5-49b2-a57e-20c7c58e89d0", null, "Teacher", "TEACHER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_AspNetUsers_UserId",
                table: "Profiles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
