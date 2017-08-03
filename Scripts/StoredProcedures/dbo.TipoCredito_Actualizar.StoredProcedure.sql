IF EXISTS (SELECT 1 FROM sysobjects WHERE name =  'TipoCredito_Actualizar' AND type = 'P')
BEGIN
	DROP PROCEDURE [dbo].[TipoCredito_Actualizar]
END 
GO
--======================================================    
-- Author     : Sergio Alberto Gamez Gomez  
-- Create date: 18/05/2016  
-- Description:     
-- SpName     : TipoCredito_Actualizar    
--======================================================    
CREATE PROCEDURE [dbo].[TipoCredito_Actualizar]    
@TipoCreditoID int,    
@Descripcion varchar(50),    
@Activo bit,    
@UsuarioModificacionID int    
AS    
BEGIN    
SET NOCOUNT ON;    
	UPDATE TipoCredito SET    
		Descripcion = @Descripcion,    
		Activo = @Activo,    
		UsuarioModificacionID = @UsuarioModificacionID,    
		FechaModificacion = GETDATE()    
	WHERE TipoCreditoID = @TipoCreditoID    
SET NOCOUNT OFF;    
END