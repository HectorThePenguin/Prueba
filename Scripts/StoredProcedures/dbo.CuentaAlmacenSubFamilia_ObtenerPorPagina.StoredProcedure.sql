USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CuentaAlmacenSubFamilia_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CuentaAlmacenSubFamilia_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[CuentaAlmacenSubFamilia_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 03/09/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CuentaAlmacenSubFamilia_ObtenerPorPagina
--======================================================
CREATE PROCEDURE [dbo].[CuentaAlmacenSubFamilia_ObtenerPorPagina]
@CuentaAlmacenSubFamiliaID int,
@AlmacenID INT,
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY CuentaAlmacenSubFamiliaID ASC) AS [RowNum],
		CuentaAlmacenSubFamiliaID,
		AlmacenID,
		SubFamiliaID,
		CuentaSAPID,
		Activo
	INTO #CuentaAlmacenSubFamilia
	FROM CuentaAlmacenSubFamilia
	WHERE Activo = @Activo
	and @AlmacenID in (AlmacenID,0)
	SELECT
		cas.CuentaAlmacenSubFamiliaID,
		a.AlmacenID,
		a.Descripcion AS Almacen,
		sf.SubFamiliaID,
		sf.Descripcion AS SubFamilia,		
		cs.CuentaSAPID,
		cs.CuentaSAP,
		cs.Descripcion CuentaSAPDescripcion,
		cas.Activo
	FROM #CuentaAlmacenSubFamilia cas
	INNER JOIN SubFamilia sf on cas.SubFamiliaID = sf.SubFamiliaID
	INNER JOIN Almacen a on cas.AlmacenID = a.AlmacenID
	INNER JOIN CuentaSAP cs on cas.CuentaSAPID = cs.CuentaSAPID
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(CuentaAlmacenSubFamiliaID) AS [TotalReg]
	FROM #CuentaAlmacenSubFamilia
	DROP TABLE #CuentaAlmacenSubFamilia
	SET NOCOUNT OFF;
END

GO
