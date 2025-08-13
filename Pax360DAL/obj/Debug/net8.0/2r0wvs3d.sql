IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Authorizations] (
    [ID] int NOT NULL IDENTITY,
    [UserID] int NOT NULL,
    [Module_Users] bit NOT NULL,
    [Module_Role] bit NOT NULL,
    [Module_IsEmirleri] bit NOT NULL,
    [Module_Loglar] bit NOT NULL,
    [Module_DosyaYukleme] bit NOT NULL,
    [Module_Malzeme] bit NOT NULL,
    [Module_ManuelIsEmri] bit NOT NULL,
    [Module_Tatiller] bit NOT NULL,
    [Module_TeknisyenAdresleri] bit NOT NULL,
    [Module_Raporlar] bit NOT NULL,
    [Module_Partner] bit NOT NULL,
    [Module_Uygulamalar] bit NOT NULL,
    [Module_Raporlar_Portfoy] bit NOT NULL,
    [Module_Raporlar_Bolgesel] bit NOT NULL,
    [Module_SatisZiyaret_Listesi] bit NOT NULL,
    [Module_Satis_Listesi] bit NOT NULL,
    [Module_Fiyatlar] bit NOT NULL,
    [Module_Kargo] bit NOT NULL,
    [Module_PropaySatis] bit NOT NULL,
    [Module_PropaySatisList] bit NOT NULL,
    [Module_TaksidePosGiris] bit NOT NULL,
    CONSTRAINT [PK_Authorizations] PRIMARY KEY ([ID])
);
GO

CREATE TABLE [RoleTypes] (
    [ID] int NOT NULL IDENTITY,
    [RoleName] nvarchar(max) NOT NULL,
    [Module_Users] bit NOT NULL,
    [Module_Role] bit NOT NULL,
    [Module_IsEmirleri] bit NOT NULL,
    [Module_Loglar] bit NOT NULL,
    [Module_DosyaYukleme] bit NOT NULL,
    [Module_Malzeme] bit NOT NULL,
    [Module_ManuelIsEmri] bit NOT NULL,
    [Module_Tatiller] bit NOT NULL,
    [Module_TeknisyenAdresleri] bit NOT NULL,
    [Module_Raporlar] bit NOT NULL,
    [Module_Partner] bit NOT NULL,
    [Module_Uygulamalar] bit NOT NULL,
    [Module_Raporlar_Portfoy] bit NOT NULL,
    [Module_Raporlar_Bolgesel] bit NOT NULL,
    [Module_SatisZiyaret_Listesi] bit NOT NULL,
    [Module_Satis_Listesi] bit NOT NULL,
    [Module_Fiyatlar] bit NOT NULL,
    [Module_Kargo] bit NOT NULL,
    [Module_PropaySatis] bit NOT NULL,
    [Module_PropaySatisList] bit NOT NULL,
    [Module_TaksidePosGiris] bit NOT NULL,
    CONSTRAINT [PK_RoleTypes] PRIMARY KEY ([ID])
);
GO

CREATE TABLE [Teams] (
    [ID] int NOT NULL IDENTITY,
    [TeamName] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Teams] PRIMARY KEY ([ID])
);
GO

CREATE TABLE [Users] (
    [ID] int NOT NULL IDENTITY,
    [Username] nvarchar(max) NOT NULL,
    [Password] nvarchar(max) NOT NULL,
    [NameSurname] nvarchar(max) NOT NULL,
    [Role] nvarchar(max) NOT NULL,
    [IsEnable] bit NOT NULL,
    [TeamName] nvarchar(max) NOT NULL,
    [PhoneNumber] nvarchar(max) NOT NULL,
    [Image] nvarchar(max) NOT NULL,
    [TCKN] nvarchar(max) NOT NULL,
    [City] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([ID])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250805075310_ver1', N'8.0.19');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'City');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Users] DROP COLUMN [City];
GO

EXEC sp_rename N'[Users].[Image]', N'MikroCompanyName', N'COLUMN';
GO

ALTER TABLE [Users] ADD [MikroCompanyID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250805094026_ver2', N'8.0.19');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Users] ADD [AuthPersonID] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [Users] ADD [AuthPersonName] nvarchar(max) NOT NULL DEFAULT N'';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250805143446_ver3', N'8.0.19');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'MikroCompanyName');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Users] ALTER COLUMN [MikroCompanyName] nvarchar(max) NULL;
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'AuthPersonName');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Users] ALTER COLUMN [AuthPersonName] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250805144455_ver4', N'8.0.19');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[RoleTypes]') AND [c].[name] = N'Module_DosyaYukleme');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [RoleTypes] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [RoleTypes] DROP COLUMN [Module_DosyaYukleme];
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[RoleTypes]') AND [c].[name] = N'Module_Fiyatlar');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [RoleTypes] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [RoleTypes] DROP COLUMN [Module_Fiyatlar];
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[RoleTypes]') AND [c].[name] = N'Module_IsEmirleri');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [RoleTypes] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [RoleTypes] DROP COLUMN [Module_IsEmirleri];
GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[RoleTypes]') AND [c].[name] = N'Module_Kargo');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [RoleTypes] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [RoleTypes] DROP COLUMN [Module_Kargo];
GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[RoleTypes]') AND [c].[name] = N'Module_Loglar');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [RoleTypes] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [RoleTypes] DROP COLUMN [Module_Loglar];
GO

