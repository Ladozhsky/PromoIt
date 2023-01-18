CREATE TABLE [dbo].[role]
(
[role_id] [int] NOT NULL IDENTITY(1, 1),
[role_name] [nvarchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[role] ADD CONSTRAINT [PK_role] PRIMARY KEY CLUSTERED ([role_id]) ON [PRIMARY]
GO
