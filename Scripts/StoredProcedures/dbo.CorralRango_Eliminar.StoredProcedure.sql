USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CorralRango_Eliminar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CorralRango_Eliminar]
GO
/****** Object:  StoredProcedure [dbo].[CorralRango_Eliminar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CorralRango_Eliminar]
	@OrganizacionID INT,
	@CorralID INT
AS
BEGIN	
SET NOCOUNT ON;	
		DELETE  
		  FROM CorralRango 
		 WHERE CorralID = @CorralID
		   AND OrganizacionID = @OrganizacionID
SET NOCOUNT OFF;
END

GO
