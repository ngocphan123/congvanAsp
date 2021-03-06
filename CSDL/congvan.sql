USE [CongVan]
GO
/****** Object:  Table [dbo].[assignment]    Script Date: 5/17/2017 12:27:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[assignment](
	[id] [int] NOT NULL,
	[id_dispatch] [int] NULL,
	[id_department] [int] NULL,
	[assingtime] [datetime] NULL,
	[completiontime] [datetime] NULL,
	[userid_command] [int] NULL,
	[userid_follow] [int] NULL,
	[userid_perform] [int] NULL,
	[work_content] [nvarchar](500) NULL,
	[result] [text] NULL,
	[attach_file] [nvarchar](255) NULL,
 CONSTRAINT [PK_assignment] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[cat]    Script Date: 5/17/2017 12:27:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cat](
	[id] [int] NOT NULL,
	[parentid] [int] NULL,
	[title] [nvarchar](255) NULL,
	[introduction] [text] NULL,
	[addtime] [datetime] NULL,
	[status] [int] NULL,
 CONSTRAINT [PK_cat] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[department]    Script Date: 5/17/2017 12:27:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[department](
	[id] [int] NOT NULL,
	[depcatid] [int] NULL,
	[title] [nvarchar](255) NULL,
	[weight] [int] NULL,
 CONSTRAINT [PK_Phong_Ban] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[department_cat]    Script Date: 5/17/2017 12:27:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[department_cat](
	[id] [int] NOT NULL,
	[title] [nvarchar](250) NULL,
	[weight] [int] NULL,
 CONSTRAINT [PK_department_cat] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[employee]    Script Date: 5/17/2017 12:27:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[employee](
	[userid] [int] NOT NULL,
	[iddepart] [int] NOT NULL,
	[office] [tinyint] NULL,
 CONSTRAINT [PK_employee] PRIMARY KEY CLUSTERED 
(
	[userid] ASC,
	[iddepart] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[follows]    Script Date: 5/17/2017 12:27:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[follows](
	[id] [int] NOT NULL,
	[id_dispatch] [int] NULL,
	[id_department] [int] NULL,
	[list_userid] [nvarchar](255) NULL,
	[list_timeview] [text] NULL,
	[list_hitstotal] [nvarchar](255) NULL,
 CONSTRAINT [PK_follows] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[groups_user]    Script Date: 5/17/2017 12:27:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[groups_user](
	[id] [int] NOT NULL,
	[title] [nvarchar](255) NULL,
	[description] [text] NULL,
	[group_type] [int] NULL,
	[leader_id] [int] NULL,
 CONSTRAINT [PK_groups_user] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[rows]    Script Date: 5/17/2017 12:27:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[rows](
	[id] [int] NOT NULL,
	[type] [int] NULL,
	[idcat] [int] NULL,
	[idtype] [int] NULL,
	[title] [nvarchar](255) NULL,
	[abstract] [text] NULL,
	[name_signer] [nvarchar](255) NULL,
	[name_initial] [nvarchar](255) NULL,
	[level_important] [int] NULL,
	[number_dispatch] [nvarchar](255) NULL,
	[number_text_come] [nvarchar](255) NULL,
	[note] [text] NULL,
	[publtime] [datetime] NULL,
	[date_iss] [smalldatetime] NULL,
	[date_first] [smalldatetime] NULL,
	[date_die] [smalldatetime] NULL,
	[from_org] [nvarchar](255) NULL,
	[to_org] [nvarchar](255) NULL,
	[dep_catid] [int] NULL,
	[to_depid] [int] NULL,
	[attach_file] [nvarchar](255) NULL,
	[status] [tinyint] NULL,
	[term_view] [datetime] NULL,
	[reply] [tinyint] NULL,
	[groups_view] [nvarchar](50) NULL,
	[views] [int] NULL,
 CONSTRAINT [PK_rows] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[type]    Script Date: 5/17/2017 12:27:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[type](
	[id] [int] NOT NULL,
	[parentid] [int] NULL,
	[title] [nvarchar](255) NULL,
	[weight] [int] NULL,
	[status] [int] NULL,
 CONSTRAINT [PK_type] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[users]    Script Date: 5/17/2017 12:27:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[id] [int] NOT NULL,
	[group_id] [int] NULL,
	[username] [nvarchar](50) NULL,
	[password] [nvarchar](150) NULL,
	[email] [nvarchar](150) NULL,
	[first_name] [nvarchar](150) NULL,
	[last_name] [nvarchar](150) NULL,
	[gender] [tinyint] NULL,
	[photo] [nvarchar](255) NULL,
	[birthday] [smalldatetime] NULL,
	[active] [tinyint] NULL,
 CONSTRAINT [PK_users] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[assignment]  WITH CHECK ADD  CONSTRAINT [FK_assignment_department] FOREIGN KEY([id_department])
REFERENCES [dbo].[department] ([id])
GO
ALTER TABLE [dbo].[assignment] CHECK CONSTRAINT [FK_assignment_department]
GO
ALTER TABLE [dbo].[assignment]  WITH CHECK ADD  CONSTRAINT [FK_assignment_rows] FOREIGN KEY([id_dispatch])
REFERENCES [dbo].[rows] ([id])
GO
ALTER TABLE [dbo].[assignment] CHECK CONSTRAINT [FK_assignment_rows]
GO
ALTER TABLE [dbo].[department]  WITH CHECK ADD  CONSTRAINT [FK_department_department_cat] FOREIGN KEY([depcatid])
REFERENCES [dbo].[department_cat] ([id])
GO
ALTER TABLE [dbo].[department] CHECK CONSTRAINT [FK_department_department_cat]
GO
ALTER TABLE [dbo].[employee]  WITH CHECK ADD  CONSTRAINT [FK_employee_department] FOREIGN KEY([iddepart])
REFERENCES [dbo].[department] ([id])
GO
ALTER TABLE [dbo].[employee] CHECK CONSTRAINT [FK_employee_department]
GO
ALTER TABLE [dbo].[employee]  WITH CHECK ADD  CONSTRAINT [FK_employee_users] FOREIGN KEY([userid])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[employee] CHECK CONSTRAINT [FK_employee_users]
GO
ALTER TABLE [dbo].[follows]  WITH CHECK ADD  CONSTRAINT [FK_follows_department] FOREIGN KEY([id_department])
REFERENCES [dbo].[department] ([id])
GO
ALTER TABLE [dbo].[follows] CHECK CONSTRAINT [FK_follows_department]
GO
ALTER TABLE [dbo].[follows]  WITH CHECK ADD  CONSTRAINT [FK_follows_rows] FOREIGN KEY([id_dispatch])
REFERENCES [dbo].[rows] ([id])
GO
ALTER TABLE [dbo].[follows] CHECK CONSTRAINT [FK_follows_rows]
GO
ALTER TABLE [dbo].[rows]  WITH CHECK ADD  CONSTRAINT [FK_rows_cat] FOREIGN KEY([idcat])
REFERENCES [dbo].[cat] ([id])
GO
ALTER TABLE [dbo].[rows] CHECK CONSTRAINT [FK_rows_cat]
GO
ALTER TABLE [dbo].[rows]  WITH CHECK ADD  CONSTRAINT [FK_rows_type] FOREIGN KEY([type])
REFERENCES [dbo].[type] ([id])
GO
ALTER TABLE [dbo].[rows] CHECK CONSTRAINT [FK_rows_type]
GO
ALTER TABLE [dbo].[users]  WITH CHECK ADD  CONSTRAINT [FK_users_groups_user] FOREIGN KEY([group_id])
REFERENCES [dbo].[groups_user] ([id])
GO
ALTER TABLE [dbo].[users] CHECK CONSTRAINT [FK_users_groups_user]
GO
