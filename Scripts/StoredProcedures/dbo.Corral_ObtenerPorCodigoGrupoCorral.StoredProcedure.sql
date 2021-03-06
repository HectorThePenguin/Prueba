USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerPorCodigoGrupoCorral]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Corral_ObtenerPorCodigoGrupoCorral]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerPorCodigoGrupoCorral]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author:		Gilberto Carranza
-- Create date: 07/04/2014
-- Description:	Obtiene un Corral.
-- Corral_ObtenerPorCodigoGrupoCorral 'D25', 4, 1
--=============================================
CREATE PROCEDURE [dbo].[Corral_ObtenerPorCodigoGrupoCorral]
@Codigo CHAR(10),
@OrganizacionID INT, 
@GrupoCorralID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 	    
		C.CorralID
		,C.OrganizacionID
		,C.Codigo
		,C.TipoCorralID
		,C.Capacidad
		,C.MetrosLargo
		,C.MetrosAncho
		,C.Seccion
		,C.Orden
		,C.Activo				
		,GC.Descripcion AS GrupoCorral
		, TC.Descripcion AS TipoCorral
		, GC.GrupoCorralID
		FROM Corral C
		INNER JOIN TipoCorral TC
			ON (C.TipoCorralID = TC.TipoCorralID)
		INNER JOIN GrupoCorral GC
			ON (TC.GrupoCorralID = GC.GrupoCorralID
				AND @GrupoCorralID IN (GC.GrupoCorralID, 0))
		WHERE Codigo = @Codigo			  
			  AND C.OrganizacionID = @OrganizacionID
	SET NOCOUNT OFF;
END

GO
