USE [master]
GO
/****** Object:  Database [WebMovie]    Script Date: 13/12/2021 10:57:38 PM ******/
CREATE DATABASE [WebMovie]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'WebMovie', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\WebMovie.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'WebMovie_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\WebMovie_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [WebMovie] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [WebMovie].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [WebMovie] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [WebMovie] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [WebMovie] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [WebMovie] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [WebMovie] SET ARITHABORT OFF 
GO
ALTER DATABASE [WebMovie] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [WebMovie] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [WebMovie] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [WebMovie] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [WebMovie] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [WebMovie] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [WebMovie] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [WebMovie] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [WebMovie] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [WebMovie] SET  ENABLE_BROKER 
GO
ALTER DATABASE [WebMovie] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [WebMovie] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [WebMovie] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [WebMovie] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [WebMovie] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [WebMovie] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [WebMovie] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [WebMovie] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [WebMovie] SET  MULTI_USER 
GO
ALTER DATABASE [WebMovie] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [WebMovie] SET DB_CHAINING OFF 
GO
ALTER DATABASE [WebMovie] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [WebMovie] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [WebMovie] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [WebMovie] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [WebMovie] SET QUERY_STORE = OFF
GO
USE [WebMovie]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 13/12/2021 10:57:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[displayName] [nvarchar](100) NULL,
	[userName] [varchar](100) NULL,
	[pass] [varchar](1000) NULL,
	[email] [varchar](255) NULL,
	[role] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 13/12/2021 10:57:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CategoryDetails]    Script Date: 13/12/2021 10:57:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategoryDetails](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[movieId] [int] NULL,
	[categoryId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comment]    Script Date: 13/12/2021 10:57:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comment](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[movieId] [int] NULL,
	[accountId] [int] NULL,
	[content] [nvarchar](4000) NULL,
	[commentDate] [datetime] NULL,
	[fatherComment] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[episode]    Script Date: 13/12/2021 10:57:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[episode](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[episodeNumber] [int] NULL,
	[episodeName] [varchar](20) NULL,
	[movieId] [int] NULL,
	[video] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[follow]    Script Date: 13/12/2021 10:57:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[follow](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[accountId] [int] NULL,
	[movieId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[history]    Script Date: 13/12/2021 10:57:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[history](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[accountId] [int] NULL,
	[episodeId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Movie]    Script Date: 13/12/2021 10:57:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Movie](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](255) NULL,
	[anotherName] [nvarchar](255) NULL,
	[image] [varchar](255) NULL,
	[releaseYear] [int] NULL,
	[description] [nvarchar](4000) NULL,
	[duration] [nvarchar](30) NULL,
	[seriesId] [int] NULL,
	[part] [int] NULL,
	[status] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MovieRate]    Script Date: 13/12/2021 10:57:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MovieRate](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[accountId] [int] NULL,
	[movieId] [int] NULL,
	[rateNumber] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Series]    Script Date: 13/12/2021 10:57:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Series](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[viewsByDate]    Script Date: 13/12/2021 10:57:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[viewsByDate](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[movieId] [int] NULL,
	[day] [date] NULL,
	[numView] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Account] ON 

INSERT [dbo].[Account] ([id], [displayName], [userName], [pass], [email], [role]) VALUES (1, N'admin', N'admin', N'21232f297a57a5a743894a0e4a801fc3', N'admin@gmail.com', 0)
INSERT [dbo].[Account] ([id], [displayName], [userName], [pass], [email], [role]) VALUES (2, N'Kiên Trần', N'kien', N'5d2297b2f56654636090aaad75d0578f', N'kientr201@gmail.com', 1)
INSERT [dbo].[Account] ([id], [displayName], [userName], [pass], [email], [role]) VALUES (4, N'test', N'test', N'202cb962ac59075b964b07152d234b70', N'vip2k18@gmail.com', 0)
SET IDENTITY_INSERT [dbo].[Account] OFF
GO
ALTER TABLE [dbo].[Comment] ADD  DEFAULT (getdate()) FOR [commentDate]
GO
ALTER TABLE [dbo].[CategoryDetails]  WITH CHECK ADD  CONSTRAINT [FK__CategoryD__categ__34C8D9D1] FOREIGN KEY([categoryId])
REFERENCES [dbo].[Category] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CategoryDetails] CHECK CONSTRAINT [FK__CategoryD__categ__34C8D9D1]
GO
ALTER TABLE [dbo].[CategoryDetails]  WITH CHECK ADD  CONSTRAINT [FK__CategoryD__movie__33D4B598] FOREIGN KEY([movieId])
REFERENCES [dbo].[Movie] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CategoryDetails] CHECK CONSTRAINT [FK__CategoryD__movie__33D4B598]
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK__Comment__account__3B75D760] FOREIGN KEY([accountId])
REFERENCES [dbo].[Account] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK__Comment__account__3B75D760]
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD FOREIGN KEY([fatherComment])
REFERENCES [dbo].[Comment] ([id])
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK__Comment__movieId__3A81B327] FOREIGN KEY([movieId])
REFERENCES [dbo].[Movie] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK__Comment__movieId__3A81B327]
GO
ALTER TABLE [dbo].[episode]  WITH CHECK ADD  CONSTRAINT [FK__episode__movieId__37A5467C] FOREIGN KEY([movieId])
REFERENCES [dbo].[Movie] ([id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[episode] CHECK CONSTRAINT [FK__episode__movieId__37A5467C]
GO
ALTER TABLE [dbo].[follow]  WITH CHECK ADD  CONSTRAINT [FK__follow__accountI__403A8C7D] FOREIGN KEY([accountId])
REFERENCES [dbo].[Account] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[follow] CHECK CONSTRAINT [FK__follow__accountI__403A8C7D]
GO
ALTER TABLE [dbo].[follow]  WITH CHECK ADD  CONSTRAINT [FK__follow__movieId__412EB0B6] FOREIGN KEY([movieId])
REFERENCES [dbo].[Movie] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[follow] CHECK CONSTRAINT [FK__follow__movieId__412EB0B6]
GO
ALTER TABLE [dbo].[history]  WITH CHECK ADD  CONSTRAINT [FK__history__account__440B1D61] FOREIGN KEY([accountId])
REFERENCES [dbo].[Account] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[history] CHECK CONSTRAINT [FK__history__account__440B1D61]
GO
ALTER TABLE [dbo].[history]  WITH CHECK ADD  CONSTRAINT [FK__history__episode__44FF419A] FOREIGN KEY([episodeId])
REFERENCES [dbo].[episode] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[history] CHECK CONSTRAINT [FK__history__episode__44FF419A]
GO
ALTER TABLE [dbo].[Movie]  WITH CHECK ADD  CONSTRAINT [FK__Movie__seriesId__2A4B4B5E] FOREIGN KEY([seriesId])
REFERENCES [dbo].[Series] ([id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Movie] CHECK CONSTRAINT [FK__Movie__seriesId__2A4B4B5E]
GO
ALTER TABLE [dbo].[MovieRate]  WITH CHECK ADD  CONSTRAINT [FK__MovieRate__accou__300424B4] FOREIGN KEY([accountId])
REFERENCES [dbo].[Account] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MovieRate] CHECK CONSTRAINT [FK__MovieRate__accou__300424B4]
GO
ALTER TABLE [dbo].[MovieRate]  WITH CHECK ADD  CONSTRAINT [FK__MovieRate__movie__30F848ED] FOREIGN KEY([movieId])
REFERENCES [dbo].[Movie] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MovieRate] CHECK CONSTRAINT [FK__MovieRate__movie__30F848ED]
GO
ALTER TABLE [dbo].[viewsByDate]  WITH CHECK ADD  CONSTRAINT [FK__viewsByDa__movie__2D27B809] FOREIGN KEY([movieId])
REFERENCES [dbo].[Movie] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[viewsByDate] CHECK CONSTRAINT [FK__viewsByDa__movie__2D27B809]
GO
USE [master]
GO
ALTER DATABASE [WebMovie] SET  READ_WRITE 
GO
