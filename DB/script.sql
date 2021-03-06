USE [master]
GO
/****** Object:  Database [TestGeneratorDB]    Script Date: 6/28/2018 5:21:47 PM ******/
CREATE DATABASE [TestGeneratorDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TestGeneratorDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\TestGeneratorDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TestGeneratorDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\TestGeneratorDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [TestGeneratorDB] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TestGeneratorDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TestGeneratorDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TestGeneratorDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TestGeneratorDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TestGeneratorDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TestGeneratorDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [TestGeneratorDB] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [TestGeneratorDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TestGeneratorDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TestGeneratorDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TestGeneratorDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TestGeneratorDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TestGeneratorDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TestGeneratorDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TestGeneratorDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TestGeneratorDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [TestGeneratorDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TestGeneratorDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TestGeneratorDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TestGeneratorDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TestGeneratorDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TestGeneratorDB] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [TestGeneratorDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TestGeneratorDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [TestGeneratorDB] SET  MULTI_USER 
GO
ALTER DATABASE [TestGeneratorDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TestGeneratorDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TestGeneratorDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TestGeneratorDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TestGeneratorDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TestGeneratorDB] SET QUERY_STORE = OFF
GO
USE [TestGeneratorDB]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [TestGeneratorDB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 6/28/2018 5:21:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Answer]    Script Date: 6/28/2018 5:21:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Answer](
	[AnswerId] [int] IDENTITY(1,1) NOT NULL,
	[AnswerPicture] [varbinary](max) NULL,
	[AnswerText] [nvarchar](max) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
	[Note] [nvarchar](max) NULL,
 CONSTRAINT [PK_Answer] PRIMARY KEY CLUSTERED 
(
	[AnswerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Area]    Script Date: 6/28/2018 5:21:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Area](
	[AreaId] [int] IDENTITY(1,1) NOT NULL,
	[AreaName] [nvarchar](max) NULL,
	[YearOfStudyId] [int] NULL,
	[SubjectId] [int] NULL,
	[TestId] [int] NULL,
 CONSTRAINT [PK_Area] PRIMARY KEY CLUSTERED 
(
	[AreaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DifficultyLevel]    Script Date: 6/28/2018 5:21:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DifficultyLevel](
	[DifficultyLevelId] [int] IDENTITY(1,1) NOT NULL,
	[Level] [int] NOT NULL,
 CONSTRAINT [PK_DifficultyLevel] PRIMARY KEY CLUSTERED 
(
	[DifficultyLevelId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NumberOfPoints]    Script Date: 6/28/2018 5:21:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NumberOfPoints](
	[NumberOfPointsId] [int] IDENTITY(1,1) NOT NULL,
	[Points] [int] NOT NULL,
 CONSTRAINT [PK_NumberOfPoints] PRIMARY KEY CLUSTERED 
(
	[NumberOfPointsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Question]    Script Date: 6/28/2018 5:21:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Question](
	[QuestionId] [int] IDENTITY(1,1) NOT NULL,
	[AnswerId] [int] NULL,
	[DifficultyLevelId] [int] NULL,
	[NumberOfPointsId] [int] NULL,
	[QuestionTypeId] [int] NULL,
	[StatusId] [int] NULL,
	[SubjectId] [int] NULL,
	[VisibilityId] [int] NULL,
	[QuestionImage] [varbinary](max) NULL,
	[QuestionText] [nvarchar](max) NULL,
	[AreaId] [int] NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
	[Note] [nvarchar](max) NULL,
	[TestId] [int] NULL,
 CONSTRAINT [PK_Question] PRIMARY KEY CLUSTERED 
(
	[QuestionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuestionType]    Script Date: 6/28/2018 5:21:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuestionType](
	[QuestionTypeId] [int] IDENTITY(1,1) NOT NULL,
	[QuesstionTypeName] [nvarchar](max) NULL,
 CONSTRAINT [PK_QuestionType] PRIMARY KEY CLUSTERED 
(
	[QuestionTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Status]    Script Date: 6/28/2018 5:21:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Status](
	[StatusId] [int] IDENTITY(1,1) NOT NULL,
	[StatusName] [nvarchar](max) NULL,
 CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
(
	[StatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Subject]    Script Date: 6/28/2018 5:21:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subject](
	[SubjectId] [int] IDENTITY(1,1) NOT NULL,
	[SubjectName] [nvarchar](max) NULL,
 CONSTRAINT [PK_Subject] PRIMARY KEY CLUSTERED 
(
	[SubjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Test]    Script Date: 6/28/2018 5:21:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Test](
	[TestId] [int] IDENTITY(1,1) NOT NULL,
	[SubjectId] [int] NULL,
	[YearOfStudyId] [int] NULL,
	[MaxDifficultyLevelDifficultyLevelId] [int] NULL,
	[TotalDifficultyLevel] [float] NOT NULL,
	[NumberOfQuestions] [int] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[Group] [nvarchar](max) NULL,
	[Note] [nvarchar](max) NULL,
	[TestGroup] [nvarchar](max) NULL,
 CONSTRAINT [PK_Test] PRIMARY KEY CLUSTERED 
(
	[TestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TestArea]    Script Date: 6/28/2018 5:21:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TestArea](
	[TestAreaId] [int] IDENTITY(1,1) NOT NULL,
	[AreaId] [int] NULL,
	[TestId] [int] NULL,
 CONSTRAINT [PK_TestArea] PRIMARY KEY CLUSTERED 
(
	[TestAreaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TestQuestion]    Script Date: 6/28/2018 5:21:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TestQuestion](
	[TestQuestionId] [int] IDENTITY(1,1) NOT NULL,
	[QuestionId] [int] NULL,
	[TestId] [int] NULL,
 CONSTRAINT [PK_TestQuestion] PRIMARY KEY CLUSTERED 
(
	[TestQuestionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Visibility]    Script Date: 6/28/2018 5:21:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Visibility](
	[VisibilityId] [int] IDENTITY(1,1) NOT NULL,
	[VisibilityName] [nvarchar](max) NULL,
 CONSTRAINT [PK_Visibility] PRIMARY KEY CLUSTERED 
(
	[VisibilityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[YearOfStudy]    Script Date: 6/28/2018 5:21:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[YearOfStudy](
	[YearOfStudyId] [int] IDENTITY(1,1) NOT NULL,
	[YearOfStudyName] [nvarchar](max) NULL,
 CONSTRAINT [PK_YearOfStudy] PRIMARY KEY CLUSTERED 
(
	[YearOfStudyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Index [IX_Area_SubjectId]    Script Date: 6/28/2018 5:21:48 PM ******/
CREATE NONCLUSTERED INDEX [IX_Area_SubjectId] ON [dbo].[Area]
(
	[SubjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Area_TestId]    Script Date: 6/28/2018 5:21:48 PM ******/
CREATE NONCLUSTERED INDEX [IX_Area_TestId] ON [dbo].[Area]
(
	[TestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Area_YearOfStudyId]    Script Date: 6/28/2018 5:21:48 PM ******/
CREATE NONCLUSTERED INDEX [IX_Area_YearOfStudyId] ON [dbo].[Area]
(
	[YearOfStudyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Question_AnswerId]    Script Date: 6/28/2018 5:21:48 PM ******/
CREATE NONCLUSTERED INDEX [IX_Question_AnswerId] ON [dbo].[Question]
(
	[AnswerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Question_AreaId]    Script Date: 6/28/2018 5:21:48 PM ******/
CREATE NONCLUSTERED INDEX [IX_Question_AreaId] ON [dbo].[Question]
(
	[AreaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Question_DifficultyLevelId]    Script Date: 6/28/2018 5:21:48 PM ******/
CREATE NONCLUSTERED INDEX [IX_Question_DifficultyLevelId] ON [dbo].[Question]
(
	[DifficultyLevelId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Question_NumberOfPointsId]    Script Date: 6/28/2018 5:21:48 PM ******/
CREATE NONCLUSTERED INDEX [IX_Question_NumberOfPointsId] ON [dbo].[Question]
(
	[NumberOfPointsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Question_QuestionTypeId]    Script Date: 6/28/2018 5:21:48 PM ******/
CREATE NONCLUSTERED INDEX [IX_Question_QuestionTypeId] ON [dbo].[Question]
(
	[QuestionTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Question_StatusId]    Script Date: 6/28/2018 5:21:48 PM ******/
CREATE NONCLUSTERED INDEX [IX_Question_StatusId] ON [dbo].[Question]
(
	[StatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Question_SubjectId]    Script Date: 6/28/2018 5:21:48 PM ******/
CREATE NONCLUSTERED INDEX [IX_Question_SubjectId] ON [dbo].[Question]
(
	[SubjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Question_TestId]    Script Date: 6/28/2018 5:21:48 PM ******/
CREATE NONCLUSTERED INDEX [IX_Question_TestId] ON [dbo].[Question]
(
	[TestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Question_VisibilityId]    Script Date: 6/28/2018 5:21:48 PM ******/
CREATE NONCLUSTERED INDEX [IX_Question_VisibilityId] ON [dbo].[Question]
(
	[VisibilityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Test_MaxDifficultyLevelDifficultyLevelId]    Script Date: 6/28/2018 5:21:48 PM ******/
CREATE NONCLUSTERED INDEX [IX_Test_MaxDifficultyLevelDifficultyLevelId] ON [dbo].[Test]
(
	[MaxDifficultyLevelDifficultyLevelId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Test_SubjectId]    Script Date: 6/28/2018 5:21:48 PM ******/
CREATE NONCLUSTERED INDEX [IX_Test_SubjectId] ON [dbo].[Test]
(
	[SubjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Test_YearOfStudyId]    Script Date: 6/28/2018 5:21:48 PM ******/
CREATE NONCLUSTERED INDEX [IX_Test_YearOfStudyId] ON [dbo].[Test]
(
	[YearOfStudyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_TestArea_AreaId]    Script Date: 6/28/2018 5:21:48 PM ******/
CREATE NONCLUSTERED INDEX [IX_TestArea_AreaId] ON [dbo].[TestArea]
(
	[AreaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_TestArea_TestId]    Script Date: 6/28/2018 5:21:48 PM ******/
CREATE NONCLUSTERED INDEX [IX_TestArea_TestId] ON [dbo].[TestArea]
(
	[TestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_TestQuestion_QuestionId]    Script Date: 6/28/2018 5:21:48 PM ******/
CREATE NONCLUSTERED INDEX [IX_TestQuestion_QuestionId] ON [dbo].[TestQuestion]
(
	[QuestionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_TestQuestion_TestId]    Script Date: 6/28/2018 5:21:48 PM ******/
CREATE NONCLUSTERED INDEX [IX_TestQuestion_TestId] ON [dbo].[TestQuestion]
(
	[TestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Answer] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Answer] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[Question] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Question] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[Test] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Area]  WITH CHECK ADD  CONSTRAINT [FK_Area_Subject_SubjectId] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[Subject] ([SubjectId])
GO
ALTER TABLE [dbo].[Area] CHECK CONSTRAINT [FK_Area_Subject_SubjectId]
GO
ALTER TABLE [dbo].[Area]  WITH CHECK ADD  CONSTRAINT [FK_Area_Test_TestId] FOREIGN KEY([TestId])
REFERENCES [dbo].[Test] ([TestId])
GO
ALTER TABLE [dbo].[Area] CHECK CONSTRAINT [FK_Area_Test_TestId]
GO
ALTER TABLE [dbo].[Area]  WITH CHECK ADD  CONSTRAINT [FK_Area_YearOfStudy_YearOfStudyId] FOREIGN KEY([YearOfStudyId])
REFERENCES [dbo].[YearOfStudy] ([YearOfStudyId])
GO
ALTER TABLE [dbo].[Area] CHECK CONSTRAINT [FK_Area_YearOfStudy_YearOfStudyId]
GO
ALTER TABLE [dbo].[Question]  WITH CHECK ADD  CONSTRAINT [FK_Question_Answer_AnswerId] FOREIGN KEY([AnswerId])
REFERENCES [dbo].[Answer] ([AnswerId])
GO
ALTER TABLE [dbo].[Question] CHECK CONSTRAINT [FK_Question_Answer_AnswerId]
GO
ALTER TABLE [dbo].[Question]  WITH CHECK ADD  CONSTRAINT [FK_Question_Area_AreaId] FOREIGN KEY([AreaId])
REFERENCES [dbo].[Area] ([AreaId])
GO
ALTER TABLE [dbo].[Question] CHECK CONSTRAINT [FK_Question_Area_AreaId]
GO
ALTER TABLE [dbo].[Question]  WITH CHECK ADD  CONSTRAINT [FK_Question_DifficultyLevel_DifficultyLevelId] FOREIGN KEY([DifficultyLevelId])
REFERENCES [dbo].[DifficultyLevel] ([DifficultyLevelId])
GO
ALTER TABLE [dbo].[Question] CHECK CONSTRAINT [FK_Question_DifficultyLevel_DifficultyLevelId]
GO
ALTER TABLE [dbo].[Question]  WITH CHECK ADD  CONSTRAINT [FK_Question_NumberOfPoints_NumberOfPointsId] FOREIGN KEY([NumberOfPointsId])
REFERENCES [dbo].[NumberOfPoints] ([NumberOfPointsId])
GO
ALTER TABLE [dbo].[Question] CHECK CONSTRAINT [FK_Question_NumberOfPoints_NumberOfPointsId]
GO
ALTER TABLE [dbo].[Question]  WITH CHECK ADD  CONSTRAINT [FK_Question_QuestionType_QuestionTypeId] FOREIGN KEY([QuestionTypeId])
REFERENCES [dbo].[QuestionType] ([QuestionTypeId])
GO
ALTER TABLE [dbo].[Question] CHECK CONSTRAINT [FK_Question_QuestionType_QuestionTypeId]
GO
ALTER TABLE [dbo].[Question]  WITH CHECK ADD  CONSTRAINT [FK_Question_Status_StatusId] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Status] ([StatusId])
GO
ALTER TABLE [dbo].[Question] CHECK CONSTRAINT [FK_Question_Status_StatusId]
GO
ALTER TABLE [dbo].[Question]  WITH CHECK ADD  CONSTRAINT [FK_Question_Subject_SubjectId] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[Subject] ([SubjectId])
GO
ALTER TABLE [dbo].[Question] CHECK CONSTRAINT [FK_Question_Subject_SubjectId]
GO
ALTER TABLE [dbo].[Question]  WITH CHECK ADD  CONSTRAINT [FK_Question_Test_TestId] FOREIGN KEY([TestId])
REFERENCES [dbo].[Test] ([TestId])
GO
ALTER TABLE [dbo].[Question] CHECK CONSTRAINT [FK_Question_Test_TestId]
GO
ALTER TABLE [dbo].[Question]  WITH CHECK ADD  CONSTRAINT [FK_Question_Visibility_VisibilityId] FOREIGN KEY([VisibilityId])
REFERENCES [dbo].[Visibility] ([VisibilityId])
GO
ALTER TABLE [dbo].[Question] CHECK CONSTRAINT [FK_Question_Visibility_VisibilityId]
GO
ALTER TABLE [dbo].[Test]  WITH CHECK ADD  CONSTRAINT [FK_Test_DifficultyLevel_MaxDifficultyLevelDifficultyLevelId] FOREIGN KEY([MaxDifficultyLevelDifficultyLevelId])
REFERENCES [dbo].[DifficultyLevel] ([DifficultyLevelId])
GO
ALTER TABLE [dbo].[Test] CHECK CONSTRAINT [FK_Test_DifficultyLevel_MaxDifficultyLevelDifficultyLevelId]
GO
ALTER TABLE [dbo].[Test]  WITH CHECK ADD  CONSTRAINT [FK_Test_Subject_SubjectId] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[Subject] ([SubjectId])
GO
ALTER TABLE [dbo].[Test] CHECK CONSTRAINT [FK_Test_Subject_SubjectId]
GO
ALTER TABLE [dbo].[Test]  WITH CHECK ADD  CONSTRAINT [FK_Test_YearOfStudy_YearOfStudyId] FOREIGN KEY([YearOfStudyId])
REFERENCES [dbo].[YearOfStudy] ([YearOfStudyId])
GO
ALTER TABLE [dbo].[Test] CHECK CONSTRAINT [FK_Test_YearOfStudy_YearOfStudyId]
GO
ALTER TABLE [dbo].[TestArea]  WITH CHECK ADD  CONSTRAINT [FK_TestArea_Area_AreaId] FOREIGN KEY([AreaId])
REFERENCES [dbo].[Area] ([AreaId])
GO
ALTER TABLE [dbo].[TestArea] CHECK CONSTRAINT [FK_TestArea_Area_AreaId]
GO
ALTER TABLE [dbo].[TestArea]  WITH CHECK ADD  CONSTRAINT [FK_TestArea_Test_TestId] FOREIGN KEY([TestId])
REFERENCES [dbo].[Test] ([TestId])
GO
ALTER TABLE [dbo].[TestArea] CHECK CONSTRAINT [FK_TestArea_Test_TestId]
GO
ALTER TABLE [dbo].[TestQuestion]  WITH CHECK ADD  CONSTRAINT [FK_TestQuestion_Question_QuestionId] FOREIGN KEY([QuestionId])
REFERENCES [dbo].[Question] ([QuestionId])
GO
ALTER TABLE [dbo].[TestQuestion] CHECK CONSTRAINT [FK_TestQuestion_Question_QuestionId]
GO
ALTER TABLE [dbo].[TestQuestion]  WITH CHECK ADD  CONSTRAINT [FK_TestQuestion_Test_TestId] FOREIGN KEY([TestId])
REFERENCES [dbo].[Test] ([TestId])
GO
ALTER TABLE [dbo].[TestQuestion] CHECK CONSTRAINT [FK_TestQuestion_Test_TestId]
GO
USE [master]
GO
ALTER DATABASE [TestGeneratorDB] SET  READ_WRITE 
GO
