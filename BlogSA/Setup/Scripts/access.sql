/*-----< SITES >-----*/
CREATE TABLE Sites ([SiteID] counter primary key,[ParentID] int not null,[UserID] int not null,[Code] varchar(120) not null,[State] smallint not null default 0)
/*-----< COMMENTS >-----*/
CREATE TABLE Comments ([CommentID] counter primary key,[UserID] integer not null default 0,[PostID] integer not null,[Name] varchar(60) not null,[Comment] memo not null,[Email] varchar(160) not null,[WebPage] varchar(160),[Ip] varchar(15) not null default "0.0.0.0",[CreateDate] datetime default now() not null,[Approve] bit not null default 0,[NotifyMe] bit not null default 0)
/*-----< LINKS >-----*/
CREATE TABLE Links ([LinkID] counter primary key,[SiteID] int not null,[Name] varchar(100) not null,[Url] varchar(160) not null,[Description] memo,[Target] varchar(50) not null default "_blank",[LanguageCode] varchar(20) not null)
/*-----< POSTS >-----*/
CREATE TABLE Posts ([PostID] counter primary key,[SiteID] int not null default 0,[ParentID] int not null default 0,[UserID] integer not null,[Title] varchar(255) not null,[LanguageCode] varchar(20) not null,[Content] memo not null,[Code] varchar(255),[State] smallint not null default 0,[AddComment] bit not null default 0,[CreateDate] datetime not null default now(),[ModifyDate] datetime,[Type] smallint not null default 0,[ReadCount] int not null default 0,[Show] smallint not null default 1)
/*-----< SETTINGS >-----*/
CREATE TABLE Settings ([SettingID] counter primary key,[SiteID] int not null,[Name] varchar(100) not null,[Value] memo not null,[Title] varchar(120),[Description] memo,[Main] bit not null default 0,[Sort] integer not null default 0,[Visible] bit not null default 1) 
/*-----< TERMS >-----*/
CREATE TABLE Terms ([TermID] counter primary key,[SiteID] int not null,[Name] varchar(80) not null,[Description] varchar(160),[Code] varchar(80) not null,[Type] varchar(12) not null,[SubID] integer not null default 0)
/*-----< TERMSTO >-----*/
CREATE TABLE TermsTo ([ObjectID] integer not null,[TermID] int not null,[Type] varchar(12) not null)
/*-----< USERS >-----*/
CREATE TABLE Users ([UserID] counter primary key,[SiteID] int not null,[UserName] varchar(16) not null,[Password] varchar(32) not null,[Name] varchar(60) not null,[Email] varchar(255) not null,[WebPage] varchar(255),[CreateDate] datetime not null default now(),[LastLoginDate] datetime,[State] smallint not null default 0,[Role] varchar(16) not null default "user")
/*-----< WIDGETS >-----*/
CREATE TABLE Widgets ([WidgetID] counter primary key,[SiteID] int not null,[LanguageCode] varchar(20) not null,[Title] varchar(160) not null,[Description] memo,[FolderName] varchar(160),[Type] smallint,[Sort] integer not null default 0,[Visible] bit not null default 1,[PlaceHolder] varchar(50) not null default "Default")
/*-----< MENU GROUPS >-----*/
CREATE TABLE MenuGroups ([MenuGroupID] counter primary key,[SiteID] int not null,[Title] varchar(255) not null,[Description] memo not null,[Code] varchar(255) not null,[Default] bit not null,[LanguageCode] varchar(20) not null)
/*-----< MENUS >-----*/
CREATE TABLE Menus ([MenuID] counter primary key,[MenuGroupID] integer not null,[ParentID] integer not null,[ObjectID] integer not null,[ObjectType] smallint not null,[MenuType] smallint not null,[Title] varchar(255) not null,[Description] memo not null,[Url] varchar(255) not null,[Target] varchar(120) not null,[Sort] smallint not null)
/*-----< METAS >-----*/
CREATE TABLE Metas ([SiteID] int not null,[Key] varchar(160) not null,[Value] memo not null,[ValueType] smallint not null,[ExpressionType] smallint not null,[Expression] memo,[ObjectID] int not null,[ObjectType] smallint not null,[Sort] int not null default 0,[Visible] bit not null default 1,[ReadOnly] bit not null default 0) 
