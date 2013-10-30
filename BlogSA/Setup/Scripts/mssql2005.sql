IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sites]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Sites](
	[SiteID] [int] IDENTITY(1,1) NOT NULL,
	[ParentID] [int] NOT NULL CONSTRAINT [DF_Sites_ParentID]  DEFAULT ((0)),
	[UserID] [int] NOT NULL,
	[Code] [nvarchar](255) NULL,
	[State] [smallint] NOT NULL CONSTRAINT [DF_Sites_State]  DEFAULT ((0)),
	[CreateDate] [datetime] NOT NULL CONSTRAINT [DF_Sites_CreateDate]  DEFAULT (getdate()),
 CONSTRAINT [PK_Sites] PRIMARY KEY CLUSTERED 
(
	[SiteID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Posts]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Posts](
	[PostID] [int] IDENTITY(1,1) NOT NULL,
	[ParentID] [int] NOT NULL CONSTRAINT [DF_Posts_ParentID] DEFAULT ((0)),
	[SiteID] [int] NOT NULL CONSTRAINT [DF_Posts_SiteID] DEFAULT ((1)),
	[UserID] [int] NOT NULL,
	[LanguageCode] [nvarchar](20) NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[Code] [nvarchar](255) NULL,
	[State] [smallint] NOT NULL CONSTRAINT [DF_Posts_State]  DEFAULT ((0)),
	[AddComment] [bit] NOT NULL CONSTRAINT [DF_Posts_AddComment]  DEFAULT ((1)),
	[CreateDate] [datetime] NOT NULL CONSTRAINT [DF_Posts_CreateDate]  DEFAULT (getdate()),
	[ModifyDate] [datetime] NULL,
	[Type] [smallint] NOT NULL CONSTRAINT [DF_Posts_Type]  DEFAULT ((0)),
	[ReadCount] [int] NOT NULL CONSTRAINT [DF_Posts_ReadCount]  DEFAULT ((0)),
	[Show] [smallint] NOT NULL CONSTRAINT [DF_Posts_Show]  DEFAULT ((1)),
 CONSTRAINT [PK_Posts] PRIMARY KEY CLUSTERED 
(
	[PostID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Terms]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Terms](
	[TermID] [int] IDENTITY(1,1) NOT NULL,
	[SiteID] [int] NOT NULL CONSTRAINT [DF_Terms_SiteID] DEFAULT ((1)),
	[Name] [nvarchar](80) NOT NULL,
	[Description] [nvarchar](160) NULL,
	[Code] [nvarchar](80) NOT NULL,
	[Type] [nvarchar](12) NOT NULL CONSTRAINT [DF_Terms_Type]  DEFAULT (N'category'),
	[SubID] [int] NOT NULL CONSTRAINT [DF_Categories_SubID]  DEFAULT ((0)),
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[TermID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Settings]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Settings](
	[SettingID] [int] IDENTITY(1,1) NOT NULL,
	[SiteID] [int] NOT NULL CONSTRAINT [DF_Settings_SiteID] DEFAULT ((1)),
	[Name] [nvarchar](100) NOT NULL,
	[Value] [nvarchar](max) NOT NULL,
	[Title] [nvarchar](120) NULL,
	[Description] [nvarchar](max) NULL,
	[Main] [bit] NOT NULL CONSTRAINT [DF_Settings_Main]  DEFAULT ((0)),
	[Sort] [int] NOT NULL CONSTRAINT [DF_Settings_Sort]  DEFAULT ((0)),
	[Visible] [bit] NOT NULL CONSTRAINT [DF_Settings_Visible]  DEFAULT ((1)),
 CONSTRAINT [PK_Settings] PRIMARY KEY CLUSTERED 
(
	[SettingID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TermsTo]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TermsTo](
	[ObjectID] [int] NOT NULL,
	[TermID] [int] NOT NULL,
	[Type] [nvarchar](12) NOT NULL CONSTRAINT [DF_TermsTo_Type]  DEFAULT (N'category'),
 CONSTRAINT [PK_TermsTo] PRIMARY KEY CLUSTERED 
(
	[ObjectID],[TermID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[SiteID] [int] NOT NULL CONSTRAINT [DF_Users_SiteID] DEFAULT ((1)),
	[UserName] [nvarchar](16) NOT NULL,
	[Password] [nvarchar](32) NOT NULL,
	[Name] [nvarchar](60) NOT NULL,
	[Email] [nvarchar](255) NOT NULL,
	[WebPage] [nvarchar](255) NULL,
	[CreateDate] [datetime] NOT NULL CONSTRAINT [DF_Users_CreateDate]  DEFAULT (getdate()),
	[LastLoginDate] [datetime] NULL,
	[State] [smallint] NOT NULL CONSTRAINT [DF_Users_State]  DEFAULT ((0)),
	[Role] [nvarchar](16) NOT NULL CONSTRAINT [DF_Users_Role]  DEFAULT (N'user'),
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Widgets]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Widgets](
	[WidgetID] [int] IDENTITY(1,1) NOT NULL,
	[SiteID] [int] NOT NULL CONSTRAINT [DF_Widgets_SiteID] DEFAULT ((1)),
	[Title] [nvarchar](160) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[FolderName] [nvarchar](160) NULL,
	[Type] [smallint] NULL,
	[Sort] [int] NOT NULL CONSTRAINT [DF_Addons_Sort]  DEFAULT ((0)),
	[Visible] [bit] NOT NULL CONSTRAINT [DF_Addons_Visible]  DEFAULT ((1)),
	[PlaceHolder] [nvarchar](50) NOT NULL CONSTRAINT [DF_Widgets_PlaceHolder]  DEFAULT (N'Default'),
	[LanguageCode] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_Addons] PRIMARY KEY CLUSTERED 
(
	[WidgetID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Comments]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Comments](
	[CommentID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL CONSTRAINT [DF_Comments_UserID]  DEFAULT ((0)),
	[PostID] [int] NOT NULL,
	[Name] [nvarchar](60) NOT NULL,
	[Comment] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](160) NOT NULL,
	[WebPage] [nvarchar](160) NULL,
	[Ip] [nvarchar](15) NOT NULL CONSTRAINT [DF_Comments_Ip]  DEFAULT (N'0.0.0.0'),
	[CreateDate] [datetime] NOT NULL CONSTRAINT [DF_Comments_CreateDate]  DEFAULT (getdate()),
	[Approve] [bit] NOT NULL CONSTRAINT [DF_Comments_Approve]  DEFAULT ((0)),
	[NotifyMe] [bit] NOT NULL CONSTRAINT [DF_Comments_NotifyMe]  DEFAULT ((0)),
 CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED 
(
	[CommentID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Links]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Links](
	[LinkID] [int] IDENTITY(1,1) NOT NULL,
	[SiteID] [int] NOT NULL CONSTRAINT [DF_Links_SiteID] DEFAULT ((1)),
	[Name] [nvarchar](100) NOT NULL,
	[Url] [nvarchar](160) NOT NULL CONSTRAINT [DF_Links_Url]  DEFAULT (N'#'),
	[Description] [nvarchar](max) NULL,
	[Target] [nvarchar](50) NOT NULL CONSTRAINT [DF_Links_Target]  DEFAULT (N'_self'),
	[LanguageCode] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_Links] PRIMARY KEY CLUSTERED 
(
	[LinkID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MenuGroups]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MenuGroups](
	[MenuGroupID] [int] IDENTITY(1,1) NOT NULL,
	[SiteID] [int] NOT NULL CONSTRAINT [DF_MenuGroups_SiteID] DEFAULT ((1)),
	[Title] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Code] [nvarchar](255) NOT NULL,
	[LanguageCode] [nvarchar](20) NOT NULL,
	[Default] [bit] NOT NULL,
 CONSTRAINT [PK_MenuGroups] PRIMARY KEY CLUSTERED 
(
	[MenuGroupID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Menus]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Menus](
	[MenuID] [int] IDENTITY(1,1) NOT NULL,
	[MenuGroupID] [int] NOT NULL,
	[ParentID] [int] NOT NULL,
	[ObjectID] [int] NOT NULL,
	[ObjectType] [smallint] NOT NULL,
	[MenuType] [smallint] NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Url] [nvarchar](255) NOT NULL,
	[Target] [nvarchar](120) NOT NULL,
	[Sort] [smallint] NOT NULL,
 CONSTRAINT [PK_Menus] PRIMARY KEY CLUSTERED 
(
	[MenuID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END