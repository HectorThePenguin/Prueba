USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoCuenta_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoCuenta_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[TipoCuenta_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
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
CREATE PROCEDURE [dbo].[TipoCuenta_ObtenerPorPagina]
@TipoCuentaID INT,
@Descripcion varchar(50),
@Activo BIT,
@Inicio INT,
@Limite INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY Descripcion ASC) AS RowNum,
		TipoCuentaID,
		Descripcion,
		Activo
		INTO #Datos
	FROM TipoCuenta
	WHERE (Descripcion like '%' + @Descripcion + '%' OR @Descripcion = '')
	AND Activo = @Activo
	SELECT
		TipoCuentaID,
		Descripcion,
		Activo
	FROM #Datos
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT 
		COUNT(TipoCuentaID)AS TotalReg
	From #Datos
	DROP TABLE #Datos
	SET NOCOUNT OFF;
END

GO
