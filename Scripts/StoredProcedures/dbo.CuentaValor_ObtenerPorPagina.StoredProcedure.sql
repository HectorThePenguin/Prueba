USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CuentaValor_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CuentaValor_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[CuentaValor_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 16/09/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CuentaValor_ObtenerPorPagina 0,0,0,1,1,15
--======================================================
CREATE PROCEDURE [dbo].[CuentaValor_ObtenerPorPagina]
@CuentaValorID int,
@CuentaID int,
@OrganizacionID int,
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY CuentaValorID ASC) AS [RowNum],
		CuentaValorID,
		CuentaID,
		OrganizacionID,
		Valor,
		Activo		
	INTO #CuentaValor
	FROM CuentaValor
	WHERE Activo = @Activo
	AND @OrganizacionID in (OrganizacionID , 0)
	AND @CuentaID in (CuentaID , 0)
	SELECT
		cv.CuentaValorID,
		cu.CuentaID,
		cu.Descripcion AS Cuenta,
		o.OrganizacionID,
		o.Descripcion AS Organizacion,
		cv.Valor,
		cv.Activo		
	FROM #CuentaValor cv
	INNER JOIN Organizacion o on cv.OrganizacionID = o.OrganizacionID
	inner join Cuenta cu on cv.CuentaID = cu.CuentaID
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(CuentaValorID) AS [TotalReg]
	FROM #CuentaValor
	DROP TABLE #CuentaValor
	SET NOCOUNT OFF;
END

GO
