IF EXISTS (SELECT 1 FROM sysobjects WHERE name =  'ConfiguracionCredito_ObtenerPorPlazoCredito' AND type = 'P')
BEGIN
	DROP PROCEDURE [dbo].[ConfiguracionCredito_ObtenerPorPlazoCredito]
END 
GO
--======================================================      
-- Author     : Sergio Alberto Gamez Gomez    
-- Create date: 23/06/2016    
-- Description:       
-- SpName     : ConfiguracionCredito_ObtenerPorPlazoCredito 3      
--======================================================      
CREATE PROCEDURE [dbo].[ConfiguracionCredito_ObtenerPorPlazoCredito]    
@PlazoCreditoID INT    
AS  
BEGIN      
SET NOCOUNT ON
  
	SELECT   
		ConfiguracionCreditoID
	FROM ConfiguracionCredito
	WHERE PlazoCreditoID = @PlazoCreditoID
	

SET NOCOUNT OFF    
END