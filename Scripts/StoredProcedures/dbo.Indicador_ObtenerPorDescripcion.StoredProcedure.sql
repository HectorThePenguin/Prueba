USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Indicador_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Indicador_ObtenerPorDescripcion]
GO
/****** Object:  StoredProcedure [dbo].[Indicador_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 02/09/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Indicador_ObtenerPorDescripcion
--======================================================
CREATE PROCEDURE [dbo].[Indicador_ObtenerPorDescripcion]
@Descripcion varchar(50)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		IndicadorID,
		Descripcion,
		Activo
	FROM Indicador
	WHERE Descripcion = @Descripcion
	SET NOCOUNT OFF;
END

GO
