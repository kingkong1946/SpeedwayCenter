-- 2016-03-12
DROP DATABASE [SpeedwayCenter]
GO

DECLARE @sql AS nvarchar(MAX)
IF OBJECT_ID(N'[Riders]') IS NULL 
BEGIN

SET @sql = 'CREATE TABLE [Riders](
     [Id]           uniqueidentifier    NOT NULL
    ,[Name]         nvarchar(20)        NOT NULL
    ,[Forname]      nvarchar(20)        NOT NULL
    ,[Country]      nvarchar(20)        NOT NULL
    ,[BirthDate]    date                NOT NULL

     CONSTRAINT PK_RidersId PRIMARY KEY ([Id])
)'

END
EXEC(@sql)
GO

DECLARE @sql AS nvarchar(MAX)
IF OBJECT_ID(N'[Teams]') IS NULL 
BEGIN

SET @sql = 'CREATE TABLE [Teams] (
     [Id]           uniqueidentifier    NOT NULL
    ,[Name]         nvarchar(20)        NOT NULL
    ,[City]         nvarchar(20)        NOT NULL
    ,[StadiumName]  nvarchar(20)        NOT NULL
    ,[Capacity]     int                 NOT NULL

     CONSTRAINT PK_TeamsId PRIMARY KEY ([Id])
)'  

END
EXEC(@sql)
GO

DECLARE @sql AS nvarchar(MAX)
IF OBJECT_ID(N'[TeamsRiders]') IS NULL 
BEGIN

SET @sql = 'CREATE TABLE [TeamsRiders] (
     [Id]       uniqueidentifier NOT NULL
    ,[RiderId]  uniqueidentifier NOT NULL
    ,[TeamId]   uniqueidentifier NOT NULL

     CONSTRAINT PK_TeamsRidersId PRIMARY KEY ([Id])

     CONSTRAINT FK_RiderId  FOREIGN KEY ([RiderId]) REFERENCES [Riders]([Id])   ON DELETE CASCADE
    ,CONSTRAINT FK_TeamId   FOREIGN KEY ([TeamId])  REFERENCES [Teams]([Id])    ON DELETE CASCADE
)'

END
EXEC(@sql)
GO

DECLARE @sql AS nvarchar(MAX)
IF OBJECT_ID(N'[Meetings]') IS NULL 
BEGIN

SET @sql = 'CREATE TABLE [Meetings] (
     [Id]   uniqueidentifier    NOT NULL
    ,[Date] datetime2           NOT NULL

     CONSTRAINT PK_MeetingsId PRIMARY KEY ([Id])
)'

END
EXEC(@sql)
GO

DECLARE @sql AS nvarchar(MAX)
IF OBJECT_ID(N'[Heats]') IS NULL 
BEGIN

SET @sql = 'CREATE TABLE [Heats] (
     [Id]           uniqueidentifier NOT NULL
    ,[GateARiderId] uniqueidentifier NOT NULL
    ,[GateBRiderId] uniqueidentifier NOT NULL
    ,[GateCRiderId] uniqueidentifier NOT NULL
    ,[GateDRiderId] uniqueidentifier NOT NULL
    
    ,[GateAPoints] int NULL
    ,[GateBPoints] int NULL
    ,[GateCPoints] int NULL
    ,[GateDPoints] int NULL

    ,[GateASubstitution] uniqueidentifier NOT NULL
    ,[GateBSubstitution] uniqueidentifier NOT NULL
    ,[GateCSubstitution] uniqueidentifier NOT NULL
    ,[GateDSubstitution] uniqueidentifier NOT NULL

    ,[Number] int NOT NULL

    ,[MeetingId] uniqueidentifier	NOT NULL

     CONSTRAINT PK_HeatsId PRIMARY KEY ([Id])

     CONSTRAINT FK_MeetingId FOREIGN KEY ([MeetingId]) REFERENCES [Meetings]([Id]) ON DELETE CASCADE
)'

END
EXEC(@sql)
GO

DECLARE @sql AS nvarchar(MAX)
IF OBJECT_ID(N'[Attributes]') IS NULL 
BEGIN

SET @sql = 'CREATE TABLE [Attributes] (
     [Id]   uniqueidentifier    NOT NULL
    ,[Name] nvarchar(30)        NOT NULL
    
     CONSTRAINT PK_AttributesId PRIMARY KEY ([Id])
)'    

END
EXEC(@sql)
GO

DECLARE @sql AS nvarchar(MAX)
IF OBJECT_ID(N'[MeetingAttributes]') IS NULL 
BEGIN

SET @sql = 'CREATE TABLE [MeetingAttributes] (
     [Id]           uniqueidentifier    NOT NULL
    ,[MeetingId]    uniqueidentifier    NOT NULL
    ,[AttributeId]  uniqueidentifier    NOT NULL
    ,[Value]        nvarchar(MAX)       NOT NULL
    
     CONSTRAINT PK_MeetingAttributesId PRIMARY KEY ([Id])

     CONSTRAINT FK_MeetingInAttributesId    FOREIGN KEY ([MeetingId])   REFERENCES [Meetings]([Id])     ON DELETE CASCADE
    ,CONSTRAINT FK_AttributeId              FOREIGN KEY ([AttributeId]) REFERENCES [Attributes]([Id])   ON DELETE CASCADE
)'

