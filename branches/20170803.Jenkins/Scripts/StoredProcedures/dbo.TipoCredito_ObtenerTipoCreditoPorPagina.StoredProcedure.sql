IF EXISTS (SELECT 1 FROM sysobjects WHERE name =  'TipoCredito_ObtenerTipoCreditoPorPagina' AND type = 'P')
BEGIN
	DROP PROCEDURE [dbo].[TipoCredito_ObtenerTipoCreditoPorPagina]
END 
GO
--======================================================      
-- Author     : Sergio Alberto Gamez Gomez    
-- Create date: 17/05/2016    
-- Description:       
-- SpName     : TipoCredito_ObtenerTipoCreditoPorPagina 'TRES',1,1,15       
--======================================================      
CREATE PROCEDURE [dbo].[TipoCredito_ObtenerTipoCreditoPorPagina]     
@Descripcion VARCHAR(100),   
@Activo BIT,     
@Inicio INT,    
@Limite INT      
AS  
BEGIN      
SET NOCOUNT ON  
      
	DECLARE @TipoCredito AS TABLE (      
		TipoCreditoID INT,      
		Descripcion VARCHAR(100),    
		Activo BIT,      
		RowNum INT IDENTITY      
	)    

	INSERT INTO @TipoCredito      
	SELECT   
		TipoCreditoID,  
		Descripcion,  
		Activo  
	FROM TipoCredito      
	WHERE Descripcion LIKE '%' + RTRIM(LTRIM(@Descripcion)) + '%'     
	AND Activo = @Activo    

	SELECT     
		TipoCreditoID,  
		Descripcion,  
		Activo   
	FROM @TipoCredito      
	WHERE RowNum BETWEEN @Inicio AND @Limite  
	 
	SELECT COUNT(TipoCreditoID) AS TotalReg FROM @TipoCredito    
  
SET NOCOUNT OFF    
END