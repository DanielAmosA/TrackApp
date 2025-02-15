USE [AmosDanielDB]
GO
/****** Object:  Table [dbo].[Machines]    Script Date: 21/01/2025 13:10:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Machines](
	[MachineID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](256) NOT NULL,
	[MachineType] [nvarchar](50) NOT NULL,
	[CurrentStatusId] [int] NOT NULL,
	[Notes] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_Machines] PRIMARY KEY CLUSTERED 
(
	[MachineID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Statuses]    Script Date: 21/01/2025 13:10:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Statuses](
	[StatusID] [int] IDENTITY(1,1) NOT NULL,
	[StatusType] [nvarchar](50) NOT NULL,
	[StatusIcon] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Statuses] PRIMARY KEY CLUSTERED 
(
	[StatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 21/01/2025 13:10:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Machines]  WITH CHECK ADD  CONSTRAINT [FK_Machines_Statuses] FOREIGN KEY([CurrentStatusId])
REFERENCES [dbo].[Statuses] ([StatusID])
GO
ALTER TABLE [dbo].[Machines] CHECK CONSTRAINT [FK_Machines_Statuses]
GO
ALTER TABLE [dbo].[Machines]  WITH CHECK ADD  CONSTRAINT [CK_MachineType] CHECK  (([MachineType]='Human Operated' OR [MachineType]='Robot'))
GO
ALTER TABLE [dbo].[Machines] CHECK CONSTRAINT [CK_MachineType]
GO
/****** Object:  StoredProcedure [dbo].[Machines_Delete]    Script Date: 21/01/2025 13:10:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Daniel Amos Artzi
-- Create date: 20.01.24
-- Description:	Deleting Machines
-- =============================================
Create PROCEDURE [dbo].[Machines_Delete]

	@MachineID INT
	 
	AS
	Begin

		Begin try

			Begin Tran

				DELETE FROM Machines
				WHERE	MachineID = @MachineID

				-- Check if the addition was successful based on @@ROWCOUNT.
				If @@ROWCOUNT = 0
				Begin
					RAISERROR('No rows were deleted.', 16, 1);
				End

			Commit Tran
			Select 'Deletion successful ! :)' AS SuccessMessage
			
	  End try

	 Begin catch
			Rollback Tran
			Select ERROR_MESSAGE() AS ErrorMessage
	 End catch
  End
GO
/****** Object:  StoredProcedure [dbo].[Machines_GetMachinesByStatus]    Script Date: 21/01/2025 13:10:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Daniel Amos Artzi
-- Create date: 20.01.24
-- Description:	Get all machines by status.
-- =============================================
CREATE PROCEDURE [dbo].[Machines_GetMachinesByStatus]

	@StatusType NVARCHAR(50)
	
	AS

	BEGIN
		SELECT Name, Description, MachineType, Notes StatusType, StatusIcon
		From Machines
		Inner Join Statuses 
			On Machines.CurrentStatusId = Statuses.StatusID
		Where Statuses.StatusType = @StatusType
	END
GO
/****** Object:  StoredProcedure [dbo].[Machines_GetMachinesByStatusId]    Script Date: 21/01/2025 13:10:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Daniel Amos Artzi
-- Create date: 20.01.24
-- Description:	Get all machines by status id.
-- =============================================
CREATE PROCEDURE [dbo].[Machines_GetMachinesByStatusId]

	@CurrentStatusId int
	
	AS

	BEGIN
		SELECT Name, Description, MachineType, Notes StatusType, StatusIcon
		From Machines
		Inner Join Statuses 
			On Machines.CurrentStatusId = Statuses.StatusID
		Where CurrentStatusId = @CurrentStatusId
	END
GO
/****** Object:  StoredProcedure [dbo].[Machines_Insert]    Script Date: 21/01/2025 13:10:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Daniel Amos Artzi
-- Create date: 20.01.24
-- Description:	Creating New Machine
-- =============================================
CREATE PROCEDURE [dbo].[Machines_Insert]
    @Name NVARCHAR(100),
	@Description NVARCHAR(256),
	@MachineType NVARCHAR(50),
	@CurrentStatusId int,
	@Notes NVARCHAR(256)
	
	AS
	Begin

		Begin try

			Begin Tran
				INSERT INTO Machines
						   (Name, Description, MachineType, CurrentStatusId, Notes)
						Values
						   (@Name, @Description, @MachineType, @CurrentStatusId, @Notes)
			Commit Tran

			--We will retrieve the last values saved in the table to verify the process integrity
			Declare @MachineID INT = SCOPE_IDENTITY() -- Used to retrieve the last value of the IDENTITY column 
													   -- generated after adding a new row to the table.
			Select Name, Description, MachineType,StatusType
			From Machines 
			Inner join Statuses 
				On Machines.CurrentStatusId = Statuses.StatusID
			Where MachineID = @MachineID
		End try

		Begin catch
			Rollback Tran
			Select ERROR_MESSAGE() AS ErrorMessage
	    End catch
	End
GO
/****** Object:  StoredProcedure [dbo].[Machines_Update]    Script Date: 21/01/2025 13:10:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Daniel Amos Artzi
-- Create date: 20.01.24
-- Description:	Update the machine details.
-- =============================================
CREATE PROCEDURE [dbo].[Machines_Update]
    @MachineID INT,
	@New_Name NVARCHAR(100),
	@New_Description NVARCHAR(256),
	@New_MachineType NVARCHAR(50),
	@New_CurrentStatusId int,
	@New_Notes NVARCHAR(256)
	AS
	Begin

		Begin try

			Begin Tran
				UPDATE Machines
				SET Name = @New_Name,
					Description = @New_Description,
					MachineType = @New_MachineType,
					CurrentStatusId = @New_CurrentStatusId,
					Notes = @New_Notes
				WHERE MachineID = @MachineID

			Commit Tran	

			Select Name, Description, MachineType, StatusType, StatusIcon, Notes
			From Machines
			Inner join Statuses
				On Machines.CurrentStatusId = Statuses.StatusID
			Where MachineID = @MachineID

		End try

		Begin catch
			Rollback Tran
			Select ERROR_MESSAGE() AS ErrorMessage
	    End catch
	End
GO
/****** Object:  StoredProcedure [dbo].[Statuses_GetAllStatus]    Script Date: 21/01/2025 13:10:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Daniel Amos Artzi
-- Create date: 20.01.24
-- Description:	Get all status.
-- =============================================
CREATE PROCEDURE [dbo].[Statuses_GetAllStatus]

	AS

	BEGIN
		SELECT StatusID, StatusType, StatusIcon
		From Statuses
	END
GO
/****** Object:  StoredProcedure [dbo].[Users_GetUserByEmail]    Script Date: 21/01/2025 13:10:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Daniel Amos Artzi
-- Create date: 20.01.24
-- Description:	Get user by email.
-- =============================================
Create PROCEDURE [dbo].[Users_GetUserByEmail]

	@Email VARCHAR(250)
	
	AS

	BEGIN
		SELECT Name,Email,Password
		From Users
		Where Email = @Email
	END
GO
/****** Object:  StoredProcedure [dbo].[Users_GetUserByEmailAndPassword]    Script Date: 21/01/2025 13:10:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Daniel Amos Artzi
-- Create date: 20.01.24
-- Description:	Get user by email and password.
-- =============================================
CREATE PROCEDURE [dbo].[Users_GetUserByEmailAndPassword]

	@Email VARCHAR(250),
	@Password VARCHAR(MAX)
	
	AS

	BEGIN
		SELECT Name
		From Users
		Where Email = @Email
			and Password = @Password
	END
GO
/****** Object:  StoredProcedure [dbo].[Users_Insert]    Script Date: 21/01/2025 13:10:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Daniel Amos Artzi
-- Create date: 20.01.24
-- Description:	Creating New User
-- =============================================
CREATE PROCEDURE [dbo].[Users_Insert]
    @Name NVARCHAR(100),
	@Email NVARCHAR(100),
	@Password NVARCHAR(256)
	
	AS
	Begin

		Begin try

			Begin Tran
				INSERT INTO Users
						   (Name, Password, Email)
						Values
						   (@Name, @Password, @Email)
			Commit Tran

			--We will retrieve the last values saved in the table to verify the process integrity
			Declare @UserID INT = SCOPE_IDENTITY() -- Used to retrieve the last value of the IDENTITY column 
													   -- generated after adding a new row to the table.
			Select Name, Email
			From Users
			Where UserID = @UserID
			And UserID = @UserID
		End try

		Begin catch
			Rollback Tran
			Select ERROR_MESSAGE() AS ErrorMessage
	    End catch
	End
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Link to the machine statuses.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Machines', @level2type=N'CONSTRAINT',@level2name=N'FK_Machines_Statuses'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Checking the type of machine.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Machines', @level2type=N'CONSTRAINT',@level2name=N'CK_MachineType'
GO
