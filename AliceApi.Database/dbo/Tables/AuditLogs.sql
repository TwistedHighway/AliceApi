CREATE TABLE [dbo].[AuditLogs] (
    [AuditLogId]    UNIQUEIDENTIFIER NOT NULL,
    [UserName]      NVARCHAR (200)   NULL,
    [EventDateTime] DATETIME         NULL,
    [EventType]     NVARCHAR (50)    NULL,
    [TableName]     NVARCHAR (50)    NULL,
    [RecordId]      UNIQUEIDENTIFIER NULL,
    [ColumnName]    NVARCHAR (150)   NULL,
    [OriginalValue] NVARCHAR (MAX)   NULL,
    [NewValue]      NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_AuditLog] PRIMARY KEY NONCLUSTERED ([AuditLogId] ASC)
);

