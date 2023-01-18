CREATE TABLE [dbo].[user_balance]
(
[balance_id] [int] NOT NULL IDENTITY(1, 1),
[user_id] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[balance] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[user_balance] ADD CONSTRAINT [PK_user_balance] PRIMARY KEY CLUSTERED ([balance_id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[user_balance] ADD CONSTRAINT [FK_user_balance_user] FOREIGN KEY ([user_id]) REFERENCES [dbo].[user] ([user_id])
GO
