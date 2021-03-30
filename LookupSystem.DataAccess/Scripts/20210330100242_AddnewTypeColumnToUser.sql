--Result run command 'Script-Migration'

BEGIN TRANSACTION;
GO


                                UPDATE [LookupSystemDatabase].[dbo].[Users]
                                SET Fired = CONVERT(INT, Fired);
                                
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'Fired');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Users] ALTER COLUMN [Fired] int NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210330100242_AddnewTypeColumnToUser', N'5.0.3');
GO

COMMIT;
GO

