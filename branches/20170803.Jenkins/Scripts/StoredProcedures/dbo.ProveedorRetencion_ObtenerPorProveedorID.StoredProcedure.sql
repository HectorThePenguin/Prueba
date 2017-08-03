IF EXISTS(SELECT ''
FROM sys.objects
WHERE [object_id] = Object_id(N'[dbo].[ProveedorRetencion_ObtenerPorProveedorID]'))
	DROP PROCEDURE [dbo].[ProveedorRetencion_ObtenerPorProveedorID]; 
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 10/03/2016 12:00:00 a.m.
-- Description: 
-- SpName     : [ProveedorRetencion_ObtenerPorProveedorID]
--======================================================
CREATE PROCEDURE [dbo].[ProveedorRetencion_ObtenerPorProveedorID]
@ProveedorID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		pr.ProveedorRetencionID,
		pro.ProveedorID,		
		pro.Descripcion AS Proveedor,
		pr.Activo,
		risr.RetencionID,
		risr.TipoRetencion,		
		i.IvaID,
		i.Descripcion As DescripcionIva
	FROM ProveedorRetencion pr
	inner join Proveedor pro on pr.ProveedorID = pro.ProveedorID
	left join Retencion risr on pr.RetencionID = risr.RetencionID 	
	left join Iva i on pr.IvaID = i.IvaID
	WHERE pr.ProveedorID = @ProveedorID
	SET NOCOUNT OFF;
END
GO

