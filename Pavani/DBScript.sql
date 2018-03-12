USE [UsatHubDB]
GO

/****** Object:  Table [dbo].[Note]    Script Date: 3/11/2018 2:31:32 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Note](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TOMESSAGE] [varchar](100) NOT NULL,
	[FROMMESSAGE] [varchar](100) NULL,
	[HEADING] [varchar](100) NULL,
	[BODY] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

SET ANSI_PADDING OFF
GO


USE [UsatHubDB]
GO

/****** Object:  Table [dbo].[MODEM]    Script Date: 3/11/2018 12:23:50 PM ******/
	CREATE Proc [dbo].[usp_ins_note]
	(
	   @to  varchar(100),
	   @from  varchar(100),
	   @heading varchar(100),
	   @body  varchar(100)  
	)
	as
	Begin

	insert into Note (TOMESSAGE,FROMMESSAGE,HEADING,BODY) values(@to,@from,@heading,@body )
	End