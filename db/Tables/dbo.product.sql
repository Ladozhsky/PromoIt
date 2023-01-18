CREATE TABLE [dbo].[product]
(
[product_id] [int] NOT NULL IDENTITY(1, 1),
[product_name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[price] [int] NOT NULL,
[company_id] [int] NOT NULL,
[image] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[product] ADD CONSTRAINT [PK_product] PRIMARY KEY CLUSTERED ([product_id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[product] ADD CONSTRAINT [FK_product_company] FOREIGN KEY ([company_id]) REFERENCES [dbo].[company] ([company_id])
GO
