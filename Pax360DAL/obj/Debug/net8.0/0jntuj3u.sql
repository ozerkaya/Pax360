BEGIN TRANSACTION;
GO

ALTER TABLE [Orders] ADD [SiparisDurumu] nvarchar(500) NOT NULL DEFAULT N'';
GO

ALTER TABLE [Orders] ADD [SiparisTarihi] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250814184539_ver15', N'8.0.19');
GO

COMMIT;
GO

