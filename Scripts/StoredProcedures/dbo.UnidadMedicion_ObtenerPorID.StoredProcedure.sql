USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[UnidadMedicion_ObtenerPorID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[UnidadMedicion_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[UnidadMedicion_ObtenerPorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Gilberto Carranza
-- Create date: 2014/01/14
-- Description: Obtiene Unidad Medicion por ID
-- UnidadMedicion_ObtenerPorID 1
--=============================================
CREATE PROCEDURE [dbo].[UnidadMedicion_ObtenerPorID]
@UnidadID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT UnidadID
		,  Descripcion
		,  ClaveUnidad
		,  Activo
	FROM UnidadMedicion
	WHERE UnidadID = @UnidadID
	SET NOCOUNT OFF;
END

GO
