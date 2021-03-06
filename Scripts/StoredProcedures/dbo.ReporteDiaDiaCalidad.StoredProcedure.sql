USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ReporteDiaDiaCalidad]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ReporteDiaDiaCalidad]
GO
/****** Object:  StoredProcedure [dbo].[ReporteDiaDiaCalidad]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gumaro Alberto Lugo
-- Create date: 03/07/2014 12:00:00 p.m.
-- Description:  Procedimiento para generar el reporte de dia a dia calidad
-- SpName     : ReporteDiaDiaCalidad
--======================================================
CREATE PROCEDURE [dbo].[ReporteDiaDiaCalidad]
   @OrganizacionID INT ,
   @Fecha DATE
AS -- Para tomar la informaci?n del reporte mediante la siguiente consulta:
   SELECT P.Descripcion as Producto, I.Descripcion AS Indicador, avg(CMP.Resultado) as Resultado, 
   dbo.obtenerResumenObservacionesCMP(@OrganizacionID,p.ProductoID,IP.IndicadorProductoID,@Fecha)  as Observaciones,
dbo.ObtenerRangoObjetivoCalidad(IOB.TipoObjetivoCalidadID) as RangoObjetivo
FROM CalidadMateriaPrima CMP
INNER JOIN PedidoDetalle PD ON CMP.PedidoDetalleID = PD.PedidoDetalleID
INNER JOIN Pedido Ped ON PD.PedidoID = Ped.PedidoID
INNER JOIN Producto P ON PD.ProductoID = P.ProductoID
INNER JOIN IndicadorProducto IP ON P.ProductoID = IP.ProductoID
INNER JOIN Indicador I on IP.IndicadorID = I.IndicadorID
INNER JOIN IndicadorProductoCalidad IPC ON IPC.IndicadorID = i.IndicadorID
INNER JOIN IndicadorObjetivo IOB ON IPC.IndicadorProductoCalidadID = IOB.IndicadorProductoCalidadID
WHERE CMP.IndicadorObjetivoID = IOB.IndicadorObjetivoID
 and Ped.Organizacionid = @OrganizacionID
 and cast(CMP.FechaCreacion AS date) = @Fecha
 group by P.Descripcion,P.ProductoID, I.Descripcion,I.IndicadorID,IP.IndicadorProductoID, IOB.TipoObjetivoCalidadID
 order by P.Descripcion, I.Descripcion

GO
