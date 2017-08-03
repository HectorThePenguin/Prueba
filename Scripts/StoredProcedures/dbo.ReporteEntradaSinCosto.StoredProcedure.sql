IF EXISTS(SELECT * FROM   sys.objects WHERE  [object_id] = Object_id(N'[dbo].[ReporteEntradaSinCosto]'))
BEGIN
	DROP PROCEDURE [dbo].[ReporteEntradaSinCosto]
END
GO
-- =============================================
-- Author:    Juan Diego Valenzuela Rivera
-- Create date: 15/10/2015
-- Description:  Genera los datos para el reporte de Entradas Sin Costeo 
-- Origen: APInterfaces
-- EXEC ReporteEntradaSinCosto 4,20150401,20151015 idorganizacion,fechadesde,fechahasta
-- =============================================

CREATE PROCEDURE [dbo].[ReporteEntradaSinCosto]
		@organizacionid INT,
		@Activo int
AS
BEGIN
SELECT
	Folio,
	Fecha,
	p.Descripcion AS Producto,
	PesoBruto - PesoTara AS Peso,
	Observaciones,
	a.Descripcion AS AlmacenDestino,
	Lote AS LoteDestino,
	pr.Descripcion AS Proveedor,
	o.Descripcion AS Organizacion,
	o.OrganizacionID
FROM EntradaProducto ep

INNER JOIN Organizacion o on ep.OrganizacionID = o.OrganizacionID

INNER JOIN Producto p
	ON p.ProductoID = ep.ProductoID

INNER JOIN AlmacenInventarioLote ail
	ON ail.AlmacenInventarioLoteID = ep.AlmacenInventarioLoteID

INNER JOIN AlmacenInventario ai
	ON ai.AlmacenInventarioID = ail.AlmacenInventarioID

INNER JOIN RegistroVigilancia rv
	ON rv.RegistroVigilanciaID = ep.RegistroVigilanciaID

INNER JOIN Proveedor pr
	ON pr.ProveedorID = rv.ProveedorIDMateriasPrimas

INNER JOIN Almacen a
	ON a.AlmacenID = ai.AlmacenID

WHERE ep.AlmacenMovimientoID IS NULL
AND ep.PesoTara > 0
AND ep.Activo = @activo
AND ep.OrganizacionID=@organizacionid
END