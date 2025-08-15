BEGIN TRANSACTION;
GO

ALTER TABLE [RoleTypes] ADD [Module_Offer] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [Authorizations] ADD [Module_Offer] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

CREATE TABLE [Offers] (
    [ID] int NOT NULL IDENTITY,
    [UrunAdi] nvarchar(max) NOT NULL,
    [UrunKodu] nvarchar(max) NOT NULL,
    [Adet] int NOT NULL,
    [Fiyat] decimal(18,2) NOT NULL,
    [TeklifSartlari] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Offers] PRIMARY KEY ([ID])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250813155112_ver7', N'8.0.19');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Offers]') AND [c].[name] = N'Adet');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Offers] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Offers] DROP COLUMN [Adet];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Offers]') AND [c].[name] = N'Fiyat');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Offers] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Offers] DROP COLUMN [Fiyat];
GO

EXEC sp_rename N'[Offers].[UrunKodu]', N'cari_kod', N'COLUMN';
GO

EXEC sp_rename N'[Offers].[UrunAdi]', N'TeklifStatus', N'COLUMN';
GO

ALTER TABLE [Offers] ADD [MusteriAdi] nvarchar(max) NOT NULL DEFAULT N'';
GO

CREATE TABLE [OffersItem] (
    [ID] int NOT NULL IDENTITY,
    [UrunAdi] nvarchar(max) NOT NULL,
    [UrunKodu] nvarchar(max) NOT NULL,
    [Adet] int NOT NULL,
    [Fiyat] decimal(18,2) NOT NULL,
    [OfferID] int NOT NULL,
    CONSTRAINT [PK_OffersItem] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_OffersItem_Offers_OfferID] FOREIGN KEY ([OfferID]) REFERENCES [Offers] ([ID]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_OffersItem_OfferID] ON [OffersItem] ([OfferID]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250813173331_ver9', N'8.0.19');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Offers] ADD [cari_Guid] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250813184506_ver10', N'8.0.19');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Orders] (
    [ID] int NOT NULL IDENTITY,
    [MusteriAdi] nvarchar(max) NOT NULL,
    [TicariUnvan] nvarchar(max) NOT NULL,
    [VKNTCKN] nvarchar(max) NOT NULL,
    [FaturaAdresi] nvarchar(max) NOT NULL,
    [Il] nvarchar(max) NOT NULL,
    [Ilce] nvarchar(max) NOT NULL,
    [SaticiPlasiyer] nvarchar(max) NOT NULL,
    [TeslimatAdresi] nvarchar(max) NOT NULL,
    [TeslimatIl] nvarchar(max) NOT NULL,
    [TeslimatIlce] nvarchar(max) NOT NULL,
    [SiparisNumarasi] nvarchar(max) NOT NULL,
    [cari_Guid] uniqueidentifier NOT NULL,
    [cari_kod] nvarchar(max) NOT NULL,
    [AdSoyad] nvarchar(max) NOT NULL,
    [Eposta] nvarchar(max) NOT NULL,
    [Telefon] nvarchar(max) NOT NULL,
    [VadeTarihi] datetime2 NOT NULL,
    [TeslimTuru] nvarchar(max) NOT NULL,
    [SahaFirmasi] nvarchar(max) NOT NULL,
    [BankaOrtami] nvarchar(max) NOT NULL,
    [CihazModu] nvarchar(max) NOT NULL,
    [Entegrasyon] nvarchar(max) NOT NULL,
    [YuklenecekBanka] nvarchar(max) NOT NULL,
    [YuklenecekUygulama] nvarchar(max) NOT NULL,
    [Not] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY ([ID])
);
GO

CREATE TABLE [OrdersItem] (
    [ID] int NOT NULL IDENTITY,
    [CihazModeli] nvarchar(max) NOT NULL,
    [Miktar] int NOT NULL,
    [BirimFiyat] decimal(18,2) NOT NULL,
    [BirimFiyatTL] decimal(18,2) NOT NULL,
    [Kdv] nvarchar(max) NOT NULL,
    [Iskonto] nvarchar(max) NOT NULL,
    [ToplamTutar] nvarchar(max) NOT NULL,
    [OrderID] int NOT NULL,
    CONSTRAINT [PK_OrdersItem] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_OrdersItem_Orders_OrderID] FOREIGN KEY ([OrderID]) REFERENCES [Orders] ([ID]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_OrdersItem_OrderID] ON [OrdersItem] ([OrderID]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250814172952_ver11', N'8.0.19');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[OrdersItem]') AND [c].[name] = N'ToplamTutar');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [OrdersItem] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [OrdersItem] ALTER COLUMN [ToplamTutar] decimal(18,2) NOT NULL;
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Orders]') AND [c].[name] = N'VadeTarihi');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Orders] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Orders] ALTER COLUMN [VadeTarihi] nvarchar(max) NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250814174718_ver12', N'8.0.19');
GO

COMMIT;
GO

