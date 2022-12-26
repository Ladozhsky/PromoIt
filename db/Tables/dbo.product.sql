CREATE TABLE [dbo].[product]
(
[product_id] [int] NOT NULL IDENTITY(1, 1),
[product_name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[price] [decimal] (18, 0) NOT NULL,
[amount] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[product] ADD CONSTRAINT [PK_product] PRIMARY KEY CLUSTERED ([product_id]) ON [PRIMARY]
GO
