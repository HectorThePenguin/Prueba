IF EXISTS(SELECT ''
FROM sys.objects
WHERE [object_id] = Object_id(N'[dbo].[ProveedorRetencion_ObtenerPorID]'))
	DROP PROCEDURE [dbo].[ProveedorRetencion_ObtenerPorID]; 
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 09/03/2016 12:00:00 a.m.
-- Description: 
-- SpName     : ProveedorRetencion_ObtenerPorID
--======================================================
CREATE PROCEDURE [dbo].[ProveedorRetencion_ObtenerPorID]
@ProveedorRetencionID int
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
	WHERE ProveedorRetencionID = @ProveedorRetencionID
	SET NOCOUNT OFF;
END
GO

