CREATE TABLE [dbo].[balance_transactions]
(
[transaction_id] [int] NOT NULL IDENTITY(1, 1),
[user_id] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[campaign_id] [int] NOT NULL,
[amount] [int] NOT NULL,
[reason] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[retweet_id] [int] NULL,
[create_date] [datetime] NOT NULL,
[update_date] [datetime] NOT NULL,
[create_by_user] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[update_by_user] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[balance_transactions] ADD CONSTRAINT [PK_balance_transactions] PRIMARY KEY CLUSTERED ([transaction_id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[balance_transactions] ADD CONSTRAINT [FK_balance_transactions_campaign] FOREIGN KEY ([campaign_id]) REFERENCES [dbo].[campaign] ([campaign_id])
GO
