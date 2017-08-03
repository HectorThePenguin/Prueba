IF EXISTS (SELECT 1 FROM sysobjects WHERE name =  'TipoCredito_ObtenerTodos' AND type = 'P')
BEGIN
	DROP PROCEDURE [dbo].[TipoCredito_ObtenerTodos]
END 
GO
--======================================================      
-- Author     : Sergio Alberto Gamez Gomez    
-- Create date: 22/06/2016    
-- Description:       
-- SpName     : TipoCredito_ObtenerTodos
--======================================================      
CREATE PROCEDURE [dbo].[TipoCredito_ObtenerTodos]     
AS  
BEGIN      
SET NOCOUNT ON  
      
	SELECT   
		TipoCreditoID,  
		Descripcion,  
		Activo  
	FROM TipoCredito      
  
SET NOCOUNT OFF    
END