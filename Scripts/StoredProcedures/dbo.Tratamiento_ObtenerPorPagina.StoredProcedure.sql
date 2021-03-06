USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Tratamiento_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Tratamiento_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[Tratamiento_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 17/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Tratamiento_ObtenerPorPagina
--======================================================
CREATE PROCEDURE [dbo].[Tratamiento_ObtenerPorPagina]
@TratamientoID int,
@Descripcion varchar(50),
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY TratamientoID ASC) AS [RowNum],
		TratamientoID,
		OrganizacionID,
		CodigoTratamiento,
		TipoTratamientoID,
		Sexo,
		RangoInicial,
		RangoFinal,
		Activo
	INTO #Tratamiento
	FROM Tratamiento
	WHERE 	Activo = @Activo
	SELECT
		TratamientoID,
		OrganizacionID,
		CodigoTratamiento,
		TipoTratamientoID,
		Sexo,
		RangoInicial,
		RangoFinal,
		Activo
	FROM #Tratamiento
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(TratamientoID) AS [TotalReg]
	FROM #Tratamiento
	DROP TABLE #Tratamiento
	SET NOCOUNT OFF;
END

GO
