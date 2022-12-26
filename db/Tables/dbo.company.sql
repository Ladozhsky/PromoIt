CREATE TABLE [dbo].[company]
(
[company_id] [int] NOT NULL IDENTITY(1, 1),
[company_name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[site] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[email] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[company_type] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[company] ADD CONSTRAINT [PK_company] PRIMARY KEY CLUSTERED ([company_id]) ON [PRIMARY]
GO
