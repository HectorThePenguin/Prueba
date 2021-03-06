USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerPorCodigo]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Corral_ObtenerPorCodigo]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerPorCodigo]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author:		Gilberto Carranza
-- Create date: 26/11/2013
-- Description:	Obtiene un Corral.
-- Corral_ObtenerPorCodigo 'D25', 4, 1
--=============================================
CREATE PROCEDURE [dbo].[Corral_ObtenerPorCodigo]
@Codigo CHAR(10),
@OrganizacionID INT, 
@TipoCorralID INT
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
		FROM Corral C		
		WHERE Codigo = @Codigo
			  AND @TipoCorralID IN (C.TipoCorralId, 0)
			  AND C.OrganizacionID = @OrganizacionID
	SET NOCOUNT OFF;
END

GO
