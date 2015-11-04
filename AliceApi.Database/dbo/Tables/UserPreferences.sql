CREATE TABLE [dbo].[UserPreferences] (
    [UserPreferenceId] UNIQUEIDENTIFIER NOT NULL,
    [UserId]           UNIQUEIDENTIFIER NOT NULL,
    [PerfValue]        NVARCHAR (50)    NULL,
    CONSTRAINT [PK_UserPreferences] PRIMARY KEY CLUSTERED ([UserPreferenceId] ASC)
);

