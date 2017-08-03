IF EXISTS(SELECT ''
FROM sys.objects
WHERE [object_id] = Object_id(N'[dbo].[ProveedorRetencion_Actualizar]'))
	DROP PROCEDURE [dbo].[ProveedorRetencion_Actualizar]; 
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 09/03/2016 12:00:00 a.m.
-- Description: 
-- SpName     : ProveedorRetencion_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[ProveedorRetencion_Actualizar]
@ProveedorRetencionID int,
@ProveedorID int,
@RetencionID int,
@IvaID int,
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE ProveedorRetencion SET
		ProveedorID = @ProveedorID,
		RetencionID = @RetencionID,
		IvaID = @IvaID,
		Activo = @Activo,
		UsuarioCreacionID = @UsuarioCreacionID,
		FechaModificacion = GETDATE()
	WHERE ProveedorRetencionID = @ProveedorRetencionID
	SET NOCOUNT OFF;
END
GO

