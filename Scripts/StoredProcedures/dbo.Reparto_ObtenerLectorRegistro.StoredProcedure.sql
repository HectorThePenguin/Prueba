USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ObtenerLectorRegistro]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Reparto_ObtenerLectorRegistro]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ObtenerLectorRegistro]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author:		Roque.Solis
-- Create date: 2014-03-26
-- Origen: APInterfaces
-- Description:	Obtiene un lector registro
-- EXEC Reparto_ObtenerLectorRegistro 1, 4, 1, '1900-01-01'
--=============================================
CREATE PROCEDURE [dbo].[Reparto_ObtenerLectorRegistro]
	@LoteID INT,
	@OrganizacionID INT,
	@Activo INT,
	@Fecha DATETIME
AS
BEGIN
	SET NOCOUNT ON;
	declare @FechaLector DATE	
	set @FechaLector = @Fecha
	SELECT 
		L.LectorRegistroID,
        L.OrganizacionID,
		L.Seccion,
        L.LoteID,
        L.Fecha,
        L.CambioFormula,
        L.Cabezas,
        L.EstadoComederoID,
        CAST(ROUND(L.CantidadOriginal * L.Cabezas, 0) AS decimal) AS CantidadOriginal,
        CAST(ROUND(L.CantidadPedido * L.Cabezas, 0) AS decimal) AS CantidadPedido
	FROM LectorRegistro L
  INNER JOIN LectorRegistroDetalle LRD ON L.LectorRegistroID = LRD.LectorRegistroID
	WHERE L.LoteID = @LoteID
	AND L.OrganizacionID = @OrganizacionID
	AND L.Activo = @Activo
	AND CONVERT(CHAR(8),L.Fecha,112) = CONVERT(CHAR(8),@FechaLector,112)
	SET NOCOUNT OFF;
END

GO
