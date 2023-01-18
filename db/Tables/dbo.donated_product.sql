CREATE TABLE [dbo].[donated_product]
(
[donated_product_id] [int] NOT NULL IDENTITY(1, 1),
[product_id] [int] NOT NULL,
[campaign_id] [int] NOT NULL,
[user_id] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[amount] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[donated_product] ADD CONSTRAINT [PK_donated_product] PRIMARY KEY CLUSTERED ([donated_product_id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[donated_product] ADD CONSTRAINT [FK_donated_product_campaign] FOREIGN KEY ([campaign_id]) REFERENCES [dbo].[campaign] ([campaign_id])
GO
ALTER TABLE [dbo].[donated_product] ADD CONSTRAINT [FK_donated_product_product] FOREIGN KEY ([product_id]) REFERENCES [dbo].[product] ([product_id])
GO
ALTER TABLE [dbo].[donated_product] ADD CONSTRAINT [FK_donated_product_user] FOREIGN KEY ([user_id]) REFERENCES [dbo].[user] ([user_id])
GO
