IF EXISTS (SELECT 1 FROM sysobjects WHERE name =  'ConfiguracionCredito_ObtenerRetencionesPorID' AND type = 'P')
BEGIN
	DROP PROCEDURE [dbo].[ConfiguracionCredito_ObtenerRetencionesPorID]
END 
GO
--======================================================      
-- Author     : Sergio Alberto Gamez Gomez    
-- Create date: 21/06/2016    
-- Description:       
-- SpName     : ConfiguracionCredito_ObtenerRetencionesPorID 1       
--======================================================      
CREATE PROCEDURE [dbo].[ConfiguracionCredito_ObtenerRetencionesPorID]     
@ConfiguracionCreditoID INT      
AS  
BEGIN      
SET NOCOUNT ON
  
	SELECT 
		NumeroMes, 
		PorcentajeRetencion
	FROM ConfiguracionCreditoRetenciones
	WHERE ConfiguracionCreditoID = @ConfiguracionCreditoID
	ORDER BY NumeroMes DESC     

SET NOCOUNT OFF    
END