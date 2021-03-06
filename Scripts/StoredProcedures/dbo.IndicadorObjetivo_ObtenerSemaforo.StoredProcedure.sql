USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[IndicadorObjetivo_ObtenerSemaforo]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[IndicadorObjetivo_ObtenerSemaforo]
GO
/****** Object:  StoredProcedure [dbo].[IndicadorObjetivo_ObtenerSemaforo]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Gilberto Carranza
-- Create date: 2014/06/16
-- Description: Procedimiento almacenado para obtener el semaforo de indicador objetivo
-- IndicadorObjetivo_ObtenerSemaforo 0, 2, 1
--=============================================
CREATE PROCEDURE [dbo].[IndicadorObjetivo_ObtenerSemaforo]
@PedidoID INT,
@ProductoID INT,
@OrganizacionID INT
AS
BEGIN
	SET NOCOUNT ON;
		SELECT DISTINCT I.IndicadorID
			,  I.Descripcion AS Indicador
			,  ISNULL(IOb.Medicion,'') AS Medicion
			,  TOC.TipoObjetivoCalidadID
			,  TOC.Descripcion AS TipoObjetivoCalidad
			,  IOb.IndicadorObjetivoID
			,  IOb.ObjetivoMinimo
			,  IOb.ObjetivoMaximo
			,  IOb.Tolerancia
			,  CO.ColorObjetivoID
			,  CO.Tendencia
			,  CO.CodigoColor
			,  CO.Descripcion AS ColorDescripcion
			,  ISNULL(PD.PedidoDetalleID,0) AS PedidoDetalleID
		FROM Pedido P
		left JOIN PedidoDetalle PD
			ON (P.PedidoID = PD.PedidoID
				AND (@PedidoID IN (P.PedidoID,0))
				AND P.OrganizacionID = @OrganizacionID
				AND PD.ProductoID = @ProductoID)
		INNER JOIN Producto Pro
			ON (Pro.ProductoID = @ProductoID)
		INNER JOIN IndicadorProductoCalidad IPC
			ON ( IPC.ProductoID = @ProductoID
				AND Pro.ProductoID = IPC.ProductoID)
		INNER JOIN Indicador I
			ON (IPC.IndicadorID = I.IndicadorID)		
		INNER JOIN IndicadorObjetivo IOb
			ON (IPC.IndicadorProductoCalidadID = IOb.IndicadorProductoCalidadID
				AND P.OrganizacionID = IOb.OrganizacionID)
		INNER JOIN TipoObjetivoCalidad TOC
			ON (IOb.TipoObjetivoCalidadID = TOC.TipoObjetivoCalidadID)
		INNER JOIN ColorObjetivo CO
			ON (TOC.TipoObjetivoCalidadID = CO.TipoObjetivoCalidadID	
				AND CO.Activo = 1)
		ORDER BY I.Descripcion
	SET NOCOUNT OFF;
END

GO
