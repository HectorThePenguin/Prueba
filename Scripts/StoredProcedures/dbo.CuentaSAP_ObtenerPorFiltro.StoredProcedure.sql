USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CuentaSAP_ObtenerPorFiltro]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CuentaSAP_ObtenerPorFiltro]
GO
/****** Object:  StoredProcedure [dbo].[CuentaSAP_ObtenerPorFiltro]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--=============================================
-- Author     : José Gilberto Quintero López
-- Create date: 2013/12/10
-- Description: 
-- CuentaSAP_ObtenerPorFiltro 0,'1126001001', '','<ROOT><Datos><tipoCuenta>5</tipoCuenta></Datos></ROOT>',1
--=============================================
CREATE PROCEDURE [dbo].[CuentaSAP_ObtenerPorFiltro]
@CuentaSAPID INT,
@CuentaSAP VARCHAR(10),
@Descripcion VARCHAR(50),
@xmlTipoCuenta XML,
@Activo BIT
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
		cs.CuentaSAPID,
		cs.CuentaSAP,
		cs.Descripcion,
		cs.TipoCuentaID,
		tc.Descripcion as [TipoCuenta],
		cs.Activo
	FROM CuentaSAP cs
	INNER JOIN TipoCuenta tc on tc.TipoCuentaID = cs.TipoCuentaID
	INNER JOIN #tTipoCuenta ttc
		ON (tc.TipoCuentaID = ttc.TipoCuentaID)
	WHERE @CuentaSAPID in (0, cs.CuentaSAPID)
	AND (cs.CuentaSAP = @CuentaSAP OR @CuentaSAP = '')
	AND (cs.Activo = @Activo OR @Activo IS NULL)
	SET NOCOUNT OFF;
END

GO
