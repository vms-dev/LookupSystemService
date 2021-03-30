using Microsoft.EntityFrameworkCore.Migrations;

namespace LookupSystem.DataAccess.Migrations
{
    public partial class AddnewTypeColumnToUser : Migration
    {
        //Variant A. Type column change by SQL script with cast data.
        //protected override void Up(MigrationBuilder migrationBuilder)
        //{
        //    migrationBuilder.Sql(@"
        //                        UPDATE [LookupSystemDatabase].[dbo].[Users]
        //                        SET Fired = CONVERT(INT, Fired)
        //                        ALTER TABLE [LookupSystemDatabase].[dbo].[Users]
        //                        ALTER COLUMN Fired INT;
        //                        ");

        //}

        //protected override void Down(MigrationBuilder migrationBuilder)
        //{
        //    migrationBuilder.Sql(@"
        //                        UPDATE [LookupSystemDatabase].[dbo].[Users]
        //                        SET Fired = CONVERT(BIT, Fired)
        //                        ALTER TABLE [LookupSystemDatabase].[dbo].[Users]
        //                        ALTER COLUMN Fired BIT;
        //                        ");
        //}


        //Variant B. Cast data do with SQL script and then use method "AlterColumn" .
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                                UPDATE [LookupSystemDatabase].[dbo].[Users]
                                SET Fired = CONVERT(INT, Fired);
                                ");

            migrationBuilder.AlterColumn<int>(
                name: "Fired",
                table: "Users",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                                UPDATE [LookupSystemDatabase].[dbo].[Users]
                                SET Fired = CONVERT(BIT, Fired);
                                ");

            migrationBuilder.AlterColumn<bool>(
                name: "Fired",
                table: "Users",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: false);
        }

    }
}
