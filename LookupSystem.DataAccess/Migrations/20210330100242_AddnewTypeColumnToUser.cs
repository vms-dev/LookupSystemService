using Microsoft.EntityFrameworkCore.Migrations;

namespace LookupSystem.DataAccess.Migrations
{
    public partial class AddnewTypeColumnToUser : Migration
    {
        //Variant A. Type column change by SQL script with cast data.
        //Cast is not need for this case but important for case where we can lose data. 
        //protected override void Up(MigrationBuilder migrationBuilder)
        //{
        //    migrationBuilder.Sql(@"
        //                        UPDATE [LookupSystemDatabase].[dbo].[Users]
        //                        SET Fired = CONVERT(INT, Fired)
        //                        ALTER TABLE [LookupSystemDatabase].[dbo].[Users]
        //                        ALTER COLUMN Fired INT NOT NULL;
        //                        ");

        //}

        //protected override void Down(MigrationBuilder migrationBuilder)
        //{
        //    migrationBuilder.Sql(@"
        //                        UPDATE [LookupSystemDatabase].[dbo].[Users]
        //                        SET Fired = CONVERT(BIT, Fired)
        //                        ALTER TABLE [LookupSystemDatabase].[dbo].[Users]
        //                        ALTER COLUMN Fired BIT NOT NULL;
        //                        ");
        //}


        //Variant B. Cast data do with SQL script and then use method "AlterColumn".
        //Cast is not need for this case but important for case where we can lose data. 
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

/*
 Variant A
---Duration in the Profiler: 3 
UPDATE [LookupSystemDatabase].[dbo].[Users]
SET Fired = CONVERT(INT, Fired)
ALTER TABLE [LookupSystemDatabase].[dbo].[Users]
ALTER COLUMN Fired INT;
  

Variant B
---Duration in the Profiler: 13 (once 24)
DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'Fired');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Users] ALTER COLUMN [Fired] int NOT NULL;

 */
