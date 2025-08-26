BEGIN TRANSACTION;
GO

ALTER TABLE [Orders] ADD [SiparisTipi] nvarchar(500) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250819202703_ver24', N'8.0.19');
GO

COMMIT;
GO

