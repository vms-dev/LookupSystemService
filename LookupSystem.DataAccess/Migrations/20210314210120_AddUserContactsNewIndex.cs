using Microsoft.EntityFrameworkCore.Migrations;

namespace LookupSystem.DataAccess.Migrations
{
    public partial class AddUserContactsNewIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserContacts_Phone",
                table: "UserContacts");

            migrationBuilder.CreateIndex(
                name: "IX_UserContacts_Phone_Email",
                table: "UserContacts",
                columns: new[] { "Phone", "Email" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserContacts_Phone_Email",
                table: "UserContacts");

            migrationBuilder.CreateIndex(
                name: "IX_UserContacts_Phone",
                table: "UserContacts",
                column: "Phone");
        }
    }
}
