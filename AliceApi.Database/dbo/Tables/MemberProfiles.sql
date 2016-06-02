CREATE TABLE [dbo].[MemberProfiles] (
    [MemberProfileId]    UNIQUEIDENTIFIER NOT NULL,
    [AspNetUserId]       NVARCHAR (128)   NOT NULL,
    [ApplicationId]      UNIQUEIDENTIFIER NULL,
    [LocalUserName]      NVARCHAR (256)   NULL,
    [PublicUserName]     NVARCHAR (256)   NULL,
    [PrimaryEmail]       NVARCHAR (256)   NULL,
    [SecondaryEmail]     NVARCHAR (256)   NULL,
    [ProfileDescription] NVARCHAR (MAX)   NULL,
    [ProfileTimeZone]    NVARCHAR (128)   NULL,
    [ProfileLanguage]    NVARCHAR (128)   NULL,
    [PasswordQuestion]   NVARCHAR (256)   NULL,
    [PasswordAnswer]     NVARCHAR (256)   NULL,
    [LastLoginDate]      DATETIME         NULL,
    [CreateDate]         DATETIME         NOT NULL,
    [UpdatedDate]        DATETIME         NOT NULL,
    [CreatedBy]          NVARCHAR (50)    NOT NULL,
    [UpdatedBy]          NVARCHAR (50)    NULL,
    CONSTRAINT [PK__Membersh__1788CC4CA6AF02B0] PRIMARY KEY CLUSTERED ([MemberProfileId] ASC),
    CONSTRAINT [FK_MembershipProfiles_AspNetUsers] FOREIGN KEY ([AspNetUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
);

