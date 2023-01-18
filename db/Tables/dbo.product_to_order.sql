CREATE TABLE [dbo].[product_to_order]
(
[po_id] [int] NOT NULL IDENTITY(1, 1),
[order_id] [int] NOT NULL,
[product_id] [int] NOT NULL,
[amount] [int] NOT NULL,
[status] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[product_to_order] ADD CONSTRAINT [PK_product_to_order] PRIMARY KEY CLUSTERED ([po_id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[product_to_order] ADD CONSTRAINT [FK_product_to_order_order] FOREIGN KEY ([order_id]) REFERENCES [dbo].[order] ([order_id])
GO
ALTER TABLE [dbo].[product_to_order] ADD CONSTRAINT [FK_product_to_order_product] FOREIGN KEY ([product_id]) REFERENCES [dbo].[product] ([product_id])
GO
