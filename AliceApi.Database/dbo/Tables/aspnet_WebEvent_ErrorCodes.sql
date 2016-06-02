CREATE TABLE [dbo].[aspnet_WebEvent_ErrorCodes] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (255) NOT NULL,
    [EventCode] INT            NOT NULL,
    [Level]     NVARCHAR (10)  CONSTRAINT [DF_aspnet_WebEvent_ErrorCodes_Level] DEFAULT ('Info') NOT NULL,
    CONSTRAINT [PK_aspnet_WebEvent_ErrorCodes] PRIMARY KEY CLUSTERED ([Id] ASC)
);

