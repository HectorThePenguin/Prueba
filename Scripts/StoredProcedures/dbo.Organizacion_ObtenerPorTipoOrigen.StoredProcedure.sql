USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Organizacion_ObtenerPorTipoOrigen]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Organizacion_ObtenerPorTipoOrigen]
GO
/****** Object:  StoredProcedure [dbo].[Organizacion_ObtenerPorTipoOrigen]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Organizacion_ObtenerPorTipoOrigen]
	@TipoOrganizacionID INT,
	@OrganizacionID INT,
	@EmbarqueID INT
/* 
=============================================
-- Author: Gilberto Carranza
-- Create date: 26-11-2013
-- Description:	Otiene un listado de organizaciones por Folio de Entrada
-- [Organizacion_ObtenerPorTipoOrigen] 3, 7, 0
=============================================
*/
AS
BEGIN
	SET NOCOUNT ON;
	SELECT O.OrganizacionID,
			O.Descripcion,
			too.TipoOrganizacionID,
			too.Descripcion AS TipoOrganizacion,
			O.Activo				
	FROM Organizacion O
	INNER JOIN TipoOrganizacion too on O.TipoOrganizacionID = too.TipoOrganizacionID
	LEFT JOIN EmbarqueDetalle PED
		ON (O.OrganizacionID = PED.OrganizacionOrigenID
			AND PED.EmbarqueID = @EmbarqueID)
	WHERE O.Activo = 1
	AND @TipoOrganizacionID in (O.TipoOrganizacionID,0)		
		AND @OrganizacionID = O.OrganizacionID	  
	GROUP BY O.OrganizacionID,
			O.Descripcion,
			O.Activo,
			too.TipoOrganizacionID,
			too.Descripcion
	SET NOCOUNT OFF;
END

GO
