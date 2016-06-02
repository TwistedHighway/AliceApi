CREATE TABLE [dbo].[AppConfig] (
    [AppConfigId]     UNIQUEIDENTIFIER NOT NULL,
    [ApplicationId]   UNIQUEIDENTIFIER NULL,
    [ApiClient]       NVARCHAR (50)    NULL,
    [ApiClientId]     NVARCHAR (MAX)   NULL,
    [ApiClientSecret] NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_AppConfig] PRIMARY KEY CLUSTERED ([AppConfigId] ASC)
);