DECLARE @var8 sysname;
SELECT @var8 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[RoleTypes]') AND [c].[name] = N'Module_Malzeme');
IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [RoleTypes] DROP CONSTRAINT [' + @var8 + '];');
ALTER TABLE [RoleTypes] DROP COLUMN [Module_Malzeme];
GO

DECLARE @var9 sysname;
SELECT @var9 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[RoleTypes]') AND [c].[name] = N'Module_ManuelIsEmri');
IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [RoleTypes] DROP CONSTRAINT [' + @var9 + '];');
ALTER TABLE [RoleTypes] DROP COLUMN [Module_ManuelIsEmri];
GO

DECLARE @var10 sysname;
SELECT @var10 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[RoleTypes]') AND [c].[name] = N'Module_Partner');
IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [RoleTypes] DROP CONSTRAINT [' + @var10 + '];');
ALTER TABLE [RoleTypes] DROP COLUMN [Module_Partner];
GO

DECLARE @var11 sysname;
SELECT @var11 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[RoleTypes]') AND [c].[name] = N'Module_PropaySatis');
IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [RoleTypes] DROP CONSTRAINT [' + @var11 + '];');
ALTER TABLE [RoleTypes] DROP COLUMN [Module_PropaySatis];
GO

DECLARE @var12 sysname;
SELECT @var12 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[RoleTypes]') AND [c].[name] = N'Module_PropaySatisList');
IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [RoleTypes] DROP CONSTRAINT [' + @var12 + '];');
ALTER TABLE [RoleTypes] DROP COLUMN [Module_PropaySatisList];
GO

DECLARE @var13 sysname;
SELECT @var13 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[RoleTypes]') AND [c].[name] = N'Module_Raporlar');
IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [RoleTypes] DROP CONSTRAINT [' + @var13 + '];');
ALTER TABLE [RoleTypes] DROP COLUMN [Module_Raporlar];
GO

DECLARE @var14 sysname;
SELECT @var14 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[RoleTypes]') AND [c].[name] = N'Module_Raporlar_Bolgesel');
IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [RoleTypes] DROP CONSTRAINT [' + @var14 + '];');
ALTER TABLE [RoleTypes] DROP COLUMN [Module_Raporlar_Bolgesel];
GO

DECLARE @var15 sysname;
SELECT @var15 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[RoleTypes]') AND [c].[name] = N'Module_Raporlar_Portfoy');
IF @var15 IS NOT NULL EXEC(N'ALTER TABLE [RoleTypes] DROP CONSTRAINT [' + @var15 + '];');
ALTER TABLE [RoleTypes] DROP COLUMN [Module_Raporlar_Portfoy];
GO

DECLARE @var16 sysname;
SELECT @var16 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[RoleTypes]') AND [c].[name] = N'Module_SatisZiyaret_Listesi');
IF @var16 IS NOT NULL EXEC(N'ALTER TABLE [RoleTypes] DROP CONSTRAINT [' + @var16 + '];');
ALTER TABLE [RoleTypes] DROP COLUMN [Module_SatisZiyaret_Listesi];
GO

