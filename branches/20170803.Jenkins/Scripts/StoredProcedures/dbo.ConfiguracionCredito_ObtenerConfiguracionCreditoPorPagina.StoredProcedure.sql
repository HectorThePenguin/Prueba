IF EXISTS (SELECT 1 FROM sysobjects WHERE name =  'ConfiguracionCredito_ObtenerConfiguracionCreditoPorPagina' AND type = 'P')
BEGIN
	DROP PROCEDURE [dbo].[ConfiguracionCredito_ObtenerConfiguracionCreditoPorPagina]
END 
GO
--======================================================      
-- Author     : Sergio Alberto Gamez Gomez    
-- Create date: 21/06/2016    
-- Description:       
-- SpName     : ConfiguracionCredito_ObtenerConfiguracionCreditoPorPagina '',1,1,15       
--======================================================      
CREATE PROCEDURE [dbo].[ConfiguracionCredito_ObtenerConfiguracionCreditoPorPagina]     
@Descripcion VARCHAR(100),   
@Activo BIT,     
@Inicio INT,    
@Limite INT      
AS  
BEGIN      
SET NOCOUNT ON
  
	DECLARE @ConfiguracionCredito AS TABLE (      
		ConfiguracionCreditoID INT,      
		Descripcion VARCHAR(100),    
		Activo BIT,
		TipoCreditoID INT, 
		PlazoCreditoID INT,   
		DescripcionPlazo VARCHAR(100),      
		RowNum INT IDENTITY      
	)    

	INSERT INTO @ConfiguracionCredito      
	SELECT   
		CC.ConfiguracionCreditoID,  
		TC.Descripcion,  
		CC.Activo,
		TC.TipoCreditoID,
		PC.PlazoCreditoID,
		PC.Descripcion AS DescripcionPlazo  
	FROM ConfiguracionCredito  CC
	INNER JOIN TipoCredito TC
		ON TC.TipoCreditoID = CC.TipoCreditoID   
	INNER JOIN PlazoCredito PC
		ON PC.PlazoCreditoID = CC.PlazoCreditoID   
	WHERE TC.Descripcion LIKE '%' + RTRIM(LTRIM(@Descripcion)) + '%'     
	AND CC.Activo = @Activo    

	SELECT     
		ConfiguracionCreditoID,  
		Descripcion,  
		Activo,
		TipoCreditoID,
		PlazoCreditoID,
		DescripcionPlazo   
	FROM @ConfiguracionCredito      
	WHERE RowNum BETWEEN @Inicio AND @Limite  
	 
	SELECT COUNT(ConfiguracionCreditoID) AS TotalReg FROM @ConfiguracionCredito    

SET NOCOUNT OFF    
END