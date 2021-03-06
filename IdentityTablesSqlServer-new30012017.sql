USE [IdentityDapper]
GO
/****** Object:  StoredProcedure [dbo].[InsertEmployee]    Script Date: 1/30/2017 2:03:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jenit Vaghasiya
-- Create date: 27/1/2017
-- Description:	To Insert Employee
-- =============================================
CREATE PROCEDURE [dbo].[InsertEmployee]
	-- Add the parameters for the stored procedure here
	@EmpName nvarchar(50),
	@DeptName nvarchar(15),
	@Username nvarchar(50),
	@MobileNo numeric(12,0),
	@JoiningDate datetime,
	@RelevingDate datetime = null,
	@RESULT int output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Insert into Employee (EmpName,DeptName,Username,MobileNo,JoiningDate,CreateDate,IsActive,RelevingDate)
	Values(@EmpName,@DeptName,@Username,@MobileNo,@JoiningDate,GETDATE(),1,@RelevingDate)

	 Select @RESULT = IDENT_CURRENT('Employee')
END

GO
/****** Object:  StoredProcedure [dbo].[UpdateEmployee]    Script Date: 1/30/2017 2:03:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jenit Vaghasiya
-- Create date: 27/1/2017
-- Description:	To Insert Employee
-- =============================================
CREATE PROCEDURE [dbo].[UpdateEmployee]
	-- Add the parameters for the stored procedure here
	@EmployeeID int,
	@EmpName nvarchar(50),
	@DeptName nvarchar(15),
	@Username nvarchar(50),
	@MobileNo numeric(12,0),
	@JoiningDate datetime,
	@RelevingDate datetime = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

UPDATE [dbo].[Employee]
   SET [EmpName] = @EmpName 
      ,[DeptName] = @DeptName
      ,[Username] = @Username
      ,[MobileNo] = @MobileNo
      ,[JoiningDate] = @JoiningDate
      ,[RelevingDate] = @RelevingDate
      ,[UpdateDate] = Getdate()
 WHERE EmployeeID = @EmployeeID

END

GO
/****** Object:  Table [dbo].[Employee]    Script Date: 1/30/2017 2:03:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[EmployeeID] [int] IDENTITY(1,1) NOT NULL,
	[EmpName] [nvarchar](50) NULL,
	[DeptName] [nvarchar](15) NULL,
	[Username] [nvarchar](50) NULL,
	[MobileNo] [numeric](12, 0) NULL,
	[JoiningDate] [datetime] NULL,
	[RelevingDate] [datetime] NULL,
	[CreateDate] [datetime] NULL,
	[UpdateDate] [datetime] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[EmployeeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[IdentityLogin]    Script Date: 1/30/2017 2:03:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[IdentityLogin](
	[LoginProvider] [varchar](128) NOT NULL,
	[ProviderKey] [varchar](128) NOT NULL,
	[UserId] [int] NOT NULL,
	[Name] [varchar](256) NOT NULL,
 CONSTRAINT [PK_IdentityLogin] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[IdentityRole]    Script Date: 1/30/2017 2:03:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[IdentityRole](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_IdentityRole] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[IdentityUser]    Script Date: 1/30/2017 2:03:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[IdentityUser](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](256) NOT NULL,
	[Email] [varchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [varchar](max) NULL,
	[SecurityStamp] [varchar](38) NULL,
	[PhoneNumber] [varchar](50) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_IdentityUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[IdentityUserClaim]    Script Date: 1/30/2017 2:03:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[IdentityUserClaim](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ClaimType] [varchar](256) NOT NULL,
	[ClaimValue] [varchar](256) NULL,
 CONSTRAINT [PK_IdentityUserClaim] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[IdentityUserRole]    Script Date: 1/30/2017 2:03:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IdentityUserRole](
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_IdentityUserRole] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Products]    Script Date: 1/30/2017 2:03:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Quantity] [int] NULL,
	[Price] [float] NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SalaryReviewHistory]    Script Date: 1/30/2017 2:03:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalaryReviewHistory](
	[ReviewID] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeID] [int] NULL,
	[CurrentCTC] [decimal](18, 2) NULL,
	[EmployerComment] [nvarchar](200) NULL,
	[EmployeeComment] [nvarchar](200) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateDate] [datetime] NULL,
	[UpdatedBy] [datetime] NULL,
 CONSTRAINT [PK_SalaryReviewHistory] PRIMARY KEY CLUSTERED 
(
	[ReviewID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[IdentityLogin]  WITH CHECK ADD  CONSTRAINT [FK_dbo.IdentityLogin_dbo.IdentityUser_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[IdentityUser] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[IdentityLogin] CHECK CONSTRAINT [FK_dbo.IdentityLogin_dbo.IdentityUser_UserId]
GO
ALTER TABLE [dbo].[IdentityUserClaim]  WITH CHECK ADD  CONSTRAINT [FK_IdentityUserClaim_IdentityUser] FOREIGN KEY([UserId])
REFERENCES [dbo].[IdentityUser] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[IdentityUserClaim] CHECK CONSTRAINT [FK_IdentityUserClaim_IdentityUser]
GO
ALTER TABLE [dbo].[IdentityUserRole]  WITH CHECK ADD  CONSTRAINT [FK_IdentityUserRole_IdentityRole] FOREIGN KEY([RoleId])
REFERENCES [dbo].[IdentityRole] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[IdentityUserRole] CHECK CONSTRAINT [FK_IdentityUserRole_IdentityRole]
GO
ALTER TABLE [dbo].[IdentityUserRole]  WITH CHECK ADD  CONSTRAINT [FK_IdentityUserRole_IdentityUser] FOREIGN KEY([UserId])
REFERENCES [dbo].[IdentityUser] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[IdentityUserRole] CHECK CONSTRAINT [FK_IdentityUserRole_IdentityUser]
GO
