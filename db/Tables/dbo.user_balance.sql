CREATE TABLE [dbo].[user_balance]
(
[balance_id] [int] NOT NULL IDENTITY(1, 1),
[user_id] [int] NOT NULL,
[balance] [decimal] (18, 0) NOT NULL,
[company_id] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[user_balance] ADD CONSTRAINT [PK_user_balance] PRIMARY KEY CLUSTERED ([balance_id]) ON [PRIMARY]
GO
