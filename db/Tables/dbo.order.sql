CREATE TABLE [dbo].[order]
(
[order_id] [int] NOT NULL IDENTITY(1, 1),
[campaign_id] [int] NOT NULL,
[company_id] [int] NOT NULL,
[user_id] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[quantity] [int] NULL CONSTRAINT [DF_order_quantity] DEFAULT ((1))
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[order] ADD CONSTRAINT [PK_order] PRIMARY KEY CLUSTERED ([order_id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[order] ADD CONSTRAINT [FK_order_campaign] FOREIGN KEY ([campaign_id]) REFERENCES [dbo].[campaign] ([campaign_id])
GO
ALTER TABLE [dbo].[order] ADD CONSTRAINT [FK_order_company1] FOREIGN KEY ([company_id]) REFERENCES [dbo].[company] ([company_id])
GO
ALTER TABLE [dbo].[order] ADD CONSTRAINT [FK_order_user] FOREIGN KEY ([user_id]) REFERENCES [dbo].[user] ([user_id])
GO
