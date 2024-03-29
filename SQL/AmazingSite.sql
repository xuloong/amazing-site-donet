/****** Object:  Table [dbo].[Article]    Script Date: 2017/9/30 16:04:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Article](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NULL,
	[Abstract] [nvarchar](200) NULL,
	[Content] [nvarchar](max) NULL,
	[MenuId] [int] NULL,
	[CreateTime] [datetime] NULL CONSTRAINT [DF_Article_CreateTime]  DEFAULT (getdate()),
	[CreateUserId] [int] NULL,
	[UpdateTime] [datetime] NULL,
	[UpdateUserId] [int] NULL,
	[DeleteFlag] [char](1) NULL CONSTRAINT [DF_Article_DeleteFlag]  DEFAULT ('("N")'),
	[DeleteTime] [datetime] NULL,
	[DeleteUserId] [int] NULL,
 CONSTRAINT [PK_Article] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Banner]    Script Date: 2017/9/30 16:04:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Banner](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [char](1) NULL CONSTRAINT [DF_Banner_Type]  DEFAULT ('("P")'),
	[ImageSrc] [varchar](100) NULL,
	[Link] [varchar](100) NULL,
	[Status] [char](1) NULL CONSTRAINT [DF_Banner_Status]  DEFAULT ('("Y")'),
	[OrderByNum] [int] NULL CONSTRAINT [DF_Banner_OrderByNum]  DEFAULT ((0)),
	[CreateTime] [datetime] NULL CONSTRAINT [DF_Banner_CreateTime]  DEFAULT (getdate()),
	[CreateUserId] [int] NULL,
	[UpdateTime] [datetime] NULL,
	[UpdateUserId] [int] NULL,
	[DeleteFlag] [char](1) NULL CONSTRAINT [DF_Banner_DeleteFlag]  DEFAULT ('("N")'),
	[DeleteTime] [datetime] NULL,
	[DeleteUserId] [int] NULL,
 CONSTRAINT [PK_Banner] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Dictionary]    Script Date: 2017/9/30 16:04:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Dictionary](
	[Key] [varchar](50) NOT NULL,
	[Value] [varchar](50) NULL,
 CONSTRAINT [PK_Dictionary_1] PRIMARY KEY CLUSTERED 
(
	[Key] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Menu]    Script Date: 2017/9/30 16:04:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Menu](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Remark] [nvarchar](50) NULL CONSTRAINT [DF_Menu_Remark]  DEFAULT (N'备注'),
	[ParentId] [int] NULL,
	[ArticleId] [int] NULL,
	[OrderByNum] [int] NULL CONSTRAINT [DF_Menu_OrderByNum]  DEFAULT ((0)),
	[CreateTime] [datetime] NULL CONSTRAINT [DF_Menu_CreateTime]  DEFAULT (getdate()),
	[CreateUserId] [int] NULL,
	[UpdateTime] [datetime] NULL,
	[UpdateUserId] [int] NULL,
	[DeleteFlag] [char](1) NULL CONSTRAINT [DF_Menu_DeleteFlag]  DEFAULT ('("N")'),
	[DeleteTime] [datetime] NULL,
	[DeleteUserId] [int] NULL,
 CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Suggestion]    Script Date: 2017/9/30 16:04:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Suggestion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NULL,
	[Content] [nvarchar](max) NULL,
	[CreateTime] [datetime] NULL CONSTRAINT [DF_Suggestion_CreateTime]  DEFAULT (getdate()),
	[CreateUserId] [int] NULL,
	[UpdateTime] [datetime] NULL,
	[UpdateUserId] [int] NULL,
	[DeleteFlag] [char](1) NULL CONSTRAINT [DF_Suggestion_DeleteFlag]  DEFAULT ('("N")'),
	[DeleteTime] [datetime] NULL,
	[DeleteUserId] [int] NULL,
 CONSTRAINT [PK_Suggestion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[User]    Script Date: 2017/9/30 16:04:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
	[Name] [varchar](50) NULL,
	[Status] [nchar](10) NULL,
	[AdminFlag] [char](1) NULL,
	[Token] [varchar](50) NULL,
	[Expires] [datetime] NULL,
	[CreateTime] [datetime] NULL CONSTRAINT [DF_User_CreateTime]  DEFAULT (getdate()),
	[CreateUserId] [int] NULL,
	[UpdateTime] [datetime] NULL,
	[UpdateUserId] [int] NULL,
	[DeleteFlag] [char](1) NULL CONSTRAINT [DF_User_DeleteFlag]  DEFAULT ('("N")'),
	[DeleteTime] [datetime] NULL,
	[DeleteUserId] [int] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'标题' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Article', @level2type=N'COLUMN',@level2name=N'Title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'摘要' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Article', @level2type=N'COLUMN',@level2name=N'Abstract'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Article', @level2type=N'COLUMN',@level2name=N'Content'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Article', @level2type=N'COLUMN',@level2name=N'MenuId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Banner类型(P:PC;H:H5)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Banner', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态(Y:有效;N:无效)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Banner', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Menu', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'父ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Menu', @level2type=N'COLUMN',@level2name=N'ParentId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文章ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Menu', @level2type=N'COLUMN',@level2name=N'ArticleId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Menu', @level2type=N'COLUMN',@level2name=N'OrderByNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'标题' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Suggestion', @level2type=N'COLUMN',@level2name=N'Title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Suggestion', @level2type=N'COLUMN',@level2name=N'Content'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'token' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'Token'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'(token过期时间)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'Expires'
GO
