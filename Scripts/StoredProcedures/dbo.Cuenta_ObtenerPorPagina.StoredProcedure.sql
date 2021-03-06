USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Cuenta_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Cuenta_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[Cuenta_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jos� Gilberto Quintero L�pez
-- Create date: 2013/11/28
-- Description: 
-- 
--=============================================
CREATE PROCEDURE [dbo].[Cuenta_ObtenerPorPagina]
@CuentaID INT,
@Descripcion varchar(50),
@Activo BIT,
@Inicio INT,
@Limite INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY c.Descripcion ASC) AS RowNum,
		CuentaID,
		c.Descripcion,
		c.ClaveCuenta,
		c.Activo,
		c.TipoCuentaID,
		tc.Descripcion [TipoCuenta]
		INTO #Datos
	FROM Cuenta c
	INNER JOIN TipoCuenta tc on c.TipoCuentaID = tc.TipoCuentaID
	WHERE (c.Descripcion like '%' + @Descripcion + '%' OR @Descripcion = '')
	AND c.Activo = @Activo
	SELECT
		CuentaID,
		Descripcion,
		ClaveCuenta,
		Activo,
		TipoCuentaID,
		TipoCuenta		
	FROM #Datos
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT 
		COUNT(CuentaID)AS TotalReg
	From #Datos
	DROP TABLE #Datos
	SET NOCOUNT OFF;
END

GO
