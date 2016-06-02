CREATE TABLE [dbo].[MovieGenre] (
    [GenreId]   INT            NOT NULL,
    [GenreName] NVARCHAR (150) NOT NULL,
    CONSTRAINT [PK_MovieGenere] PRIMARY KEY CLUSTERED ([GenreId] ASC)
);

