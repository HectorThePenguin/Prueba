USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Organizacion_ObtenerPorEmbarqueTipoOrigen]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Organizacion_ObtenerPorEmbarqueTipoOrigen]
GO
/****** Object:  StoredProcedure [dbo].[Organizacion_ObtenerPorEmbarqueTipoOrigen]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Organizacion_ObtenerPorEmbarqueTipoOrigen]
	@TipoOrganizacionID INT,
	@OrganizacionID INT,
	@EmbarqueID INT
/* 
=============================================
-- Author: Gilberto Carranza
-- Create date: 26-11-2013
-- Description:	Otiene un listado de organizaciones por Folio de Entrada
-- Organizacion_ObtenerPorEmbarqueTipoOrigen 1, 0, 1
=============================================
*/
AS
BEGIN
	SET NOCOUNT ON;
	SELECT O.OrganizacionID,
			O.Descripcion,
			O.Activo				
	FROM Organizacion O
	INNER JOIN EmbarqueDetalle PED
		ON (O.OrganizacionID = PED.OrganizacionOrigenID
			AND PED.EmbarqueID = @EmbarqueID)
	WHERE O.Activo = 1
		AND @TipoOrganizacionID IN (O.TipoOrganizacionID, 0)
		AND @OrganizacionID IN (O.OrganizacionID, 0)		  
	GROUP BY O.OrganizacionID,
			O.Descripcion,
			O.Activo
	SET NOCOUNT OFF;
END

GO
