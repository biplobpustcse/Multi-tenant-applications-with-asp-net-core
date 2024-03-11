#### Tenant Database

I have to maintain a Tenant database to manage multiple customer Databases. I am using two tables, Like
```
CREATE DATABASE [TenantDB]
```
#### Tenants Table
```
USE [TenantDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tenants](
	[CustomerId] [int] NOT NULL,
	[Customer] [varchar](50) NOT NULL,
	[Host] [varchar](50) NULL,
	[SubDomain] [varchar](50) NOT NULL,
	[Logo] [varchar](50) NULL,
	[ThemeColor] [varchar](50) NULL,
	[ConnectionString] [varchar](max) NOT NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
```
#### TenantUsers Table
```
USE [TenantDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TenantUsers](
	[Id] [int] NOT NULL,
	[CustomerId] [int] NOT NULL,
	[Email] [varchar](50) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
```

```
USE [TenantDB]
GO
INSERT [dbo].[Tenants] ([CustomerId], [Customer], [Host], [SubDomain], [Logo], [ThemeColor], [ConnectionString]) VALUES (1, N'Red Customer', N'localhost:5057', N'rc', NULL, N'Red', N'Server=BIPLOB\SQL2019;Database=App-DB1; user id=sa; password=123456; MultipleActiveResultSets=true')
GO
INSERT [dbo].[Tenants] ([CustomerId], [Customer], [Host], [SubDomain], [Logo], [ThemeColor], [ConnectionString]) VALUES (2, N'Green Customer', N'localhost:5057', N'gc', NULL, N'Green', N'Server=BIPLOB\SQL2019;Database=App-DB2; user id=sa; password=123456; MultipleActiveResultSets=true')
GO
INSERT [dbo].[TenantUsers] ([Id], [CustomerId], [Email]) VALUES (1, 1, N'rc@example.com')
GO
INSERT [dbo].[TenantUsers] ([Id], [CustomerId], [Email]) VALUES (2, 2, N'gc@example.com')
GO
```
#### Application Database

Now I will add two application databases; these databases will access by a single web portal. Databases are like App-DB1 and App-DB2. Each database has one table, like Users.
```
CREATE Database [App-DB1]
GO
USE [App-DB1]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] NOT NULL,
	[UserName] [varchar](50) NULL,
	[UserEmail] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

USE [App-DB1]
GO
INSERT [dbo].[Users] ([UserId], [UserName], [UserEmail], [Password]) VALUES (1, N'Red Customer', N'rc@example.com', N'123456')
GO
```
```
CREATE Database [App-DB2]
GO
USE [App-DB2]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 04/15/23 5:26:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] NOT NULL,
	[UserName] [varchar](50) NULL,
	[UserEmail] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

USE [App-DB2]
GO
INSERT [dbo].[Users] ([UserId], [UserName], [UserEmail], [Password]) VALUES (1, N'Green Customer', N'gc@example.com', N'123456')
GO
```