DECLARE @var17 sysname;
SELECT @var17 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[RoleTypes]') AND [c].[name] = N'Module_Satis_Listesi');
IF @var17 IS NOT NULL EXEC(N'ALTER TABLE [RoleTypes] DROP CONSTRAINT [' + @var17 + '];');
ALTER TABLE [RoleTypes] DROP COLUMN [Module_Satis_Listesi];
GO

DECLARE @var18 sysname;
SELECT @var18 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[RoleTypes]') AND [c].[name] = N'Module_TaksidePosGiris');
IF @var18 IS NOT NULL EXEC(N'ALTER TABLE [RoleTypes] DROP CONSTRAINT [' + @var18 + '];');
ALTER TABLE [RoleTypes] DROP COLUMN [Module_TaksidePosGiris];
GO

DECLARE @var19 sysname;
SELECT @var19 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[RoleTypes]') AND [c].[name] = N'Module_Tatiller');
IF @var19 IS NOT NULL EXEC(N'ALTER TABLE [RoleTypes] DROP CONSTRAINT [' + @var19 + '];');
ALTER TABLE [RoleTypes] DROP COLUMN [Module_Tatiller];
GO

DECLARE @var20 sysname;
SELECT @var20 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[RoleTypes]') AND [c].[name] = N'Module_TeknisyenAdresleri');
IF @var20 IS NOT NULL EXEC(N'ALTER TABLE [RoleTypes] DROP CONSTRAINT [' + @var20 + '];');
ALTER TABLE [RoleTypes] DROP COLUMN [Module_TeknisyenAdresleri];
GO

DECLARE @var21 sysname;
SELECT @var21 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Authorizations]') AND [c].[name] = N'Module_DosyaYukleme');
IF @var21 IS NOT NULL EXEC(N'ALTER TABLE [Authorizations] DROP CONSTRAINT [' + @var21 + '];');
ALTER TABLE [Authorizations] DROP COLUMN [Module_DosyaYukleme];
GO

DECLARE @var22 sysname;
SELECT @var22 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Authorizations]') AND [c].[name] = N'Module_Fiyatlar');
IF @var22 IS NOT NULL EXEC(N'ALTER TABLE [Authorizations] DROP CONSTRAINT [' + @var22 + '];');
ALTER TABLE [Authorizations] DROP COLUMN [Module_Fiyatlar];
GO

DECLARE @var23 sysname;
SELECT @var23 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Authorizations]') AND [c].[name] = N'Module_IsEmirleri');
IF @var23 IS NOT NULL EXEC(N'ALTER TABLE [Authorizations] DROP CONSTRAINT [' + @var23 + '];');
ALTER TABLE [Authorizations] DROP COLUMN [Module_IsEmirleri];
GO

DECLARE @var24 sysname;
SELECT @var24 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Authorizations]') AND [c].[name] = N'Module_Kargo');
IF @var24 IS NOT NULL EXEC(N'ALTER TABLE [Authorizations] DROP CONSTRAINT [' + @var24 + '];');
ALTER TABLE [Authorizations] DROP COLUMN [Module_Kargo];
GO

DECLARE @var25 sysname;
SELECT @var25 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Authorizations]') AND [c].[name] = N'Module_Loglar');
IF @var25 IS NOT NULL EXEC(N'ALTER TABLE [Authorizations] DROP CONSTRAINT [' + @var25 + '];');
ALTER TABLE [Authorizations] DROP COLUMN [Module_Loglar];
GO

DECLARE @var26 sysname;
SELECT @var26 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Authorizations]') AND [c].[name] = N'Module_Malzeme');
IF @var26 IS NOT NULL EXEC(N'ALTER TABLE [Authorizations] DROP CONSTRAINT [' + @var26 + '];');
ALTER TABLE [Authorizations] DROP COLUMN [Module_Malzeme];
GO

DECLARE @var27 sysname;
SELECT @var27 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Authorizations]') AND [c].[name] = N'Module_ManuelIsEmri');
IF @var27 IS NOT NULL EXEC(N'ALTER TABLE [Authorizations] DROP CONSTRAINT [' + @var27 + '];');
ALTER TABLE [Authorizations] DROP COLUMN [Module_ManuelIsEmri];
GO

DECLARE @var28 sysname;
SELECT @var28 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Authorizations]') AND [c].[name] = N'Module_Partner');
IF @var28 IS NOT NULL EXEC(N'ALTER TABLE [Authorizations] DROP CONSTRAINT [' + @var28 + '];');
ALTER TABLE [Authorizations] DROP COLUMN [Module_Partner];
GO

