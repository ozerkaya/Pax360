BEGIN TRANSACTION;
GO

ALTER TABLE [OrdersItem] ADD [DovizCinsi] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250818115031_ver21', N'8.0.19');
GO

COMMIT;
GO

