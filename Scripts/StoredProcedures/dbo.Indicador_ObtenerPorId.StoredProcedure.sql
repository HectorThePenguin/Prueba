USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Indicador_ObtenerPorId]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Indicador_ObtenerPorId]
GO
/****** Object:  StoredProcedure [dbo].[Indicador_ObtenerPorId]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author:		Jesus Alvarez
-- Create date: 17/05/2014
-- Description:	Obtiene un indicador producto por producto id
-- Indicador_ObtenerPorId 1
--======================================================
CREATE PROCEDURE [dbo].[Indicador_ObtenerPorId]
@IndicadorID INT
AS
BEGIN
	SELECT  
		IndicadorID,
		Descripcion,
		Activo
	FROM Indicador (NOLOCK)
	WHERE IndicadorID = @IndicadorID
END

GO
