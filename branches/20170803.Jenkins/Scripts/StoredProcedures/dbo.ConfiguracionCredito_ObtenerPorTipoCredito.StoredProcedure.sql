IF EXISTS (SELECT 1 FROM sysobjects WHERE name =  'ConfiguracionCredito_ObtenerPorTipoCredito' AND type = 'P')
BEGIN
	DROP PROCEDURE [dbo].[ConfiguracionCredito_ObtenerPorTipoCredito]
END 
GO
--======================================================      
-- Author     : Sergio Alberto Gamez Gomez    
-- Create date: 23/06/2016    
-- Description:       
-- SpName     : ConfiguracionCredito_ObtenerPorTipoCredito 1      
--======================================================      
CREATE PROCEDURE [dbo].[ConfiguracionCredito_ObtenerPorTipoCredito]    
@TipoCreditoID INT    
AS  
BEGIN      
SET NOCOUNT ON
  
	SELECT   
		ConfiguracionCreditoID
	FROM ConfiguracionCredito
	WHERE TipoCreditoID = @TipoCreditoID
	

SET NOCOUNT OFF    
END