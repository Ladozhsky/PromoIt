CREATE TABLE [dbo].[user]
(
[user_id] [int] NOT NULL IDENTITY(1, 1),
[user_name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[password] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[email] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[address] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[tel_number] [int] NOT NULL,
[role_id] [int] NOT NULL,
[company_id] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[user] ADD CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED ([user_id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[user] ADD CONSTRAINT [FK_user_company] FOREIGN KEY ([company_id]) REFERENCES [dbo].[company] ([company_id])
GO
ALTER TABLE [dbo].[user] ADD CONSTRAINT [FK_user_role] FOREIGN KEY ([user_id]) REFERENCES [dbo].[role] ([role_id])
GO
