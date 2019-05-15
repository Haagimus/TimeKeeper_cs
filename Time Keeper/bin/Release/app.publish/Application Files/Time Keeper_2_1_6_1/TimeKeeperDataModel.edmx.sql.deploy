
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/29/2019 09:57:20
-- Generated from EDMX file: C:\Users\ghaag\Programming\CSharp_Projects\Time Keeper\Time Keeper\TimeKeeperDataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [TimeKeeperDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_DatesEntries]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Entries] DROP CONSTRAINT [FK_DatesEntries];
GO
IF OBJECT_ID(N'[dbo].[FK_DatesTotals]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Totals] DROP CONSTRAINT [FK_DatesTotals];
GO
IF OBJECT_ID(N'[dbo].[FK_ProgramsEntries]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Entries] DROP CONSTRAINT [FK_ProgramsEntries];
GO
IF OBJECT_ID(N'[dbo].[FK_ProgramsTotals]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Totals] DROP CONSTRAINT [FK_ProgramsTotals];
GO
IF OBJECT_ID(N'[dbo].[FK_EntriesDates]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Dates] DROP CONSTRAINT [FK_EntriesDates];
GO
IF OBJECT_ID(N'[dbo].[FK_EntriesPrograms]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Programs] DROP CONSTRAINT [FK_EntriesPrograms];
GO
IF OBJECT_ID(N'[dbo].[FK_TotalsDates]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Dates] DROP CONSTRAINT [FK_TotalsDates];
GO
IF OBJECT_ID(N'[dbo].[FK_TotalsPrograms]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Programs] DROP CONSTRAINT [FK_TotalsPrograms];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Dates]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Dates];
GO
IF OBJECT_ID(N'[dbo].[Entries]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Entries];
GO
IF OBJECT_ID(N'[dbo].[Programs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Programs];
GO
IF OBJECT_ID(N'[dbo].[Totals]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Totals];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Dates'
CREATE TABLE [dbo].[Dates] (
    [DateID] datetime  NOT NULL,
    [EntryID] int  NULL,
    [TotalID] int  NULL
);
GO

-- Creating table 'Entries'
CREATE TABLE [dbo].[Entries] (
    [EntryID] int IDENTITY(1,1) NOT NULL,
    [Hours] decimal(18,2)  NULL,
    [In] datetime  NULL,
    [Out] datetime  NULL,
    [DateID] datetime  NOT NULL,
    [ProgramName] varchar(50)  NOT NULL
);
GO

-- Creating table 'Programs'
CREATE TABLE [dbo].[Programs] (
    [Name] varchar(50)  NOT NULL,
    [Order] int  NOT NULL,
    [Code] varchar(50)  NULL,
    [Notes] varchar(255)  NULL,
    [EntryID] int  NULL,
    [TotalID] int  NULL
);
GO

-- Creating table 'Totals'
CREATE TABLE [dbo].[Totals] (
    [TotalID] int IDENTITY(1,1) NOT NULL,
    [Hours] decimal(18,2)  NULL,
    [Comments] varchar(255)  NULL,
    [DateID] datetime  NOT NULL,
    [ProgramName] varchar(50)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [DateID] in table 'Dates'
ALTER TABLE [dbo].[Dates]
ADD CONSTRAINT [PK_Dates]
    PRIMARY KEY CLUSTERED ([DateID] ASC);
GO

-- Creating primary key on [EntryID] in table 'Entries'
ALTER TABLE [dbo].[Entries]
ADD CONSTRAINT [PK_Entries]
    PRIMARY KEY CLUSTERED ([EntryID] ASC);
GO

-- Creating primary key on [Name] in table 'Programs'
ALTER TABLE [dbo].[Programs]
ADD CONSTRAINT [PK_Programs]
    PRIMARY KEY CLUSTERED ([Name] ASC);
GO

-- Creating primary key on [TotalID] in table 'Totals'
ALTER TABLE [dbo].[Totals]
ADD CONSTRAINT [PK_Totals]
    PRIMARY KEY CLUSTERED ([TotalID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [DateID] in table 'Entries'
ALTER TABLE [dbo].[Entries]
ADD CONSTRAINT [FK_DatesEntries]
    FOREIGN KEY ([DateID])
    REFERENCES [dbo].[Dates]
        ([DateID])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DatesEntries'
CREATE INDEX [IX_FK_DatesEntries]
ON [dbo].[Entries]
    ([DateID]);
GO

-- Creating foreign key on [DateID] in table 'Totals'
ALTER TABLE [dbo].[Totals]
ADD CONSTRAINT [FK_DatesTotals]
    FOREIGN KEY ([DateID])
    REFERENCES [dbo].[Dates]
        ([DateID])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DatesTotals'
CREATE INDEX [IX_FK_DatesTotals]
ON [dbo].[Totals]
    ([DateID]);
GO

-- Creating foreign key on [ProgramName] in table 'Entries'
ALTER TABLE [dbo].[Entries]
ADD CONSTRAINT [FK_ProgramsEntries]
    FOREIGN KEY ([ProgramName])
    REFERENCES [dbo].[Programs]
        ([Name])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProgramsEntries'
CREATE INDEX [IX_FK_ProgramsEntries]
ON [dbo].[Entries]
    ([ProgramName]);
GO

-- Creating foreign key on [ProgramName] in table 'Totals'
ALTER TABLE [dbo].[Totals]
ADD CONSTRAINT [FK_ProgramsTotals]
    FOREIGN KEY ([ProgramName])
    REFERENCES [dbo].[Programs]
        ([Name])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProgramsTotals'
CREATE INDEX [IX_FK_ProgramsTotals]
ON [dbo].[Totals]
    ([ProgramName]);
GO

-- Creating foreign key on [EntryID] in table 'Dates'
ALTER TABLE [dbo].[Dates]
ADD CONSTRAINT [FK_EntriesDates]
    FOREIGN KEY ([EntryID])
    REFERENCES [dbo].[Entries]
        ([EntryID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EntriesDates'
CREATE INDEX [IX_FK_EntriesDates]
ON [dbo].[Dates]
    ([EntryID]);
GO

-- Creating foreign key on [EntryID] in table 'Programs'
ALTER TABLE [dbo].[Programs]
ADD CONSTRAINT [FK_EntriesPrograms]
    FOREIGN KEY ([EntryID])
    REFERENCES [dbo].[Entries]
        ([EntryID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EntriesPrograms'
CREATE INDEX [IX_FK_EntriesPrograms]
ON [dbo].[Programs]
    ([EntryID]);
GO

-- Creating foreign key on [TotalID] in table 'Dates'
ALTER TABLE [dbo].[Dates]
ADD CONSTRAINT [FK_TotalsDates]
    FOREIGN KEY ([TotalID])
    REFERENCES [dbo].[Totals]
        ([TotalID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TotalsDates'
CREATE INDEX [IX_FK_TotalsDates]
ON [dbo].[Dates]
    ([TotalID]);
GO

-- Creating foreign key on [TotalID] in table 'Programs'
ALTER TABLE [dbo].[Programs]
ADD CONSTRAINT [FK_TotalsPrograms]
    FOREIGN KEY ([TotalID])
    REFERENCES [dbo].[Totals]
        ([TotalID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TotalsPrograms'
CREATE INDEX [IX_FK_TotalsPrograms]
ON [dbo].[Programs]
    ([TotalID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------