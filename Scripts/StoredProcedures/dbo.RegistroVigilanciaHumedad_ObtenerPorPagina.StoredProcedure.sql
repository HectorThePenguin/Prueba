USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[RegistroVigilanciaHumedad_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[RegistroVigilanciaHumedad_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[RegistroVigilanciaHumedad_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 09/04/2015 12:00:00 a.m.
-- Description: 
-- SpName     : RegistroVigilanciaHumedad_ObtenerPorPagina
--======================================================
CREATE PROCEDURE [dbo].[RegistroVigilanciaHumedad_ObtenerPorPagina]
@RegistroVigilanciaHumedadID int,
@Descripcion varchar(50),
@Activo BIT,
@Inicio INT,
@Limite INT 

AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY RegistroVigilanciaHumedadID ASC) AS [RowNum],
		RegistroVigilanciaHumedadID,
		RegistroVigilanciaID,
		Humedad,
		NumeroMuestra,
		FechaMuestra,
		Activo
	INTO #RegistroVigilanciaHumedad
	FROM RegistroVigilanciaHumedad
	WHERE 
	--(Descripcion like '%' + @Descripcion + '%' OR @Descripcion = '') 
	 Activo = @Activo

	SELECT
		RegistroVigilanciaHumedadID,
		RegistroVigilanciaID,
		Humedad,
		NumeroMuestra,
		FechaMuestra,
		Activo
	FROM #RegistroVigilanciaHumedad
	WHERE RowNum BETWEEN @Inicio AND @Limite

	SELECT
	COUNT(RegistroVigilanciaHumedadID) AS [TotalReg]
	FROM #RegistroVigilanciaHumedad

	DROP TABLE #RegistroVigilanciaHumedad

	SET NOCOUNT OFF;
END

GO
