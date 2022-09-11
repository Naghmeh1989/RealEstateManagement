USE [master]
GO
/****** Object:  Database [FirstProject]    Script Date: 11/09/2022 12:23:49 PM ******/
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
/****** Object:  Table [dbo].[Agency]    Script Date: 11/09/2022 12:23:49 PM ******/
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
/****** Object:  Table [dbo].[Building]    Script Date: 11/09/2022 12:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Building](
	[NumberOfFlats] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Address] [nvarchar](200) NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Building] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contract]    Script Date: 11/09/2022 12:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contract](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FlatId] [int] NOT NULL,
	[TenantId] [int] NOT NULL,
	[AgencyId] [int] NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[RentPaymentDay] [int] NOT NULL,
	[RentAmount] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_Contract] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Flat]    Script Date: 11/09/2022 12:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Flat](
	[Number] [int] NOT NULL,
	[Floor] [int] NOT NULL,
	[Bedroom] [int] NULL,
	[Parking] [bit] NULL,
	[PetAllowed] [bit] NULL,
	[BillsIncluded] [bit] NULL,
	[Furnished] [bit] NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BuildingId] [int] NOT NULL,
 CONSTRAINT [PK_Flat] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RentPayment]    Script Date: 11/09/2022 12:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RentPayment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ContractId] [int] NOT NULL,
	[IsPaid] [bit] NOT NULL,
	[PaymentDate] [datetime] NULL,
 CONSTRAINT [PK_RentPayment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tenant]    Script Date: 11/09/2022 12:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tenant](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Tenant] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 11/09/2022 12:23:50 PM ******/
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
ALTER TABLE [dbo].[Contract]  WITH CHECK ADD  CONSTRAINT [FK_Contract_Agency] FOREIGN KEY([AgencyId])
REFERENCES [dbo].[Agency] ([Id])
GO
ALTER TABLE [dbo].[Contract] CHECK CONSTRAINT [FK_Contract_Agency]
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
USE [master]
GO
ALTER DATABASE [FirstProject] SET  READ_WRITE 
GO
