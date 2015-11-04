CREATE TABLE [dbo].[Movies] (
    [MovieId]    INT           IDENTITY (1, 1) NOT NULL,
    [MovieTitle] NVARCHAR (50) NULL,
    CONSTRAINT [PK_Movies] PRIMARY KEY CLUSTERED ([MovieId] ASC)
);

