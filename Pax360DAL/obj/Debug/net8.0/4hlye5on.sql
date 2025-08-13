BEGIN TRANSACTION;
GO

ALTER TABLE [Users] ADD [SipSaticiKodu] nvarchar(max) NOT NULL DEFAULT N'';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250813124505_ver6', N'8.0.19');
GO

COMMIT;
GO