END
EXEC(@sql)
GO

DECLARE @sql AS nvarchar(MAX)
IF OBJECT_ID(N'[Leagues]') IS NULL 
BEGIN

SET @sql = 'CREATE TABLE [Leagues] (
     [Id]       uniqueidentifier    NOT NULL
    ,[Name]     nvarchar(50)        NOT NULL
    ,[Country]  nvarchar(20)        NOT NULL
    
     CONSTRAINT PK_LeaguesId PRIMARY KEY ([Id])
)'

END
EXEC(@sql)
GO

DECLARE @sql AS nvarchar(MAX)
IF OBJECT_ID(N'[Seasons]') IS NULL 
BEGIN

SET @sql = 'CREATE TABLE [Seasons] (
     [Id]       uniqueidentifier    NOT NULL
    ,[Name]     nvarchar(50)        NOT NULL
    ,[LeagueId] uniqueidentifier    NOT NULL
    
     CONSTRAINT PK_SeasonsId PRIMARY KEY ([Id])

     CONSTRAINT FK_LeagueId FOREIGN KEY ([LeagueId]) REFERENCES [Leagues]([Id]) ON DELETE CASCADE
)'

END
EXEC(@sql)
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

CREATE VIEW SpeedwayEkstraligaMeetingsView
AS 
SELECT m.Id
    ,m.[Date]
    ,m.[Status]
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
    WHERE a.Name = 'HomeTeamId'
    ) mht
ON m.Id = mht.MeetingId 
JOIN (
    SELECT MeetingId, Name, [Value]        
    FROM
        dbo.MeetingAttributes ma
    JOIN
        dbo.Attributes a
    ON a.Id = ma.AttributeId
    WHERE a.Name = 'AwayTeamId'
    ) mat
ON m.Id = mat.MeetingId 
JOIN (
    SELECT MeetingId, Name, [Value]        
    FROM
        dbo.MeetingAttributes ma
    JOIN
        dbo.Attributes a
    ON a.Id = ma.AttributeId
    WHERE a.Name = 'Round'
    ) mr
ON m.Id = mr.MeetingId
GO

USE [SpeedwayCenter]
GO

DECLARE @sql AS nvarchar(MAX)
IF OBJECT_ID(N'[Riders]') IS NOT NULL 
BEGIN
	SET @sql = 'ALTER TABLE dbo.Riders ADD [Country] nvarchar(20) NOT NULL'
END
ELSE 
BEGIN
    SET @sql = 'CREATE TABLE [Riders](
     [Id]           uniqueidentifier    NOT NULL
    ,[Name]         nvarchar(20)        NOT NULL
    ,[Forname]      nvarchar(20)        NOT NULL
    ,[Country]      nvarchar(20)        NOT NULL
    ,[BirthDate]    date                NOT NULL

     CONSTRAINT PK_RidersId PRIMARY KEY ([Id])
)'
END
EXEC(@sql)
GO

-- 2016-03-13
USE [SpeedwayCenter]
GO

IF OBJECT_ID(N'[SeasonsTeamsRiders]') IS NOT NULL 
BEGIN

DROP TABLE dbo.SeasonsTeamsRiders

END
GO

DECLARE @sql AS nvarchar(MAX)
IF OBJECT_ID(N'[TeamsSeasons]') IS NOT NULL 
BEGIN
	SET @sql = 'ALTER TABLE dbo.TeamsSeasons ADD 
    [Year]      int             NOT NULL
   ,[ExtraName] nvarchar(60)'
END
ELSE 
BEGIN

    SET @sql = 'CREATE TABLE [TeamsSeasons] (
     [Id]           uniqueidentifier    NOT NULL
    ,[TeamId]       uniqueidentifier    NOT NULL
    ,[SeasonId]     uniqueidentifier    NOT NULL
    ,[Year]         int                 NOT NULL
    ,[ExtraName]    nvarchar(60)        NOT NULL

     CONSTRAINT PK_SeasonTeamId PRIMARY KEY ([Id])

     CONSTRAINT FK_TeamIdForSeason  FOREIGN KEY ([TeamId])      REFERENCES [Teams]([Id])    ON DELETE CASCADE
    ,CONSTRAINT FK_SeasonId         FOREIGN KEY ([SeasonId])    REFERENCES [Seasons]([Id])  ON DELETE CASCADE
)'
END
EXEC(@sql)
GO

USE SpeedwayCenter
GO

DECLARE @sql AS nvarchar(MAX)
IF OBJECT_ID(N'Meetings') IS NOT NULL
BEGIN
	set @sql = 'ALTER TABLE dbo.Meetings ADD [Status] int NOT NULL DEFAULT 0'
