IF EXISTS(SELECT ''
FROM sys.objects
WHERE [object_id] = Object_id(N'[dbo].[ProveedorRetencion_ObtenerTodos]'))
	DROP PROCEDURE [dbo].[ProveedorRetencion_ObtenerTodos]; 
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 09/03/2016 12:00:00 a.m.
-- Description: 
-- SpName     : ProveedorRetencion_ObtenerTodos
--======================================================
CREATE PROCEDURE [dbo].[ProveedorRetencion_ObtenerTodos]
@Activo BIT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ProveedorRetencionID,
		ProveedorID,
		RetencionID,
		IvaID,
		Activo
	FROM ProveedorRetencion
	WHERE Activo = @Activo OR @Activo IS NULL
	SET NOCOUNT OFF;
END
GO

