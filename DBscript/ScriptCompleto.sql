USE [master]
GO
/****** Object:  Database [SIST_RECONOCIMIENTO]    Script Date: 25/6/2023 19:41:54 ******/
CREATE DATABASE [SIST_RECONOCIMIENTO]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SIST_RECONOCIMIENTO', FILENAME = N'E:\SQLserver\Features\MSSQL16.JESUSSQLSERVER\MSSQL\DATA\SIST_RECONOCIMIENTO.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SIST_RECONOCIMIENTO_log', FILENAME = N'E:\SQLserver\Features\MSSQL16.JESUSSQLSERVER\MSSQL\DATA\SIST_RECONOCIMIENTO_log.ldf' , SIZE = 139264KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [SIST_RECONOCIMIENTO] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SIST_RECONOCIMIENTO].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SIST_RECONOCIMIENTO] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SIST_RECONOCIMIENTO] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SIST_RECONOCIMIENTO] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SIST_RECONOCIMIENTO] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SIST_RECONOCIMIENTO] SET ARITHABORT OFF 
GO
ALTER DATABASE [SIST_RECONOCIMIENTO] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SIST_RECONOCIMIENTO] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SIST_RECONOCIMIENTO] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SIST_RECONOCIMIENTO] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SIST_RECONOCIMIENTO] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SIST_RECONOCIMIENTO] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SIST_RECONOCIMIENTO] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SIST_RECONOCIMIENTO] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SIST_RECONOCIMIENTO] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SIST_RECONOCIMIENTO] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SIST_RECONOCIMIENTO] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SIST_RECONOCIMIENTO] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SIST_RECONOCIMIENTO] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SIST_RECONOCIMIENTO] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SIST_RECONOCIMIENTO] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SIST_RECONOCIMIENTO] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [SIST_RECONOCIMIENTO] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SIST_RECONOCIMIENTO] SET RECOVERY FULL 
GO
ALTER DATABASE [SIST_RECONOCIMIENTO] SET  MULTI_USER 
GO
ALTER DATABASE [SIST_RECONOCIMIENTO] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SIST_RECONOCIMIENTO] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SIST_RECONOCIMIENTO] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SIST_RECONOCIMIENTO] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SIST_RECONOCIMIENTO] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SIST_RECONOCIMIENTO] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'SIST_RECONOCIMIENTO', N'ON'
GO
ALTER DATABASE [SIST_RECONOCIMIENTO] SET QUERY_STORE = ON
GO
ALTER DATABASE [SIST_RECONOCIMIENTO] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [SIST_RECONOCIMIENTO]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 25/6/2023 19:41:54 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 25/6/2023 19:41:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 25/6/2023 19:41:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 25/6/2023 19:41:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 25/6/2023 19:41:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 25/6/2023 19:41:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 25/6/2023 19:41:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[IsNew] [bit] NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 25/6/2023 19:41:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CSAT]    Script Date: 25/6/2023 19:41:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CSAT](
	[ID_Survey] [int] IDENTITY(1,1) NOT NULL,
	[Score] [int] NULL,
	[Comments] [varchar](500) NULL,
	[Email_Engineer] [varchar](50) NULL,
	[DTC] [int] NULL,
	[Engineer_ID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Survey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Engineers]    Script Date: 25/6/2023 19:41:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Engineers](
	[ID_Engineer] [int] IDENTITY(1,1) NOT NULL,
	[Name_Engineer] [varchar](50) NULL,
	[LastName_Engineer] [varchar](50) NULL,
	[Position] [int] NULL,
	[Points] [int] NULL,
	[Picture] [varbinary](max) NULL,
	[ID_Account] [nvarchar](450) NULL,
	[ID_Manager] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Engineer] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Log_PasswordUpdate]    Script Date: 25/6/2023 19:41:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Log_PasswordUpdate](
	[ID_Change] [int] IDENTITY(1,1) NOT NULL,
	[Reason] [varchar](50) NULL,
	[ID_Engineer] [int] NULL,
	[Change_Date] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Change] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Manager]    Script Date: 25/6/2023 19:41:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Manager](
	[ID_Manager] [int] IDENTITY(1,1) NOT NULL,
	[Name_Manager] [varchar](50) NULL,
	[LastName_Manager] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Manager] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Phrases]    Script Date: 25/6/2023 19:41:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Phrases](
	[Phrases_ID] [int] IDENTITY(1,1) NOT NULL,
	[Phrase] [varchar](500) NULL,
	[Engineer_ID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Phrases_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Positions]    Script Date: 25/6/2023 19:41:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Positions](
	[ID_Position] [int] IDENTITY(1,1) NOT NULL,
	[Position_Name] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Position] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Purchases]    Script Date: 25/6/2023 19:41:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Purchases](
	[ID_Purchase] [int] IDENTITY(1,1) NOT NULL,
	[Engineer_ID] [int] NULL,
	[Reward_ID] [int] NULL,
	[Reward_Price] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Purchase] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Recognitions]    Script Date: 25/6/2023 19:41:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Recognitions](
	[ID_Recognition] [int] IDENTITY(1,1) NOT NULL,
	[Petitioner_Eng] [int] NULL,
	[Recognized_Eng] [int] NULL,
	[Recognition_State] [varchar](50) NULL,
	[Case_Number] [varchar](30) NULL,
	[Comment] [varchar](500) NULL,
	[Evaluator_Admin] [int] NULL,
	[Recognition_Date] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Recognition] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rewards]    Script Date: 25/6/2023 19:41:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rewards](
	[ID_Reward] [int] IDENTITY(1,1) NOT NULL,
	[Reward_Name] [varchar](100) NULL,
	[Reward_Description] [varchar](1000) NULL,
	[Price] [int] NULL,
	[Picture] [varbinary](max) NULL,
	[IsEnabled] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Reward] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 25/6/2023 19:41:54 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 25/6/2023 19:41:54 ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 25/6/2023 19:41:54 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 25/6/2023 19:41:54 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 25/6/2023 19:41:54 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 25/6/2023 19:41:54 ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 25/6/2023 19:41:54 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[CSAT]  WITH CHECK ADD FOREIGN KEY([Engineer_ID])