DECLARE @var29 sysname;
SELECT @var29 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Authorizations]') AND [c].[name] = N'Module_PropaySatis');
IF @var29 IS NOT NULL EXEC(N'ALTER TABLE [Authorizations] DROP CONSTRAINT [' + @var29 + '];');
ALTER TABLE [Authorizations] DROP COLUMN [Module_PropaySatis];
GO

DECLARE @var30 sysname;
SELECT @var30 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Authorizations]') AND [c].[name] = N'Module_PropaySatisList');
IF @var30 IS NOT NULL EXEC(N'ALTER TABLE [Authorizations] DROP CONSTRAINT [' + @var30 + '];');
ALTER TABLE [Authorizations] DROP COLUMN [Module_PropaySatisList];
GO

DECLARE @var31 sysname;
SELECT @var31 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Authorizations]') AND [c].[name] = N'Module_Raporlar');
IF @var31 IS NOT NULL EXEC(N'ALTER TABLE [Authorizations] DROP CONSTRAINT [' + @var31 + '];');
ALTER TABLE [Authorizations] DROP COLUMN [Module_Raporlar];
GO

DECLARE @var32 sysname;
SELECT @var32 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Authorizations]') AND [c].[name] = N'Module_Raporlar_Bolgesel');
IF @var32 IS NOT NULL EXEC(N'ALTER TABLE [Authorizations] DROP CONSTRAINT [' + @var32 + '];');
ALTER TABLE [Authorizations] DROP COLUMN [Module_Raporlar_Bolgesel];
GO

DECLARE @var33 sysname;
SELECT @var33 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Authorizations]') AND [c].[name] = N'Module_Raporlar_Portfoy');
IF @var33 IS NOT NULL EXEC(N'ALTER TABLE [Authorizations] DROP CONSTRAINT [' + @var33 + '];');
ALTER TABLE [Authorizations] DROP COLUMN [Module_Raporlar_Portfoy];
GO

DECLARE @var34 sysname;
SELECT @var34 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Authorizations]') AND [c].[name] = N'Module_SatisZiyaret_Listesi');
IF @var34 IS NOT NULL EXEC(N'ALTER TABLE [Authorizations] DROP CONSTRAINT [' + @var34 + '];');
ALTER TABLE [Authorizations] DROP COLUMN [Module_SatisZiyaret_Listesi];
GO

DECLARE @var35 sysname;
SELECT @var35 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Authorizations]') AND [c].[name] = N'Module_Satis_Listesi');
IF @var35 IS NOT NULL EXEC(N'ALTER TABLE [Authorizations] DROP CONSTRAINT [' + @var35 + '];');
ALTER TABLE [Authorizations] DROP COLUMN [Module_Satis_Listesi];
GO

DECLARE @var36 sysname;
SELECT @var36 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Authorizations]') AND [c].[name] = N'Module_TaksidePosGiris');
IF @var36 IS NOT NULL EXEC(N'ALTER TABLE [Authorizations] DROP CONSTRAINT [' + @var36 + '];');
ALTER TABLE [Authorizations] DROP COLUMN [Module_TaksidePosGiris];
GO

DECLARE @var37 sysname;
SELECT @var37 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Authorizations]') AND [c].[name] = N'Module_Tatiller');
IF @var37 IS NOT NULL EXEC(N'ALTER TABLE [Authorizations] DROP CONSTRAINT [' + @var37 + '];');
ALTER TABLE [Authorizations] DROP COLUMN [Module_Tatiller];
GO

DECLARE @var38 sysname;
SELECT @var38 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Authorizations]') AND [c].[name] = N'Module_TeknisyenAdresleri');
IF @var38 IS NOT NULL EXEC(N'ALTER TABLE [Authorizations] DROP CONSTRAINT [' + @var38 + '];');
ALTER TABLE [Authorizations] DROP COLUMN [Module_TeknisyenAdresleri];
GO

EXEC sp_rename N'[RoleTypes].[Module_Uygulamalar]', N'Module_Order', N'COLUMN';
GO

EXEC sp_rename N'[Authorizations].[Module_Uygulamalar]', N'Module_Order', N'COLUMN';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250805151740_ver5', N'8.0.19');
GO

COMMIT;
GO

