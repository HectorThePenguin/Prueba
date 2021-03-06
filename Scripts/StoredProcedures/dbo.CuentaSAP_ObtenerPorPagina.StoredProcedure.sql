USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CuentaSAP_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CuentaSAP_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[CuentaSAP_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--=============================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 2013/12/10
-- Description: 
-- CuentaSAP_ObtenerPorPagina 0,'','','<ROOT><Datos><tipoCuenta>2</tipoCuenta></Datos></ROOT>',1,1,10
--=============================================
CREATE PROCEDURE [dbo].[CuentaSAP_ObtenerPorPagina]
@CuentaSAPID INT,
@CuentaSAP varchar(10),
@Descripcion varchar(50),
@xmlTipoCuenta XML,
@Activo BIT,
@Inicio INT,
@Limite INT
AS
BEGIN
	SET NOCOUNT ON;

	CREATE TABLE #tTipoCuenta
	(
		TipoCuentaID INT
	)

	INSERT INTO #tTipoCuenta
	SELECT DISTINCT T.N.value('./tipoCuenta[1]','INT') AS TipoCuenta
	FROM @xmlTipoCuenta.nodes('/ROOT/Datos') as T(N)

	SELECT
		ROW_NUMBER() OVER (ORDER BY cs.Descripcion ASC) AS RowNum,
		cs.CuentaSAP,
		CuentaSAPID,
		cs.Descripcion,
		cs.Activo,
		tc.TipoCuentaID,
		tc.Descripcion [TipoCuenta]
		INTO #Datos
	FROM CuentaSAP cs
	INNER JOIN TipoCuenta tc on cs.TipoCuentaID = tc.TipoCuentaID
	INNER JOIN #tTipoCuenta ttc
		ON (tc.TipoCuentaID = ttc.TipoCuentaID)
	WHERE @CuentaSAPID in (0, cs.CuentaSAPID)
			AND CuentaSAP like '%'+ @CuentaSAP +'%'
			AND cs.Descripcion LIKE '%' + @Descripcion + '%'
			AND (cs.Activo = @Activo OR @Activo IS NULL)

	SELECT
	CuentaSAP,
		CuentaSAPID,
		Descripcion,
		Activo,
		TipoCuentaID,
		TipoCuenta
	FROM #Datos
	WHERE RowNum BETWEEN @Inicio AND @Limite

	SELECT 
		COUNT(CuentaSAPID)AS TotalReg
	From #Datos

	DROP TABLE #Datos
	SET NOCOUNT OFF;
END

GO
