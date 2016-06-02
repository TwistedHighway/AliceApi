CREATE TABLE [dbo].[ToDoes] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Description] NVARCHAR (MAX) NULL,
    [IsDone]      BIT            NOT NULL,
    [User_Id]     NVARCHAR (128) NULL
);

