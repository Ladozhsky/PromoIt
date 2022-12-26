CREATE TABLE [dbo].[campaign]
(
[campaign_id] [int] NOT NULL IDENTITY(1, 1),
[capmpaign_name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[hashtag] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[description] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[user_id] [int] NOT NULL,
[company_id] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[campaign] ADD CONSTRAINT [PK_campaign] PRIMARY KEY CLUSTERED ([campaign_id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[campaign] ADD CONSTRAINT [FK_campaign_company] FOREIGN KEY ([company_id]) REFERENCES [dbo].[company] ([company_id])
GO
ALTER TABLE [dbo].[campaign] ADD CONSTRAINT [FK_campaign_user] FOREIGN KEY ([campaign_id]) REFERENCES [dbo].[user] ([user_id])
GO
