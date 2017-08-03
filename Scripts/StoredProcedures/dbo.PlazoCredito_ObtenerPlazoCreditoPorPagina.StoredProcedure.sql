IF EXISTS (SELECT 1 FROM sysobjects WHERE name =  'PlazoCredito_ObtenerPlazoCreditoPorPagina' AND type = 'P')
BEGIN
	DROP PROCEDURE [dbo].[PlazoCredito_ObtenerPlazoCreditoPorPagina]
END 
GO
--======================================================      
-- Author     : Sergio Alberto Gamez Gomez    
-- Create date: 17/05/2016    
-- Description:       
-- SpName     : PlazoCredito_ObtenerPlazoCreditoPorPagina 'TRES',1,1,15       
--======================================================      
CREATE PROCEDURE [dbo].[PlazoCredito_ObtenerPlazoCreditoPorPagina]     
@Descripcion VARCHAR(100),   
@Activo BIT,     
@Inicio INT,    
@Limite INT      
AS  
BEGIN      
SET NOCOUNT ON  
  
	DECLARE @PlazoCredito AS TABLE (      
		PlazoCreditoID INT,      
		Descripcion VARCHAR(100),    
		Activo BIT,      
		RowNum INT IDENTITY      
	)    

	INSERT INTO @PlazoCredito      
	SELECT   
		PlazoCreditoID,  
		Descripcion,  
		Activo  
	FROM PlazoCredito      
	WHERE Descripcion LIKE '%' + RTRIM(LTRIM(@Descripcion)) + '%'     
	AND Activo = @Activo    

	SELECT     
		PlazoCreditoID,  
		Descripcion,  
		Activo   
	FROM @PlazoCredito      
	WHERE RowNum BETWEEN @Inicio AND @Limite  
	 
	SELECT COUNT(PlazoCreditoID) AS TotalReg FROM @PlazoCredito    

SET NOCOUNT OFF    
END