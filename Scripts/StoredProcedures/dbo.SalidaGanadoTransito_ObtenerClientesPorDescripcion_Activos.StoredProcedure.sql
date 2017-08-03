USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaGanadoTransito_ObtenerClientesPorDescripcion_Activos]    Script Date: 12/04/2016 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[SalidaGanadoTransito_ObtenerClientesPorDescripcion_Activos]
GO
/****** Object:  StoredProcedure [dbo].[SalidaGanadoTransito_ObtenerClientesPorDescripcion_Activos]    Script Date: 12/04/2016 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Torres Lugo Manuel
-- Create date: 12/04/2016
-- Description: 
-- SpName     : SalidaGanadoTransito_ObtenerClientesPorDescripcion_Activos
--======================================================
CREATE PROCEDURE [dbo].[SalidaGanadoTransito_ObtenerClientesPorDescripcion_Activos]
@Descripcion VARCHAR(150),
@Inicio INT, 
@Limite INT 
AS
BEGIN	
	SET NOCOUNT ON
	SELECT ROW_NUMBER() OVER ( ORDER BY ClienteID ASC) AS RowNum, ClienteID, CodigoSAP, Descripcion 
	INTO #ClienteTemp 
	FROM  Cliente WHERE Descripcion LIKE '%' + @Descripcion + '%'
	AND Activo = 1

	SELECT 
		ClienteID, 
		CodigoSAP, 
		Descripcion
	FROM #ClienteTemp 
	WHERE RowNum BETWEEN @Inicio AND @Limite

	SELECT 
		COUNT(DISTINCT ClienteID)AS TotalReg 
	FROM #ClienteTemp
	DROP TABLE #ClienteTemp	
	SET NOCOUNT OFF;
END
