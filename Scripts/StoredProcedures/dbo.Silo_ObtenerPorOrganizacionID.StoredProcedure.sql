USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Silo_ObtenerPorOrganizacionID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Silo_ObtenerPorOrganizacionID]
GO
/****** Object:  StoredProcedure [dbo].[Silo_ObtenerPorOrganizacionID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Emir Lezama
-- Create date: 07/11/2014
-- Description: 
-- SpName     : exec Silo_ObtenerPorOrganizacionID
--======================================================
CREATE PROCEDURE [dbo].[Silo_ObtenerPorOrganizacionID]
@OrganizacionID INT
,@Activo BIT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT Descripcion 
	FROM Silo 
	WHERE OrganizacionID = @OrganizacionID
	AND Activo = @Activo
	SET NOCOUNT OFF;
END

GO
