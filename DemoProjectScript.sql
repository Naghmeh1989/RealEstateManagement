USE [master]
GO
/****** Object:  Database [FirstProject]    Script Date: 10/08/2022 11:09:04 AM ******/
CREATE DATABASE [FirstProject]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'FirstProject', FILENAME = N'C:\Users\44757\FirstProject.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'FirstProject_log', FILENAME = N'C:\Users\44757\FirstProject_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [FirstProject] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FirstProject].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FirstProject] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FirstProject] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FirstProject] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FirstProject] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FirstProject] SET ARITHABORT OFF 
GO
ALTER DATABASE [FirstProject] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [FirstProject] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FirstProject] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FirstProject] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FirstProject] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [FirstProject] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FirstProject] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FirstProject] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FirstProject] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FirstProject] SET  DISABLE_BROKER 
GO
ALTER DATABASE [FirstProject] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FirstProject] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FirstProject] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FirstProject] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [FirstProject] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FirstProject] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [FirstProject] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FirstProject] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [FirstProject] SET  MULTI_USER 
GO
ALTER DATABASE [FirstProject] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [FirstProject] SET DB_CHAINING OFF 
GO
ALTER DATABASE [FirstProject] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [FirstProject] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [FirstProject] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [FirstProject] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [FirstProject] SET QUERY_STORE = OFF
GO
USE [FirstProject]
GO
/****** Object:  Table [dbo].[Agency]    Script Date: 10/08/2022 11:09:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Agency](
	[UserId] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Agency] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Building]    Script Date: 10/08/2022 11:09:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Building](
	[NumberOfFlats] [int] NULL,
	[Name] [varchar](50) NOT NULL,
	[Address] [nvarchar](200) NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Building] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contract]    Script Date: 10/08/2022 11:09:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contract](
	[FlatId] [int] NOT NULL,
	[TenantId] [int] NOT NULL,
	[AgencyId] [int] NOT NULL,
	[RentPaymentDay] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[StartDate] [int] NOT NULL,
	[RentAmount] [decimal](18, 0) NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Contract] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Flat]    Script Date: 10/08/2022 11:09:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Flat](
	[BuildingId] [int] NOT NULL,
	[Number] [int] NOT NULL,
	[Floor] [int] NOT NULL,
	[Bedroom] [int] NULL,
	[Parking] [bit] NULL,
	[PetAllowed] [bit] NULL,
	[BillsIncluded] [bit] NULL,
	[Furnished] [bit] NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Flat] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RentPayment]    Script Date: 10/08/2022 11:09:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RentPayment](
	[ContractId] [int] NOT NULL,
	[IsPaid] [bit] NOT NULL,
	[PayDate] [datetime] NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_RentPayment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tenant]    Script Date: 10/08/2022 11:09:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tenant](
	[UserId] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Tenant] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 10/08/2022 11:09:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[IsActive] [bit] NULL,
	[LastLoginDate] [datetime] NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Agency] ON 

INSERT [dbo].[Agency] ([UserId], [Name], [Id]) VALUES (1, N'Iran', 2)
SET IDENTITY_INSERT [dbo].[Agency] OFF
GO
SET IDENTITY_INSERT [dbo].[Building] ON 

INSERT [dbo].[Building] ([NumberOfFlats], [Name], [Address], [Id]) VALUES (10, N'WilburnBasin', N'Salford', 1)
INSERT [dbo].[Building] ([NumberOfFlats], [Name], [Address], [Id]) VALUES (5, N'ncmhgmvv', N'xxdngmj', 2)
INSERT [dbo].[Building] ([NumberOfFlats], [Name], [Address], [Id]) VALUES (25, N'B,KHHK.KK', N'DMF,HLM', 1002)
INSERT [dbo].[Building] ([NumberOfFlats], [Name], [Address], [Id]) VALUES (89, N'jhmk', N'fmj,g.kh', 1003)
SET IDENTITY_INSERT [dbo].[Building] OFF
GO
SET IDENTITY_INSERT [dbo].[Flat] ON 

INSERT [dbo].[Flat] ([BuildingId], [Number], [Floor], [Bedroom], [Parking], [PetAllowed], [BillsIncluded], [Furnished], [Id]) VALUES (1, 5, 3, 2, 0, 0, 0, 1, 1)
INSERT [dbo].[Flat] ([BuildingId], [Number], [Floor], [Bedroom], [Parking], [PetAllowed], [BillsIncluded], [Furnished], [Id]) VALUES (1, 6, 3, 2, 0, 0, 0, 1, 2)
SET IDENTITY_INSERT [dbo].[Flat] OFF
GO
SET IDENTITY_INSERT [dbo].[Tenant] ON 

INSERT [dbo].[Tenant] ([UserId], [Name], [LastName], [Id]) VALUES (2, N'GGJK', N'JHGHK', 1)
INSERT [dbo].[Tenant] ([UserId], [Name], [LastName], [Id]) VALUES (3, N'fhnfm', N'fuk,g,y,', 2)
INSERT [dbo].[Tenant] ([UserId], [Name], [LastName], [Id]) VALUES (4, N'HFUKG', N'FTJTFK', 3)
SET IDENTITY_INSERT [dbo].[Tenant] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([IsActive], [LastLoginDate], [Username], [Password], [Id]) VALUES (NULL, NULL, N'Naghmeh89', N'Iran555555', 1)
INSERT [dbo].[User] ([IsActive], [LastLoginDate], [Username], [Password], [Id]) VALUES (NULL, NULL, N'1220BHNJ', N'1232', 2)
INSERT [dbo].[User] ([IsActive], [LastLoginDate], [Username], [Password], [Id]) VALUES (NULL, NULL, N'hjjbj,,k', N'3133', 3)
INSERT [dbo].[User] ([IsActive], [LastLoginDate], [Username], [Password], [Id]) VALUES (NULL, NULL, N'JKHKL', N'3156', 4)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
ALTER TABLE [dbo].[Agency]  WITH CHECK ADD  CONSTRAINT [FK_Agency_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Agency] CHECK CONSTRAINT [FK_Agency_User]
GO
ALTER TABLE [dbo].[Contract]  WITH CHECK ADD  CONSTRAINT [FK_Contract_Flat] FOREIGN KEY([FlatId])
REFERENCES [dbo].[Flat] ([Id])
GO
ALTER TABLE [dbo].[Contract] CHECK CONSTRAINT [FK_Contract_Flat]
GO
ALTER TABLE [dbo].[Contract]  WITH CHECK ADD  CONSTRAINT [FK_Contract_Tenant] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenant] ([Id])
GO
ALTER TABLE [dbo].[Contract] CHECK CONSTRAINT [FK_Contract_Tenant]
GO
ALTER TABLE [dbo].[Flat]  WITH CHECK ADD  CONSTRAINT [FK_Flat_Building] FOREIGN KEY([BuildingId])
REFERENCES [dbo].[Building] ([Id])
GO
ALTER TABLE [dbo].[Flat] CHECK CONSTRAINT [FK_Flat_Building]
GO
ALTER TABLE [dbo].[RentPayment]  WITH CHECK ADD  CONSTRAINT [FK_RentPayment_Contract] FOREIGN KEY([ContractId])
REFERENCES [dbo].[Contract] ([Id])
GO
ALTER TABLE [dbo].[RentPayment] CHECK CONSTRAINT [FK_RentPayment_Contract]
GO
ALTER TABLE [dbo].[Tenant]  WITH CHECK ADD  CONSTRAINT [FK_Tenant_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Tenant] CHECK CONSTRAINT [FK_Tenant_User]
GO
USE [master]
GO
ALTER DATABASE [FirstProject] SET  READ_WRITE 
GO
