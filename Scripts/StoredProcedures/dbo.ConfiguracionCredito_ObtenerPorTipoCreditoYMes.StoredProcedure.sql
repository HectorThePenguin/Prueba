IF EXISTS (SELECT 1 FROM sysobjects WHERE name =  'ConfiguracionCredito_ObtenerPorTipoCreditoYMes' AND type = 'P')
BEGIN
	DROP PROCEDURE [dbo].[ConfiguracionCredito_ObtenerPorTipoCreditoYMes]
END 
GO
--======================================================      
-- Author     : Sergio Alberto Gamez Gomez    
-- Create date: 23/06/2016    
-- Description:       
-- SpName     : ConfiguracionCredito_ObtenerPorTipoCreditoYMes 1,1      
--======================================================      
CREATE PROCEDURE [dbo].[ConfiguracionCredito_ObtenerPorTipoCreditoYMes]     
@TipoCreditoID INT,    
@PlazoCreditoID INT      
AS  
BEGIN      
SET NOCOUNT ON
  
	SELECT   
		ConfiguracionCreditoID
	FROM ConfiguracionCredito
	WHERE TipoCreditoID = @TipoCreditoID --AND PlazoCreditoID = @PlazoCreditoID
	

SET NOCOUNT OFF    
END