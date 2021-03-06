USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerCorralActivosPorCodigo]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Corral_ObtenerCorralActivosPorCodigo]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerCorralActivosPorCodigo]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author:		Ramses Santos
-- Create date: 05/04/2014
-- Description:	Obtiene los corrales que esten activos y que tengan lotes activos.
-- [Corral_ObtenerCorralActivosPorCodigo] 1
--======================================================
CREATE PROCEDURE [dbo].[Corral_ObtenerCorralActivosPorCodigo]
	@OrganizacionID INT,
	@Codigo VARCHAR(10)
AS
BEGIN
	SELECT 	    
		C.CorralID,
		C.Codigo,
		C.TipoCorralID,
		C.Activo,
		TC.GrupoCorralID
	FROM Corral (NOLOCK) AS C
	INNER JOIN TipoCorral (NOLOCK) AS TC
	ON (C.TipoCorralID = TC.TipoCorralID)
	WHERE Codigo = @Codigo AND C.OrganizacionID = @OrganizacionID
	AND C.Activo = 1
	SET NOCOUNT OFF;
END

GO
