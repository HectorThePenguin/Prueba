IF object_id('dbo.TratamientoCentros_Actualizar', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE dbo.TratamientoCentros_Actualizar
END
GO
--======================================================  
-- Author     : Sergio Alberto Gamez Gomez  
-- Create date: 12/11/2015 12:00:00 a.m.  
-- Description:   
-- SpName     : TratamientoCentros_Actualizar
--======================================================   
CREATE PROCEDURE [dbo].[TratamientoCentros_Actualizar]  
@TratamientoID int,  
@OrganizacionID int,  
@CodigoTratamiento int,  
@Descripcion varchar(100),    
@Activo bit,  
@UsuarioModificacionID int  
AS  
BEGIN  
 SET NOCOUNT ON;  
	UPDATE Sukarne.dbo.CatTratamiento SET  
	CodigoTratamiento = @CodigoTratamiento,  
	Descripcion = @Descripcion,  
	Activo = @Activo,  
	UsuarioModificacionID = @UsuarioModificacionID,  
	FechaModificacion = GETDATE()  
	WHERE TratamientoID = @TratamientoID AND OrganizacionID = @OrganizacionID    
 SET NOCOUNT OFF;  
END