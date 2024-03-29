USE [Images]
GO
/****** Object:  Table [dbo].[ImageItems]    Script Date: 07/23/2015 12:22:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ImageItems](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Url] [nvarchar](255) NOT NULL,
	[FileName] [nvarchar](255) NOT NULL,
	[ImageData] [image] NOT NULL,
 CONSTRAINT [PK_ImageItems] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[InsertImageItem]    Script Date: 07/23/2015 12:22:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertImageItem] 
	@FileName nvarchar(512),
	@Url nvarchar(256),
	@ImageData Image,
	@ID int OUTPUT

AS
INSERT INTO ImageItems (FileName, Url, ImageData)
	VALUES (@FileName, @Url, @ImageData);
SET @ID= @@IDENTITY
GO
/****** Object:  StoredProcedure [dbo].[GetImageItem]    Script Date: 07/23/2015 12:22:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetImageItem] 
@ID int
AS
SELECT ID, [FileName], Url, ImageData FROM ImageItems WHERE ID = @ID
GO
/****** Object:  StoredProcedure [dbo].[GetAllImageItems]    Script Date: 07/23/2015 12:22:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllImageItems] 
AS
SELECT ID, [FileName], Url, ImageData FROM ImageItems
GO
/****** Object:  StoredProcedure [dbo].[DeleteImageItem]    Script Date: 07/23/2015 12:22:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteImageItem]
	@ID int
AS
	DELETE FROM ImageItems WHERE ID= @ID
GO
/****** Object:  StoredProcedure [dbo].[DeleteAllImageItems]    Script Date: 07/23/2015 12:22:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteAllImageItems] 
	
AS
BEGIN
truncate table [dbo].[ImageItems]
END
GO
/****** Object:  StoredProcedure [dbo].[CountImageItems]    Script Date: 07/23/2015 12:22:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CountImageItems]
AS
SELECT COUNT(ID) FROM ImageItems
GO
