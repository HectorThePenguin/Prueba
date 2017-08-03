IF EXISTS(SELECT * FROM   sys.objects WHERE  [object_id] = Object_id(N'[dbo].[ReportePaseaProceso]'))
BEGIN
 DROP PROCEDURE [dbo].[ReportePaseaProceso]
END
GO

--======================================================
-- Author     : Carlos Cazarez Nuñez
-- Create date: 13/10/2015 12:00:00 a.m.
-- Description: 
-- SpName     : ReportePaseaProceso
--======================================================

CREATE PROCEDURE [dbo].[ReportePaseaProceso]
	@OrganizacionId AS int,
	@ValorActivo As int
	
	
AS
BEGIN
  -- routine body goes here, e.g.
  -- SELECT 'Navicat for SQL Server'
SELECT CAST(p.FolioPedido AS varchar) + '-' + CAST(pmp2.Ticket AS varchar) AS Ticket, pmp2.FechaSurtido, pmp2.PesoBruto - pmp2.PesoTara AS PesoNeto, pd.ProductoID, p2.Descripcion
FROM Pedido p 
INNER JOIN PedidoDetalle pd ON pd.PedidoID = p.PedidoID
INNER JOIN ProgramacionMateriaPrima pmp ON pmp.PedidoDetalleID = pd.PedidoDetalleID
INNER JOIN PesajeMateriaPrima pmp2 ON pmp2.ProgramacionMateriaPrimaID = pmp.ProgramacionMateriaPrimaID
INNER JOIN Producto p2 ON p2.ProductoID = pd.ProductoID
WHERE pmp2.AlmacenMovimientoOrigenID IS NULL AND pmp2.PesoBruto > 0 AND p.Activo = @ValorActivo
AND p.OrganizacionID = @OrganizacionId 
END