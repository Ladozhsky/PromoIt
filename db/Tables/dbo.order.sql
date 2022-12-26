CREATE TABLE [dbo].[order]
(
[order_id] [int] NOT NULL IDENTITY(1, 1),
[campaign_id] [int] NOT NULL,
[company_id] [int] NOT NULL,
[product_id] [int] NOT NULL,
[user_id] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[order] ADD CONSTRAINT [PK_order] PRIMARY KEY CLUSTERED ([order_id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[order] ADD CONSTRAINT [FK_order_campaign] FOREIGN KEY ([campaign_id]) REFERENCES [dbo].[campaign] ([campaign_id])
GO
ALTER TABLE [dbo].[order] ADD CONSTRAINT [FK_order_company] FOREIGN KEY ([company_id]) REFERENCES [dbo].[company] ([company_id])
GO
ALTER TABLE [dbo].[order] ADD CONSTRAINT [FK_order_product] FOREIGN KEY ([product_id]) REFERENCES [dbo].[product] ([product_id])
GO