END
ELSE
BEGIN
    SET @sql = 'CREATE TABLE [Meetings] (
     [Id]       uniqueidentifier    NOT NULL
    ,[Date]     datetime2           NOT NULL
    ,[Status]   int                 NOT NULL    DEFAULT 0

     CONSTRAINT PK_MeetingsId PRIMARY KEY ([Id])
    )'
END
EXEC(@sql)
GO

ALTER VIEW dbo.LeagueMeetingsView
AS 
SELECT m.Id
    ,m.[Date]
    ,m.[Status]
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
    WHERE a.Name = 'HomeTeamId'
    ) mht
ON m.Id = mht.MeetingId 
JOIN (
    SELECT MeetingId, Name, [Value]        
    FROM
        dbo.MeetingAttributes ma
    JOIN
        dbo.Attributes a
    ON a.Id = ma.AttributeId
    WHERE a.Name = 'AwayTeamId'
    ) mat
ON m.Id = mat.MeetingId 
JOIN (
    SELECT MeetingId, Name, [Value]        
    FROM
        dbo.MeetingAttributes ma
    JOIN
        dbo.Attributes a
    ON a.Id = ma.AttributeId
    WHERE a.Name = 'Round'
    ) mr
ON m.Id = mr.MeetingId
GO

-- 2016-03-15
USE [SpeedwayCenter]
GO

CREATE TRIGGER TRGI_LeagueMettings ON dbo.SpeedwayEkstraligaMeetingsView
INSTEAD OF INSERT
AS
    DECLARE @HomeTeamId AS uniqueidentifier
    DECLARE @AwayTeamId AS uniqueidentifier
    DECLARE @RoundId AS uniqueidentifier

    DECLARE @HomeTeamValue AS nvarchar(MAX)
    DECLARE @AwayTeamValue AS nvarchar(MAX)
    DECLARE @RoundValue AS nvarchar(MAX)

    DECLARE @Id AS uniqueidentifier

    SELECT @HomeTeamId = a.Id
    FROM Attributes a
    WHERE a.Name = 'HomeTeamId'

    SELECT @AwayTeamId = a.Id
    FROM Attributes a
    WHERE a.Name = '@AwayTeamId'

    SELECT @RoundId = a.Id
    FROM Attributes a
    WHERE a.Name = 'Round'
    
    SELECT @HomeTeamValue = i.HomeTeamId
    FROM INSERTED i

    SELECT @AwayTeamValue = i.AwayTeamId
    FROM INSERTED i
    
    SELECT @RoundValue = i.[Round]
    FROM INSERTED i
    
    SELECT @Id = i.Id
    FROM INSERTED i

BEGIN
    INSERT INTO dbo.Meetings
    SELECT i.Id, i.[Date], i.[Status]
    FROM INSERTED i

    INSERT INTO dbo.MeetingAttributes
    (
        Id,
        MeetingId,
        AttributeId,
        [Value]
    )
    VALUES
    (
        NEWID(), -- Id - uniqueidentifier
        @Id, -- MeetingId - uniqueidentifier
        @HomeTeamId, -- AttributeId - uniqueidentifier
        @HomeTeamValue -- Value - nvarchar
    )
   ,(
        NEWID(), -- Id - uniqueidentifier
        @Id, -- MeetingId - uniqueidentifier
        @AwayTeamId, -- AttributeId - uniqueidentifier
        @AwayTeamValue -- Value - nvarchar
    )
   ,(
        NEWID(), -- Id - uniqueidentifier
        @Id, -- MeetingId - uniqueidentifier
        @RoundId, -- AttributeId - uniqueidentifier
        @RoundValue -- Value - nvarchar
    )
END
GO

CREATE VIEW dbo.SpeedwayEkstraligaMeetingsView
AS 
SELECT m.Id
    ,m.[Date]
    ,m.[Status]
    ,CAST(mht.[Value] AS uniqueidentifier) AS HomeTeamId
    ,CAST(mat.[Value] AS uniqueidentifier) AS AwayTeamId
    ,CAST(mr.[Value] AS int) AS [Round]
FROM dbo.Meetings m
JOIN (
    SELECT MeetingId, Name, [Value]        
    FROM
        dbo.MeetingAttributes ma
    JOIN
        dbo.Attributes a
    ON a.Id = ma.AttributeId
    WHERE a.Name = 'HomeTeamId'
    ) mht
ON m.Id = mht.MeetingId 
JOIN (
    SELECT MeetingId, Name, [Value]        
    FROM
        dbo.MeetingAttributes ma
    JOIN
        dbo.Attributes a
    ON a.Id = ma.AttributeId
    WHERE a.Name = 'AwayTeamId'
    ) mat
ON m.Id = mat.MeetingId 
JOIN (
    SELECT MeetingId, Name, [Value]        
    FROM
        dbo.MeetingAttributes ma
    JOIN
        dbo.Attributes a
    ON a.Id = ma.AttributeId
    WHERE a.Name = 'Round'
    ) mr
ON m.Id = mr.MeetingId
GO
