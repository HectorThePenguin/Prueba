USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerValidaCorralConLoteConExistenciaActivo]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Corral_ObtenerValidaCorralConLoteConExistenciaActivo]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerValidaCorralConLoteConExistenciaActivo]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author:		Gilberto Carranza
-- Create date: 2014-09-05
-- Description:	Obtiene un Corral si tiene existencia
-- Corral_ObtenerValidaCorralConLoteConExistenciaActivo 100
--=============================================
CREATE PROCEDURE [dbo].[Corral_ObtenerValidaCorralConLoteConExistenciaActivo]
@CorralID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		C.CorralID,
		C.OrganizacionID,
		C.Codigo,
		C.TipoCorralID,
		C.Capacidad,
		C.MetrosLargo,
		C.MetrosAncho,
		C.Seccion,
		C.Orden,
		C.Activo,
		C.FechaCreacion,
		C.UsuarioCreacionID
		, O.Descripcion			AS Organizacion
		, TC.Descripcion		AS TipoCorral
	FROM Corral C
	INNER JOIN Lote L
		ON (C.CorralID = L.CorralID
			AND L.Activo = 1
			AND L.Cabezas > 0)
	INNER JOIN Organizacion O
		ON (C.OrganizacionID = O.OrganizacionID)
	INNER JOIN TipoCorral TC
		ON (C.TipoCorralID = TC.TipoCorralID)
	WHERE C.CorralID = @CorralID
	SET NOCOUNT OFF;
END

GO
