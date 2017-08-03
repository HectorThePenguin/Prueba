IF EXISTS(SELECT ''
FROM sys.objects
WHERE [object_id] = Object_id(N'[dbo].[ProveedorRetencion_Crear]'))
	DROP PROCEDURE [dbo].[ProveedorRetencion_Crear]; 
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 09/03/2016 12:00:00 a.m.
-- Description: 
-- SpName     : ProveedorRetencion_Crear
--======================================================
CREATE PROCEDURE [dbo].[ProveedorRetencion_Crear]
@ProveedorRetencionID int,
@ProveedorID int,
@RetencionID int,
@IvaID int,
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT ProveedorRetencion (
		ProveedorRetencionID,
		ProveedorID,
		RetencionID,
		IvaID,
		Activo,
		UsuarioCreacionID,
		FechaCreacion
	)
	VALUES(
		@ProveedorRetencionID,
		@ProveedorID,
		@RetencionID,
		@IvaID,
		@Activo,
		@UsuarioCreacionID,
		GETDATE()
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END
GO

