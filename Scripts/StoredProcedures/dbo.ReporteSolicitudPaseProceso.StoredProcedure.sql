USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ReporteSolicitudPaseProceso]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ReporteSolicitudPaseProceso]
GO
/****** Object:  StoredProcedure [dbo].[ReporteSolicitudPaseProceso]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gumaro Alberto Lugo D�az
-- Create date: 25/07/2014 13:00:00 p.m.
-- Description: Procedimiento almacenado para generar el reporte de Reporte Solicitud de Pase a Proceso
-- SpName     : ReporteSolicitudPaseProceso
--======================================================
CREATE PROCEDURE [dbo].[ReporteSolicitudPaseProceso]
   @OrganizacionID AS INT ,
   @Fecha DATETIME,
   @TipoAlmacen  INT,
   @Familia INT
AS
   SELECT   Codigo = AI.ProductoID, 
			P.Descripcion AS Ingrediente,
            CAST(AI.Cantidad AS DECIMAL)/CAST(1000 AS DECIMAL) AS TMExistenciaAlmacenPA, 
            ( SELECT CAST(SUM(CantidadProducto) AS decimal)/CAST(1000 AS DECIMAL)
              FROM   ProduccionFormulaDetalle PFD
              LEFT JOIN ProduccionFormula PF ON PF.ProduccionFormulaID = PFD.ProduccionFormulaID
              WHERE  PFD.ProductoID = AI.ProductoID AND CAST(PF.FechaProduccion AS DATE) = CAST(AM.FechaMovimiento AS DATE) AND PF.OrganizacionID = A.OrganizacionID )
            AS TMConsumoDia,
			CAST(AI.CapacidadAlmacenaje as decimal)/CAST(1000 AS DECIMAL) as TMCapacidadAlmacenaje,
			CAST((AI.CapacidadAlmacenaje - AI.Cantidad) as decimal)/CAST(1000 AS DECIMAL) as TMSugeridasSolicitar,
            ( SELECT CAST(SUM(PD.CantidadSolicitada) as decimal)/CAST(1000 AS DECIMAL)
              FROM   PedidoDetalle PD
              INNER JOIN Pedido Ped ON Ped.PedidoID = PD.PedidoID
              WHERE  PD.ProductoID = AI.ProductoID AND CAST(Ped.FechaPedido AS DATE) = CAST(AM.FechaMovimiento AS DATE) AND Ped.OrganizacionID = A.OrganizacionID )
            AS TMRequeridasProduccion,
			Fecha =  @Fecha
   FROM     AlmacenInventario AI
   INNER JOIN Producto P ON P.ProductoID = AI.ProductoID
   INNER JOIN SubFamilia SF ON SF.SubFamiliaID = P.SubFamiliaID
   INNER JOIN Almacen A ON A.AlmacenID = AI.AlmacenID
   INNER JOIN Organizacion O ON O.OrganizacionID = A.OrganizacionID
   INNER JOIN TipoAlmacen TA ON TA.TipoAlmacenID = A.TipoAlmacenID
   INNER JOIN AlmacenMovimientoDetalle AMD ON AMD.ProductoID = AI.ProductoID
   INNER JOIN AlmacenMovimiento AM ON AM.AlmacenMovimientoID = AMD.AlmacenMovimientoID
   WHERE    SF.FamiliaID = @Familia AND A.OrganizacionID = @OrganizacionID AND TA.TipoAlmacenID = @TipoAlmacen AND 
   CAST(AM.FechaMovimiento AS DATE) = CAST(@Fecha as DATE)
   GROUP BY A.OrganizacionID, AI.ProductoID, P.Descripcion, AI.Cantidad, AI.CapacidadAlmacenaje,
   CAST (AM.FechaMovimiento AS DATE)

GO
