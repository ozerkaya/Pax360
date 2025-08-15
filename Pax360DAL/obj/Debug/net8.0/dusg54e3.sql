BEGIN TRANSACTION;
GO

ALTER TABLE [Orders] ADD [UserID] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [Orders] ADD [UserName] nvarchar(max) NOT NULL DEFAULT N'';
GO

ALTER TABLE [Offers] ADD [TeklifTarihi] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

ALTER TABLE [Offers] ADD [UserID] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [Offers] ADD [UserName] nvarchar(max) NOT NULL DEFAULT N'';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250815112241_ver17', N'8.0.19');
GO

COMMIT;
GO

