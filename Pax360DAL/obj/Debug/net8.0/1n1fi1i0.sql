BEGIN TRANSACTION;
GO

CREATE TABLE [Customers] (
    [ID] int NOT NULL IDENTITY,
    [Cari_Guid_Mikro] uniqueidentifier NOT NULL,
    [MusteriSegmenti] nvarchar(500) NULL,
    [MusteriSektoru] nvarchar(500) NULL,
    [MagazaSayisi] int NOT NULL,
    [KasaSayisi] int NOT NULL,
    [AccountManager] nvarchar(500) NULL,
    [AccountManagerID] int NOT NULL,
    [SatisKanali] nvarchar(500) NULL,
    [SonAktiviteNumarasi] nvarchar(500) NULL,
    [SonAktiviteTarihi] datetime2 NOT NULL,
    [SonAktiviteTipi] nvarchar(500) NULL,
    [SonAktiviteOzeti] nvarchar(500) NULL,
    [SahaFirmasi] nvarchar(500) NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY ([ID])
);
GO

CREATE TABLE [CustomerBanks] (
    [ID] int NOT NULL IDENTITY,
    [BankName] nvarchar(max) NOT NULL,
    [CustomerID] int NOT NULL,
    CONSTRAINT [PK_CustomerBanks] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_CustomerBanks_Customers_CustomerID] FOREIGN KEY ([CustomerID]) REFERENCES [Customers] ([ID]) ON DELETE CASCADE
);
GO

CREATE TABLE [CustomerCases] (
    [ID] int NOT NULL IDENTITY,
    [CaseCompany] nvarchar(max) NOT NULL,
    [CustomerID] int NOT NULL,
    CONSTRAINT [PK_CustomerCases] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_CustomerCases_Customers_CustomerID] FOREIGN KEY ([CustomerID]) REFERENCES [Customers] ([ID]) ON DELETE CASCADE
);
GO

CREATE TABLE [CustomerDocuments] (
    [ID] int NOT NULL IDENTITY,
    [DocumentName] nvarchar(max) NOT NULL,
    [CustomerID] int NOT NULL,
    CONSTRAINT [PK_CustomerDocuments] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_CustomerDocuments_Customers_CustomerID] FOREIGN KEY ([CustomerID]) REFERENCES [Customers] ([ID]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_CustomerBanks_CustomerID] ON [CustomerBanks] ([CustomerID]);
GO

CREATE INDEX [IX_CustomerCases_CustomerID] ON [CustomerCases] ([CustomerID]);
GO

CREATE INDEX [IX_CustomerDocuments_CustomerID] ON [CustomerDocuments] ([CustomerID]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250822184929_ver25', N'8.0.19');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [RoleTypes] ADD [Module_Customers] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [Authorizations] ADD [Module_Customers] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250822185646_ver26', N'8.0.19');
GO

COMMIT;
GO

