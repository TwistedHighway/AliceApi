CREATE TABLE [dbo].[AppProfiles] (
    [UserId]               UNIQUEIDENTIFIER NOT NULL,
    [PropertyNames]        NVARCHAR (MAX)   NOT NULL,
    [PropertyValueStrings] NVARCHAR (MAX)   NOT NULL,
    [PropertyValueBinary]  VARBINARY (MAX)  NOT NULL,
    [LastUpdatedDate]      DATETIME         NOT NULL,
    [ApplicationId]        UNIQUEIDENTIFIER NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC)
);

