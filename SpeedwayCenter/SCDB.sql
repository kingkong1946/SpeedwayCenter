-- 2016-03-12
USE [SpeedwayCenter]
GO

DECLARE @command AS nvarchar(MAX)
IF OBJECT_ID(N'[Riders]') IS NULL 
BEGIN

SET @command = 'CREATE TABLE [Riders](
     [Id]           uniqueidentifier    NOT NULL
    ,[Name]         nvarchar(20)        NOT NULL
    ,[Forname]      nvarchar(20)        NOT NULL
    ,[Country]      nvarchar(20)        NOT NULL
    ,[BirthDate]    date                NOT NULL

     CONSTRAINT PK_RidersId PRIMARY KEY ([Id])
)'

END
EXEC(@command)
GO

DECLARE @command AS nvarchar(MAX)
IF OBJECT_ID(N'[Teams]') IS NULL 
BEGIN

SET @command = 'CREATE TABLE [Teams] (
     [Id]           uniqueidentifier    NOT NULL
    ,[Name]         nvarchar(20)        NOT NULL
    ,[City]         nvarchar(20)        NOT NULL
    ,[StadiumName]  nvarchar(20)        NOT NULL
    ,[Capacity]     int                 NOT NULL

     CONSTRAINT PK_TeamsId PRIMARY KEY ([Id])
)'  

END
EXEC(@command)
GO

DECLARE @command AS nvarchar(MAX)
IF OBJECT_ID(N'[TeamsRiders]') IS NULL 
BEGIN

SET @command = 'CREATE TABLE [TeamsRiders] (
     [Id]       uniqueidentifier NOT NULL
    ,[RiderId]  uniqueidentifier NOT NULL
    ,[TeamId]   uniqueidentifier NOT NULL

     CONSTRAINT PK_TeamsRidersId PRIMARY KEY ([Id])

     CONSTRAINT FK_RiderId  FOREIGN KEY ([RiderId]) REFERENCES [Riders]([Id])
    ,CONSTRAINT FK_TeamId   FOREIGN KEY ([TeamId])  REFERENCES [Teams]([Id])
)'

END
EXEC(@command)
GO

DECLARE @command AS nvarchar(MAX)
IF OBJECT_ID(N'[Meetings]') IS NULL 
BEGIN

SET @command = 'CREATE TABLE [Meetings] (
     [Id]   uniqueidentifier    NOT NULL
    ,[Date] datetime2           NOT NULL

     CONSTRAINT PK_MeetingsId PRIMARY KEY ([Id])
)'

END
EXEC(@command)
GO

DECLARE @command AS nvarchar(MAX)
IF OBJECT_ID(N'[Heats]') IS NULL 
BEGIN

SET @command = 'CREATE TABLE [Heats] (
     [Id]           uniqueidentifier NOT NULL
    ,[GateARiderId] uniqueidentifier NOT NULL
    ,[GateBRiderId] uniqueidentifier NOT NULL
    ,[GateCRiderId] uniqueidentifier NOT NULL
    ,[GateDRiderId] uniqueidentifier NOT NULL
    
    ,[GateAPoints] int NOT NULL
    ,[GateBPoints] int NOT NULL
    ,[GateCPoints] int NOT NULL
    ,[GateDPoints] int NOT NULL

    ,[MeetingId] uniqueidentifier NOT NULL

     CONSTRAINT PK_HeatsId PRIMARY KEY ([Id])

     CONSTRAINT FK_MeetingId FOREIGN KEY ([MeetingId]) REFERENCES [Meetings]([Id])
)'

END
EXEC(@command)
GO

DECLARE @command AS nvarchar(MAX)
IF OBJECT_ID(N'[Attributes]') IS NULL 
BEGIN

SET @command = 'CREATE TABLE [Attributes] (
     [Id]   uniqueidentifier    NOT NULL
    ,[Name] nvarchar(30)        NOT NULL
    
     CONSTRAINT PK_AttributesId PRIMARY KEY ([Id])
)'    

END
EXEC(@command)
GO

DECLARE @command AS nvarchar(MAX)
IF OBJECT_ID(N'[MeetingAttributes]') IS NULL 
BEGIN

SET @command = 'CREATE TABLE [MeetingAttributes] (
     [Id]           uniqueidentifier    NOT NULL
    ,[MeetingId]    uniqueidentifier    NOT NULL
    ,[AttributeId]  uniqueidentifier    NOT NULL
    ,[Value]        nvarchar(MAX)       NOT NULL
    
     CONSTRAINT PK_MeetingAttributesId PRIMARY KEY ([Id])

     CONSTRAINT FK_MeetingInAttributesId    FOREIGN KEY ([MeetingId])   REFERENCES [Meetings]([Id])
    ,CONSTRAINT FK_AttributeId  FOREIGN KEY ([AttributeId]) REFERENCES [Attributes]([Id])
)'

END
EXEC(@command)
GO

DECLARE @command AS nvarchar(MAX)
IF OBJECT_ID(N'[Leagues]') IS NULL 
BEGIN

