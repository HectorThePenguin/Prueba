IF object_id('dbo.TratamientoCentros_Crear', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE dbo.TratamientoCentros_Crear
END
GO
--======================================================  
-- Author     : Sergio Alberto Gamez Gomez  
-- Create date: 12/11/2015 12:00:00 a.m.  
-- Description:   
-- SpName     : TratamientoCentros_Crear
--======================================================  
CREATE PROCEDURE [dbo].[TratamientoCentros_Crear]  
@OrganizacionID int,  
@CodigoTratamiento int,  
@Descripcion varchar(100),  
@Activo bit,  
@UsuarioCreacionID int  
AS  
BEGIN  
SET NOCOUNT ON;  

	DECLARE @TratamientoID INT

	SELECT @TratamientoID = ISNULL(MAX(TratamientoID),0) + 1
	FROM Sukarne.dbo.CatTratamiento
	WHERE OrganizacionID = @OrganizacionID

	INSERT Sukarne.dbo.CatTratamiento(
		TratamientoID,  
		OrganizacionID,  
		CodigoTratamiento,
		Descripcion,  
		Activo,  
		UsuarioCreacionID,  
		FechaCreacion,
		FechaModificacion,
		UsuarioModificacionID  
	)  
	VALUES(
		@TratamientoID ,
		@OrganizacionID,  
		@CodigoTratamiento,  
		@Descripcion,
		@Activo,  
		@UsuarioCreacionID,  
		GETDATE(),  
		GETDATE(),
		@UsuarioCreacionID
	)
  
	SELECT TratamientoID, OrganizacionID, CodigoTratamiento, Descripcion,  Activo,  UsuarioCreacionID,  FechaCreacion,	FechaModificacion,	UsuarioModificacionID 
	FROM Sukarne.dbo.CatTratamiento
	WHERE OrganizacionID = @OrganizacionID AND TratamientoID = @TratamientoID

SET NOCOUNT OFF;  
END