REFERENCES [dbo].[Engineers] ([ID_Engineer])
GO
ALTER TABLE [dbo].[Engineers]  WITH CHECK ADD FOREIGN KEY([ID_Account])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Engineers]  WITH CHECK ADD FOREIGN KEY([Position])
REFERENCES [dbo].[Positions] ([ID_Position])
GO
ALTER TABLE [dbo].[Engineers]  WITH CHECK ADD  CONSTRAINT [FK_Manager] FOREIGN KEY([ID_Manager])
REFERENCES [dbo].[Manager] ([ID_Manager])
GO
ALTER TABLE [dbo].[Engineers] CHECK CONSTRAINT [FK_Manager]
GO
ALTER TABLE [dbo].[Log_PasswordUpdate]  WITH CHECK ADD FOREIGN KEY([ID_Engineer])
REFERENCES [dbo].[Engineers] ([ID_Engineer])
GO
ALTER TABLE [dbo].[Phrases]  WITH CHECK ADD FOREIGN KEY([Engineer_ID])
REFERENCES [dbo].[Engineers] ([ID_Engineer])
GO
ALTER TABLE [dbo].[Purchases]  WITH CHECK ADD FOREIGN KEY([Engineer_ID])
REFERENCES [dbo].[Engineers] ([ID_Engineer])
GO
ALTER TABLE [dbo].[Purchases]  WITH CHECK ADD FOREIGN KEY([Reward_ID])
REFERENCES [dbo].[Rewards] ([ID_Reward])
GO
ALTER TABLE [dbo].[Recognitions]  WITH CHECK ADD FOREIGN KEY([Evaluator_Admin])
REFERENCES [dbo].[Engineers] ([ID_Engineer])
GO
ALTER TABLE [dbo].[Recognitions]  WITH CHECK ADD FOREIGN KEY([Petitioner_Eng])
REFERENCES [dbo].[Engineers] ([ID_Engineer])
GO
ALTER TABLE [dbo].[Recognitions]  WITH CHECK ADD FOREIGN KEY([Recognized_Eng])
REFERENCES [dbo].[Engineers] ([ID_Engineer])
GO
/****** Object:  StoredProcedure [dbo].[ChangeIsNew]    Script Date: 25/6/2023 19:41:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChangeIsNew]
    @email nvarchar(256),
    @newValueIsNew bit,
    @newValueLockout bit
AS
BEGIN
    UPDATE AspNetUsers
    SET IsNew = @newValueIsNew
    WHERE Email = @email;
	UPDATE AspNetUsers
	SET LockoutEnabled = @newValueLockout
	WHERE Email = @email;
END
GO
/****** Object:  StoredProcedure [dbo].[ChangeRewardState]    Script Date: 25/6/2023 19:41:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChangeRewardState]
    @ID_Reward int,
    @newValue bit
AS
BEGIN
    UPDATE Rewards
    SET IsEnabled = @newValue
    WHERE ID_Reward = @ID_Reward;
END
GO
/****** Object:  StoredProcedure [dbo].[GetEngineerEmail]    Script Date: 25/6/2023 19:41:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetEngineerEmail]
    @ID_Engineer int
AS
BEGIN
	SELECT u.Email, CONCAT(e.Name_Engineer,' ',e.LastName_Engineer) as FullName
	FROM AspNetUsers u, Engineers e
	WHERE e.ID_Engineer = @ID_Engineer AND e.ID_Account = u.Id;
END
GO
/****** Object:  StoredProcedure [dbo].[GetLockoutEnabledAndIsNew]    Script Date: 25/6/2023 19:41:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--Stored Procedures
CREATE PROCEDURE [dbo].[GetLockoutEnabledAndIsNew]
    @Email VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT LockoutEnabled, IsNew
    FROM AspNetUsers
    WHERE Email = @Email;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetRewardState]    Script Date: 25/6/2023 19:41:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetRewardState]
	@ID_Reward int
AS
BEGIN
    SET NOCOUNT ON;

    SELECT ID_Reward, IsEnabled
    FROM Rewards
    WHERE ID_Reward = @ID_Reward;
END;
GO
/****** Object:  StoredProcedure [dbo].[SelectEngineers]    Script Date: 25/6/2023 19:41:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SelectEngineers]
AS
BEGIN

	SELECT U.UserName, U.Email, MG.Name_Manager AS Manager, R.Name AS RoleName, P.Position_Name AS Posicion
	FROM AspNetUsers U
	JOIN AspNetUserRoles UR ON U.Id = UR.UserId
	JOIN AspNetRoles R ON UR.RoleId = R.Id
	JOIN Engineers E ON U.Id = E.ID_Account
	JOIN Positions P ON E.Position = P.ID_Position
	JOIN Manager MG ON E.ID_Manager = MG.ID_Manager;

END
GO
/****** Object:  StoredProcedure [dbo].[SelectManagerforRecognition]    Script Date: 25/6/2023 19:41:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SelectManagerforRecognition]
    @ID_Engineer int,
	@ID_Recognition int
AS
BEGIN
	SELECT DISTINCT m.Email, m.Name_Manager, u.Email as EmailE, e.Name_Engineer, CONVERT(VARCHAR(255),r.ID_Recognition) AS ID_Recognition, r.Case_Number, r.Comment
	FROM Manager m, Engineers e, AspNetUsers u, Recognitions r
	WHERE m.ID_Manager = e.ID_Manager AND e.ID_Engineer = @ID_Engineer AND u.Id = e.ID_Account AND r.ID_Recognition = @ID_Recognition;
END
GO
/****** Object:  StoredProcedure [dbo].[SelectManagers]    Script Date: 25/6/2023 19:41:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SelectManagers]
AS
BEGIN
	SELECT * FROM MANAGER;
END
GO
/****** Object:  StoredProcedure [dbo].[SelectMyManager]    Script Date: 25/6/2023 19:41:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SelectMyManager]
    @ID_Engineer int,
	@Reward_ID int,
	@ID_Purchase int
AS
BEGIN
	SELECT DISTINCT m.Email, m.Name_Manager, u.Email as EmailE, e.Name_Engineer, CONVERT(VARCHAR(255),e.Points) AS Points, r.Reward_Name, CONVERT(VARCHAR(255),r.Price) AS Price
	FROM Manager m, Engineers e, AspNetUsers u, Rewards r, Purchases p
	WHERE m.ID_Manager = e.ID_Manager AND e.ID_Engineer = @ID_Engineer AND u.Id = e.ID_Account AND r.ID_Reward = @Reward_ID AND p.ID_Purchase = @ID_Purchase;
END
GO
/****** Object:  StoredProcedure [dbo].[SelectPositions]    Script Date: 25/6/2023 19:41:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SelectPositions]
AS
BEGIN
	SELECT * FROM Positions;
END
GO
/****** Object:  StoredProcedure [dbo].[SelectRoles]    Script Date: 25/6/2023 19:41:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SelectRoles]
AS
BEGIN
	SELECT * FROM AspNetRoles;
END
GO
/****** Object:  StoredProcedure [dbo].[ShowIdEngineer]    Script Date: 25/6/2023 19:41:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ShowIdEngineer]
    @email nvarchar(256)
AS
BEGIN
	SELECT e.ID_Engineer
	FROM Engineers e, AspNetUsers u
	WHERE u.Email = @email AND e.ID_Account = u.Id;
END
GO
/****** Object:  StoredProcedure [dbo].[ShowPoints]    Script Date: 25/6/2023 19:41:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ShowPoints]
    @email nvarchar(256)
AS
BEGIN
	SELECT e.Points
	FROM Engineers e, AspNetUsers u
	WHERE u.Email = @email AND e.ID_Account = u.Id;
END
GO
/****** Object:  StoredProcedure [dbo].[UpdatePoints]    Script Date: 25/6/2023 19:41:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdatePoints]
    @Engineer_ID int,
    @newPoints int
AS
BEGIN
    UPDATE Engineers
    SET Points = @newPoints
    WHERE ID_Engineer = @Engineer_ID;
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateRecognitionState]    Script Date: 25/6/2023 19:41:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateRecognitionState]
    @ID_Recognition int,
    @newState varchar(500)
AS
BEGIN
    UPDATE Recognitions
    SET Recognition_State = @newState
    WHERE ID_Recognition = @ID_Recognition;
END
GO
USE [master]
GO
ALTER DATABASE [SIST_RECONOCIMIENTO] SET  READ_WRITE 
GO