SET @command = 'CREATE TABLE [Leagues] (
     [Id]       uniqueidentifier    NOT NULL
    ,[Name]     nvarchar(50)        NOT NULL
    ,[Country]  nvarchar(20)        NOT NULL
    
     CONSTRAINT PK_LeaguesId PRIMARY KEY ([Id])
)'

END
EXEC(@command)
GO

DECLARE @command AS nvarchar(MAX)
IF OBJECT_ID(N'[Seasons]') IS NULL 
BEGIN

SET @command = 'CREATE TABLE [Seasons] (
     [Id]       uniqueidentifier    NOT NULL
    ,[Name]     nvarchar(50)        NOT NULL
    ,[LeagueId] uniqueidentifier    NOT NULL
    
     CONSTRAINT PK_SeasonsId PRIMARY KEY ([Id])

     CONSTRAINT FK_LeagueId FOREIGN KEY ([LeagueId]) REFERENCES [Leagues]([Id])
)'

END
EXEC(@command)
GO

DECLARE @command AS nvarchar(MAX)
IF OBJECT_ID(N'[SeasonsTeamsRiders]') IS NULL 
BEGIN

SET @command = 'CREATE TABLE [SeasonsTeamsRiders] (
     [Id]               uniqueidentifier NOT NULL
    ,[TeamsRidersId]    uniqueidentifier NOT NULL
    ,[SeasonId]         uniqueidentifier NOT NULL
    
     CONSTRAINT PK_SeasonsTeamsRidersId PRIMARY KEY ([Id])

     CONSTRAINT FK_TeamsRidersId    FOREIGN KEY ([TeamsRidersId])   REFERENCES [TeamsRiders]([Id])
    ,CONSTRAINT FK_SeasionId        FOREIGN KEY ([SeasonId])        REFERENCES [Seasons]([Id])
)'

END
EXEC(@command)
GO

IF NOT EXISTS (SELECT * FROM [Attributes] WHERE dbo.Attributes.Name = 'Round')
BEGIN

INSERT INTO dbo.Attributes(
    Id
    ,Name
)	
VALUES(
    NEWID()
   ,N'Round'
)
END
GO

IF NOT EXISTS (SELECT * FROM [Attributes] WHERE dbo.Attributes.Name = 'HomeTeamId')
BEGIN

INSERT INTO dbo.Attributes(
    Id
    ,Name
)	
VALUES(
    NEWID()
   ,N'HomeTeamId'
)
END
GO
   
IF NOT EXISTS (SELECT * FROM [Attributes] WHERE dbo.Attributes.Name = 'AwayTeamId')
BEGIN

INSERT INTO dbo.Attributes(
    Id
    ,Name
)	
VALUES(
    NEWID()
   ,N'AwayTeamId'
)
END
GO

DECLARE @command AS nvarchar(MAX)
IF OBJECT_ID(N'[LeagueMeetingsView]') IS NULL 
BEGIN

SET @command = 'CREATE VIEW LeagueMeetingsView
AS 
SELECT m.Id
    ,m.[Date]
    ,mht.[Value] AS HomeTeamId
    ,mat.[Value] AS AwayTeamId
    ,mr.[Value] AS [Round]
FROM dbo.Meetings m
JOIN (
    SELECT MeetingId, Name, [Value]        
    FROM
        dbo.MeetingAttributes ma
    JOIN
        dbo.Attributes a
    ON a.Id = ma.AttributeId
    WHERE a.Name = ''HomeTeamId''
    ) mht
ON m.Id = mht.MeetingId 
JOIN (
    SELECT MeetingId, Name, [Value]        
    FROM
        dbo.MeetingAttributes ma
    JOIN
        dbo.Attributes a
    ON a.Id = ma.AttributeId
    WHERE a.Name = ''AwayTeamId''
    ) mat
ON m.Id = mat.MeetingId 
JOIN (
    SELECT MeetingId, Name, [Value]        
    FROM
        dbo.MeetingAttributes ma
    JOIN
        dbo.Attributes a
    ON a.Id = ma.AttributeId
    WHERE a.Name = ''Round''
    ) mr
ON m.Id = mr.MeetingId'

END
EXEC(@command)
GO

USE [SpeedwayCenter]
GO

DECLARE @command AS nvarchar(MAX)
IF OBJECT_ID(N'[Riders]') IS NOT NULL 
BEGIN
	SET @command = 'ALTER TABLE dbo.Riders ADD [Country] nvarchar(20)'
END
ELSE 
BEGIN
    SET @command = 'CREATE TABLE [Riders](
     [Id]           uniqueidentifier    NOT NULL
    ,[Name]         nvarchar(20)        NOT NULL
    ,[Forname]      nvarchar(20)        NOT NULL
    ,[Country]      nvarchar(20)        NOT NULL
    ,[BirthDate]    date                NOT NULL

     CONSTRAINT PK_RidersId PRIMARY KEY ([Id])
)'
END
EXEC(@command)
GO