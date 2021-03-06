USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Cliente_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Cliente_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[Cliente_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 03/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Cliente_ObtenerPorPagina
-- 001 Jorge Luis Velazquez Araujo 02/12/2015 **Se agrega el parametro del Codigo de SAP
--======================================================
CREATE PROCEDURE [dbo].[Cliente_ObtenerPorPagina]
@ClienteID int,
@Descripcion varchar(50),
@CodigoSAP varchar(10),--001
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY Descripcion ASC) AS [RowNum],
		ClienteID,
		CodigoSAP,
		Descripcion,
		Activo
	INTO #Cliente
	FROM Cliente
	WHERE (Descripcion like '%' + @Descripcion + '%' OR @Descripcion = '') 
	AND @CodigoSAP in (CodigoSAP,'')--001
	AND Activo = @Activo
	SELECT
		ClienteID,
		CodigoSAP,
		Descripcion,
		Activo
	FROM #Cliente
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(ClienteID) AS [TotalReg]
	FROM #Cliente
	DROP TABLE #Cliente
	SET NOCOUNT OFF;
END

GO
