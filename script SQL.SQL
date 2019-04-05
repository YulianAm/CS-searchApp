USE [SearchAppLog]
GO
/****** Object:  Table [dbo].[searchLogs]    Script Date: 4/5/2019 12:29:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[searchLogs](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[searchTerm] [varchar](1000) NOT NULL,
	[searchPath] [varchar](3000) NOT NULL,
	[resultFilePath] [varchar](4000) NOT NULL,
	[DateSearched] [datetime] NULL,
	[SearchType] [varchar](300) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
