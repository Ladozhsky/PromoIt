CREATE TABLE [dbo].[tweet]
(
[tweet_id] [int] NOT NULL IDENTITY(1, 1),
[user_id] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[tweet] ADD CONSTRAINT [PK_tweet] PRIMARY KEY CLUSTERED ([tweet_id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[tweet] ADD CONSTRAINT [FK_tweet_user] FOREIGN KEY ([user_id]) REFERENCES [dbo].[user] ([user_id])
GO
