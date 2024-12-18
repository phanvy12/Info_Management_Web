USE [master]
GO
/****** Object:  Database [InfoMngDB]    Script Date: 24/11/2024 4:38:25 CH ******/
CREATE DATABASE [InfoMngDB]
GO
USE [InfoMngDB]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 24/11/2024 4:38:25 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Fullname] [nvarchar](50) NOT NULL,
	[Role] [int] NOT NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Accounts] ON 

INSERT [dbo].[Accounts] ([Id], [Username], [Password], [Fullname], [Role]) VALUES (1, N'admin', N'1', N'ADMIN', 1)
INSERT [dbo].[Accounts] ([Id], [Username], [Password], [Fullname], [Role]) VALUES (2, N'QTAnh@gv', N'1', N'Quách Tuấn Anh', 3)
INSERT [dbo].[Accounts] ([Id], [Username], [Password], [Fullname], [Role]) VALUES (3, N'48.01.103.001', N'1', N'Bùi Thị Ngọc An', 2)
SET IDENTITY_INSERT [dbo].[Accounts] OFF
GO
USE [master]
GO
ALTER DATABASE [InfoMngDB] SET  READ_WRITE 
GO
