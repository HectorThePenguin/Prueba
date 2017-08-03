USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionAlertas_ObtenerAlertasPorPaginas]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ConfiguracionAlertas_ObtenerAlertasPorPaginas]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionAlertas_ObtenerAlertasPorPaginas]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Valenzuela Rivera Juan Diego 
-- Create date: 15/03/2016
-- Description: Obtiene las Alertas
-- =============================================  
CREATE PROCEDURE [dbo].[ConfiguracionAlertas_ObtenerAlertasPorPaginas]
	@Inicio INT,         
	@Limite INT,
	@Descripcion VARCHAR(MAX)
AS
BEGIN
	SET NOCOUNT ON;  
	SELECT 
		ROW_NUMBER() OVER ( ORDER BY a.AlertaID ASC) AS RowNum,    
		a.AlertaID,
		a.Descripcion
	INTO
		#Datos
	FROM
		Alerta a
	WHERE
		a.Activo = 1 
	AND
		a.Descripcion LIKE '%' + @Descripcion + '%'
	SELECT 
		a.AlertaID,
		a.Descripcion
	FROM 
		#Datos a
	WHERE
		RowNum BETWEEN @Inicio AND @Limite
	SELECT 
		COUNT(AlertaID)AS TotalReg 
	FROM #Datos	
	DROP TABLE #Datos
	SET NOCOUNT OFF;  
END