IF EXISTS (SELECT 1 FROM sysobjects WHERE name =  'TipoCredito_Crear' AND type = 'P')
BEGIN
	DROP PROCEDURE [dbo].[TipoCredito_Crear]
END 
GO
--======================================================      
-- Author     : Sergio Alberto Gamez Gomez      
-- Create date: 18/05/2016   
-- Description:    
-- SpName     : TipoCredito_Crear      
--======================================================      
CREATE PROCEDURE [dbo].[TipoCredito_Crear]    
@Descripcion varchar(100),    
@Activo bit,    
@UsuarioCreacionID int    
AS    
BEGIN    
SET NOCOUNT ON;  
    
	INSERT INTO TipoCredito (    
		Descripcion,  
		Activo,  
		FechaCreacion,  
		UsuarioCreacionID  
	)    
	VALUES(    
		@Descripcion,    
		@Activo,    
		GETDATE(),    
		@UsuarioCreacionID   
	)    

	SELECT SCOPE_IDENTITY()    
   
SET NOCOUNT OFF;    
END