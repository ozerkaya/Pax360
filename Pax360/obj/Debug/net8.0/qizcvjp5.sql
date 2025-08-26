BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[OrdersItem]') AND [c].[name] = N'Kdv');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [OrdersItem] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [OrdersItem] ALTER COLUMN [Kdv] nvarchar(max) NULL;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[OrdersItem]') AND [c].[name] = N'Iskonto');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [OrdersItem] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [OrdersItem] ALTER COLUMN [Iskonto] nvarchar(max) NULL;
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[OrdersItem]') AND [c].[name] = N'CihazModeli');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [OrdersItem] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [OrdersItem] ALTER COLUMN [CihazModeli] nvarchar(max) NULL;
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[OffersItem]') AND [c].[name] = N'UrunKodu');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [OffersItem] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [OffersItem] ALTER COLUMN [UrunKodu] nvarchar(max) NULL;
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[OffersItem]') AND [c].[name] = N'UrunAdi');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [OffersItem] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [OffersItem] ALTER COLUMN [UrunAdi] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250815140109_ver18', N'8.0.19');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Orders]') AND [c].[name] = N'YuklenecekUygulama');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Orders] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [Orders] DROP COLUMN [YuklenecekUygulama];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250818072702_ver19', N'8.0.19');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[OrdersItem]') AND [c].[name] = N'Iskonto');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [OrdersItem] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [OrdersItem] DROP COLUMN [Iskonto];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250818090839_ver20', N'8.0.19');
GO

COMMIT;
GO

