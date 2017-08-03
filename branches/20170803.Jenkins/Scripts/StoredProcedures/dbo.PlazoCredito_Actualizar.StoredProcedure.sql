IF EXISTS (SELECT 1 FROM sysobjects WHERE name =  'PlazoCredito_Actualizar' AND type = 'P')
BEGIN
	DROP PROCEDURE [dbo].[PlazoCredito_Actualizar]
END 
GO
--======================================================    
-- Author     : Sergio Alberto Gamez Gomez  
-- Create date: 18/05/2016  
-- Description:     
-- SpName     : PlazoCredito_Actualizar    
--======================================================    
CREATE PROCEDURE [dbo].[PlazoCredito_Actualizar]    
@PlazoCreditoID int,    
@Descripcion varchar(50),    
@Activo bit,    
@UsuarioModificacionID int    
AS    
BEGIN    
SET NOCOUNT ON;    
	UPDATE PlazoCredito SET    
		Descripcion = @Descripcion,    
		Activo = @Activo,    
		UsuarioModificacionID = @UsuarioModificacionID,    
		FechaModificacion = GETDATE()    
	WHERE PlazoCreditoID = @PlazoCreditoID    
SET NOCOUNT OFF;    
END