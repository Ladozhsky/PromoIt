CREATE TABLE [dbo].[user]
(
[user_id] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[user_name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[email] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[address] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[tel_number] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[role] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[company_id] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[user] ADD CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED ([user_id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[user] ADD CONSTRAINT [FK_user_company] FOREIGN KEY ([company_id]) REFERENCES [dbo].[company] ([company_id])
GO
