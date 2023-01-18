CREATE TABLE [dbo].[retweet]
(
[retweet_id] [int] NOT NULL,
[twitt_id] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[twitter_user_id] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[campaign_id] [int] NOT NULL,
[retweets] [int] NOT NULL,
[parsing_date] [datetime] NOT NULL,
[creation_date] [datetime] NOT NULL,
[update_date] [datetime] NOT NULL,
[create_by_user] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[update_by_user] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[status] [char] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]
GO
