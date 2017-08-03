IF EXISTS (SELECT 1 FROM sysobjects WHERE name =  'PlazoCredito_ObtenerPorDescripcion' AND type = 'P')
BEGIN
	DROP PROCEDURE [dbo].[PlazoCredito_ObtenerPorDescripcion]
END 
GO
--======================================================      
-- Author     : Sergio Alberto Gámez Gómez     
-- Create date: 18/05/2016   
-- Description:       
-- SpName     : PlazoCredito_ObtenerPorDescripcion  ''    
--======================================================      
CREATE PROCEDURE [dbo].[PlazoCredito_ObtenerPorDescripcion]      
@Descripcion varchar(100)      
AS      
BEGIN      
SET NOCOUNT ON;      
	SELECT      
		PlazoCreditoID,  
		Descripcion,  
		Activo     
	FROM PlazoCredito  
	WHERE Descripcion = @Descripcion      
SET NOCOUNT OFF;      
END