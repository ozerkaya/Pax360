BEGIN TRANSACTION;
GO

ALTER TABLE [Orders] ADD [FirmaTipi] nvarchar(500) NULL;
GO

ALTER TABLE [Orders] ADD [SiparisMusterisi] nvarchar(500) NOT NULL DEFAULT N'';
GO

ALTER TABLE [Orders] ADD [SiparisMusterisi_cari_Guid] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250818141648_ver22', N'8.0.19');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Orders]') AND [c].[name] = N'FirmaTipi');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Orders] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Orders] DROP COLUMN [FirmaTipi];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Orders]') AND [c].[name] = N'VadeTarihi');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Orders] DROP CONSTRAINT [' + @var1 + '];');
UPDATE [Orders] SET [VadeTarihi] = 0 WHERE [VadeTarihi] IS NULL;
ALTER TABLE [Orders] ALTER COLUMN [VadeTarihi] int NOT NULL;
ALTER TABLE [Orders] ADD DEFAULT 0 FOR [VadeTarihi];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250818153651_ver23', N'8.0.19');
GO

COMMIT;
GO

