
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/03/2016 19:22:46
-- Generated from EDMX file: C:\Users\gbwuguo\documents\visual studio 2013\Projects\TaxImport\TaxImport\DB\TaxModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [TaxInfo];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[TaxInfoes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TaxInfoes];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'TaxInfoes'
CREATE TABLE [dbo].[TaxInfoes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Account] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [CurrencyCode] nvarchar(max)  NOT NULL,
    [Value] float  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'TaxInfoes'
ALTER TABLE [dbo].[TaxInfoes]
ADD CONSTRAINT [PK_TaxInfoes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------