
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 03/18/2012 22:58:34
-- Generated from EDMX file: C:\Users\lursu\Documents\quarter 7\.Net 2 projects\coding contest site\trunk\CCProject\CC.Domain\Entities\ContestDataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [TooMuchJuice];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_PersonTeam_Person]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersonTeam] DROP CONSTRAINT [FK_PersonTeam_Person];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonTeam_Team]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersonTeam] DROP CONSTRAINT [FK_PersonTeam_Team];
GO
IF OBJECT_ID(N'[dbo].[FK_CompetitionProblem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Problems] DROP CONSTRAINT [FK_CompetitionProblem];
GO
IF OBJECT_ID(N'[dbo].[FK_TeamTeamInCompetition]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TeamInCompetitions] DROP CONSTRAINT [FK_TeamTeamInCompetition];
GO
IF OBJECT_ID(N'[dbo].[FK_CompetitionTeamInCompetition]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TeamInCompetitions] DROP CONSTRAINT [FK_CompetitionTeamInCompetition];
GO
IF OBJECT_ID(N'[dbo].[FK_SolutionTeam]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Solutions] DROP CONSTRAINT [FK_SolutionTeam];
GO
IF OBJECT_ID(N'[dbo].[FK_SolutionFile]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Files] DROP CONSTRAINT [FK_SolutionFile];
GO
IF OBJECT_ID(N'[dbo].[FK_CompetitionSetting]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Competitions] DROP CONSTRAINT [FK_CompetitionSetting];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[People]', 'U') IS NOT NULL
    DROP TABLE [dbo].[People];
GO
IF OBJECT_ID(N'[dbo].[Teams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Teams];
GO
IF OBJECT_ID(N'[dbo].[TeamInCompetitions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TeamInCompetitions];
GO
IF OBJECT_ID(N'[dbo].[Competitions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Competitions];
GO
IF OBJECT_ID(N'[dbo].[Settings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Settings];
GO
IF OBJECT_ID(N'[dbo].[Problems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Problems];
GO
IF OBJECT_ID(N'[dbo].[Solutions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Solutions];
GO
IF OBJECT_ID(N'[dbo].[Files]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Files];
GO
IF OBJECT_ID(N'[dbo].[PersonTeam]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PersonTeam];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'People'
CREATE TABLE [dbo].[People] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserName] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [Role] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Teams'
CREATE TABLE [dbo].[Teams] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NULL
);
GO

-- Creating table 'TeamInCompetitions'
CREATE TABLE [dbo].[TeamInCompetitions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TeamId] int  NOT NULL,
    [CompetitionId] int  NOT NULL
);
GO

-- Creating table 'Competitions'
CREATE TABLE [dbo].[Competitions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [AdminId] int  NOT NULL,
    [EntryPassword] nvarchar(max)  NULL,
    [Setting_Id] int  NOT NULL
);
GO

-- Creating table 'Settings'
CREATE TABLE [dbo].[Settings] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [StartTime] datetime  NOT NULL,
    [EndTime] datetime  NOT NULL,
    [PentaltyAmount] int  NOT NULL,
    [SubmissionTimeLimit] int  NOT NULL
);
GO

-- Creating table 'Problems'
CREATE TABLE [dbo].[Problems] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [ShortName] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [ExampleInput] nvarchar(max)  NOT NULL,
    [ExampleOutput] nvarchar(max)  NOT NULL,
    [ExpectedOutput] nvarchar(max)  NOT NULL,
    [CompetitionId] int  NOT NULL,
    [ActualInput] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Solutions'
CREATE TABLE [dbo].[Solutions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SubmissionTime] datetime  NOT NULL,
    [Result] nvarchar(max)  NOT NULL,
    [Score] int  NULL,
    [TeamId] int  NOT NULL,
    [JudgeTime] datetime  NOT NULL,
    [ResultDescription] nvarchar(max)  NOT NULL,
    [ProblemId] int  NOT NULL
);
GO

-- Creating table 'Files'
CREATE TABLE [dbo].[Files] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Data] nvarchar(max)  NULL,
    [Name] nvarchar(max)  NOT NULL,
    [SolutionFile_File_Id] int  NOT NULL
);
GO

-- Creating table 'PersonTeam'
CREATE TABLE [dbo].[PersonTeam] (
    [People_Id] int  NOT NULL,
    [Teams_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'People'
ALTER TABLE [dbo].[People]
ADD CONSTRAINT [PK_People]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Teams'
ALTER TABLE [dbo].[Teams]
ADD CONSTRAINT [PK_Teams]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TeamInCompetitions'
ALTER TABLE [dbo].[TeamInCompetitions]
ADD CONSTRAINT [PK_TeamInCompetitions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Competitions'
ALTER TABLE [dbo].[Competitions]
ADD CONSTRAINT [PK_Competitions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Settings'
ALTER TABLE [dbo].[Settings]
ADD CONSTRAINT [PK_Settings]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Problems'
ALTER TABLE [dbo].[Problems]
ADD CONSTRAINT [PK_Problems]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Solutions'
ALTER TABLE [dbo].[Solutions]
ADD CONSTRAINT [PK_Solutions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Files'
ALTER TABLE [dbo].[Files]
ADD CONSTRAINT [PK_Files]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [People_Id], [Teams_Id] in table 'PersonTeam'
ALTER TABLE [dbo].[PersonTeam]
ADD CONSTRAINT [PK_PersonTeam]
    PRIMARY KEY NONCLUSTERED ([People_Id], [Teams_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [People_Id] in table 'PersonTeam'
ALTER TABLE [dbo].[PersonTeam]
ADD CONSTRAINT [FK_PersonTeam_Person]
    FOREIGN KEY ([People_Id])
    REFERENCES [dbo].[People]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Teams_Id] in table 'PersonTeam'
ALTER TABLE [dbo].[PersonTeam]
ADD CONSTRAINT [FK_PersonTeam_Team]
    FOREIGN KEY ([Teams_Id])
    REFERENCES [dbo].[Teams]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonTeam_Team'
CREATE INDEX [IX_FK_PersonTeam_Team]
ON [dbo].[PersonTeam]
    ([Teams_Id]);
GO

-- Creating foreign key on [CompetitionId] in table 'Problems'
ALTER TABLE [dbo].[Problems]
ADD CONSTRAINT [FK_CompetitionProblem]
    FOREIGN KEY ([CompetitionId])
    REFERENCES [dbo].[Competitions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CompetitionProblem'
CREATE INDEX [IX_FK_CompetitionProblem]
ON [dbo].[Problems]
    ([CompetitionId]);
GO

-- Creating foreign key on [TeamId] in table 'TeamInCompetitions'
ALTER TABLE [dbo].[TeamInCompetitions]
ADD CONSTRAINT [FK_TeamTeamInCompetition]
    FOREIGN KEY ([TeamId])
    REFERENCES [dbo].[Teams]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TeamTeamInCompetition'
CREATE INDEX [IX_FK_TeamTeamInCompetition]
ON [dbo].[TeamInCompetitions]
    ([TeamId]);
GO

-- Creating foreign key on [CompetitionId] in table 'TeamInCompetitions'
ALTER TABLE [dbo].[TeamInCompetitions]
ADD CONSTRAINT [FK_CompetitionTeamInCompetition]
    FOREIGN KEY ([CompetitionId])
    REFERENCES [dbo].[Competitions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CompetitionTeamInCompetition'
CREATE INDEX [IX_FK_CompetitionTeamInCompetition]
ON [dbo].[TeamInCompetitions]
    ([CompetitionId]);
GO

-- Creating foreign key on [TeamId] in table 'Solutions'
ALTER TABLE [dbo].[Solutions]
ADD CONSTRAINT [FK_SolutionTeam]
    FOREIGN KEY ([TeamId])
    REFERENCES [dbo].[Teams]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SolutionTeam'
CREATE INDEX [IX_FK_SolutionTeam]
ON [dbo].[Solutions]
    ([TeamId]);
GO

-- Creating foreign key on [SolutionFile_File_Id] in table 'Files'
ALTER TABLE [dbo].[Files]
ADD CONSTRAINT [FK_SolutionFile]
    FOREIGN KEY ([SolutionFile_File_Id])
    REFERENCES [dbo].[Solutions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SolutionFile'
CREATE INDEX [IX_FK_SolutionFile]
ON [dbo].[Files]
    ([SolutionFile_File_Id]);
GO

-- Creating foreign key on [Setting_Id] in table 'Competitions'
ALTER TABLE [dbo].[Competitions]
ADD CONSTRAINT [FK_CompetitionSetting]
    FOREIGN KEY ([Setting_Id])
    REFERENCES [dbo].[Settings]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CompetitionSetting'
CREATE INDEX [IX_FK_CompetitionSetting]
ON [dbo].[Competitions]
    ([Setting_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------