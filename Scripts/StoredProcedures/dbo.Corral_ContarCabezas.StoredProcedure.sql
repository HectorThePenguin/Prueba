USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ContarCabezas]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Corral_ContarCabezas]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ContarCabezas]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Corral_ContarCabezas]
	@CorralID INT,
	@OrganizacionID INT,
	@Estatus INT
AS
BEGIN	
SET NOCOUNT ON;	
	SELECT COUNT(*) [Total]
	FROM Animal A (NOLOCK)
	INNER JOIN AnimalMovimiento AM(NOLOCK) ON A.AnimalID = AM.AnimalID
	AND AM.Activo= @Estatus
	AND AM.CorralID = @CorralID
	AND AM.OrganizacionID = @OrganizacionID
SET NOCOUNT OFF;	
END

GO
