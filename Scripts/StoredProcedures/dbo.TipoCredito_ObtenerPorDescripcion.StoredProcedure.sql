IF EXISTS (SELECT 1 FROM sysobjects WHERE name =  'TipoCredito_ObtenerPorDescripcion' AND type = 'P')
BEGIN
	DROP PROCEDURE [dbo].[TipoCredito_ObtenerPorDescripcion]
END 
GO
--======================================================      
-- Author     : Sergio Alberto Gámez Gómez     
-- Create date: 18/05/2016   
-- Description:       
-- SpName     : TipoCredito_ObtenerPorDescripcion  ''    
--======================================================      
CREATE PROCEDURE [dbo].[TipoCredito_ObtenerPorDescripcion]      
@Descripcion varchar(100)      
AS      
BEGIN      
SET NOCOUNT ON;      
	SELECT      
		TipoCreditoID,  
		Descripcion,  
		Activo     
	FROM TipoCredito  
	WHERE Descripcion = @Descripcion      
SET NOCOUNT OFF;      
END