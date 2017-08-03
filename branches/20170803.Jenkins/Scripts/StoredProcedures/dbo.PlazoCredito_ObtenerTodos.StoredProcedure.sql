IF EXISTS (SELECT 1 FROM sysobjects WHERE name =  'PlazoCredito_ObtenerTodos' AND type = 'P')
BEGIN
	DROP PROCEDURE [dbo].[PlazoCredito_ObtenerTodos]
END 
GO
--======================================================      
-- Author     : Sergio Alberto Gamez Gomez    
-- Create date: 22/06/2016    
-- Description:       
-- SpName     : PlazoCredito_ObtenerTodos 'TRES',1,1,15       
--======================================================      
CREATE PROCEDURE [dbo].[PlazoCredito_ObtenerTodos]     
AS  
BEGIN      
SET NOCOUNT ON  
  
	SELECT   
		PlazoCreditoID,  
		Descripcion,  
		Activo  
	FROM PlazoCredito      

SET NOCOUNT OFF    
END