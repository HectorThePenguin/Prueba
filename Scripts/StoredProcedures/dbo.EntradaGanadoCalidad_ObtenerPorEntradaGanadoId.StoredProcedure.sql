USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanadoCalidad_ObtenerPorEntradaGanadoId]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaGanadoCalidad_ObtenerPorEntradaGanadoId]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanadoCalidad_ObtenerPorEntradaGanadoId]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 28/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : EntradaGanadoCalidad_ObtenerPorEntradaGanadoId 3
--======================================================
CREATE PROCEDURE [dbo].[EntradaGanadoCalidad_ObtenerPorEntradaGanadoId] @EntradaGanadoID INT
AS
BEGIN
	SET NOCOUNT ON;
	CREATE TABLE #tCalidades (
		CalidadGanadoID INT
		,CalidadGanado VARCHAR(250)
		,Calidad VARCHAR(100)
		,Sexo CHAR(1)
		,Activo BIT
		)
	CREATE TABLE #tEntradaCalidad (
		EntradaGanadoCalidadID INT
		,EntradaGanadoID INT
		,CalidadGanadoID INT
		,Valor INT
		)
	INSERT INTO #tCalidades
	SELECT CalidadGanadoID
		,Descripcion [CalidadGanado]
		,Calidad
		,Sexo
		,Activo
	FROM CalidadGanado cg
	INSERT INTO #tEntradaCalidad
	SELECT ec.EntradaGanadoCalidadID
		,ec.EntradaGanadoID
		,cg.CalidadGanadoID		
		,ec.Valor		
	FROM CalidadGanado cg
	INNER JOIN EntradaGanadoCalidad ec ON ec.CalidadGanadoID = cg.CalidadGanadoID
	INNER JOIN EntradaGanado EG ON (EC.EntradaGanadoID = EG.EntradaGanadoID)
	WHERE ec.EntradaGanadoID = @EntradaGanadoID
		AND ec.Activo = 1
	SELECT ec.EntradaGanadoCalidadID
		,ec.EntradaGanadoID
		,ec.CalidadGanadoID
		,c.CalidadGanado [CalidadGanado]
		,c.Calidad
		,c.Sexo
		,ec.Valor
		,c.Activo
	FROM #tCalidades c
	LEFT JOIN #tEntradaCalidad ec ON c.CalidadGanadoID = ec.CalidadGanadoID
	DROP TABLE #tCalidades
	SET NOCOUNT OFF;
END

GO
