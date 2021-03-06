IF EXISTS(SELECT *
      FROM   sys.objects
      WHERE  [object_id] = Object_id(N'[dbo].[CuentaSAP_ObtenerTodasPorPagina]'))
			DROP PROCEDURE [dbo].[CuentaSAP_ObtenerTodasPorPagina]
go

--=============================================
-- Author     : Roque Solis
-- Create date: 2014/06/24
-- Origen     : Apinterfaces
-- Description: Obtiene las todas las cuentas sap activias
-- CuentaSAP_ObtenerTodasPorPagina 0,'','',1,1,10,5
-- 001 Jorge Luis Velazquez Araujo 14/09/2015 ** Se agrega el filtro del Tipo de Cuenta
--=============================================
CREATE PROCEDURE [CuentaSAP_ObtenerTodasPorPagina]
@CuentaSAPID INT,
@CuentaSAP varchar(10),
@Descripcion varchar(50),
@Activo BIT,
@Inicio INT,
@Limite INT,
@TipoCuentaID INT --001
AS
BEGIN
	SET NOCOUNT ON;

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
	WHERE @CuentaSAPID in (0, cs.CuentaSAPID)
	AND @TipoCuentaID in (0, cs.TipoCuentaID)
			AND CuentaSAP like '%'+ @CuentaSAP +'%'
			AND cs.Descripcion LIKE '%' + @Descripcion + '%'
			AND cs.Activo = @Activo OR @Activo IS NULL
			

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
go