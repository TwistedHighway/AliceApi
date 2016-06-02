CREATE TABLE [dbo].[Members] (
    [MemberId]        UNIQUEIDENTIFIER NOT NULL,
    [MemberProfileId] UNIQUEIDENTIFIER NOT NULL,
    [FirstName]       NVARCHAR (50)    NULL,
    [MiddleName]      NVARCHAR (50)    NULL,
    [LastName]        NVARCHAR (50)    NULL,
    [PreferredName]   NVARCHAR (50)    NULL,
    [MemberType]      INT              NOT NULL,
    [BirthDate]       DATETIME         NULL,
    [Gender]          NVARCHAR (50)    NULL,
    [FacebookHomeUrl] NVARCHAR (256)   NULL,
    [MemberEmail]     NVARCHAR (128)   NULL,
    CONSTRAINT [PK_ProfileMembers] PRIMARY KEY CLUSTERED ([MemberId] ASC),
    CONSTRAINT [FK_ProfileMembers_MembershipProfiles] FOREIGN KEY ([MemberProfileId]) REFERENCES [dbo].[MemberProfiles] ([MemberProfileId]) ON DELETE CASCADE ON UPDATE CASCADE
);

