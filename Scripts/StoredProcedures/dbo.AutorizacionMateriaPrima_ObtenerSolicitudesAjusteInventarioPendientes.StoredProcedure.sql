USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AutorizacionMateriaPrima_ObtenerSolicitudesAjusteInventarioPendientes]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AutorizacionMateriaPrima_ObtenerSolicitudesAjusteInventarioPendientes]
GO
/****** Object:  StoredProcedure [dbo].[AutorizacionMateriaPrima_ObtenerSolicitudesAjusteInventarioPendientes]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Emir Lezama
-- Create date: 08/12/2014
-- Description: Sp para obtener las solicitudes pendientes del tipo movimiento ajuste de inventario
-- AutorizacionMateriaPrima_ObtenerSolicitudesAjusteInventarioPendientes 1,3,41,31,32,50,1
--=============================================
CREATE PROCEDURE [dbo].[AutorizacionMateriaPrima_ObtenerSolicitudesAjusteInventarioPendientes]
@OrganizacionID INT,
@TipoAutorizacionID INT,
@DifInvPendiente INT,
@EntradaID INT,
@SalidaID INT,
@EstatusID INT,
@Activo BIT
AS
BEGIN
	SET NOCOUNT ON;

	/*		Ajute de Inventario		*/
	DECLARE @Solicitudes AS TABLE
	(
		Folio BIGINT,
		AutorizacionMateriaPrimaID INT,
		AlmacenMovimientoID BIGINT,
		DesProducto VARCHAR(100),
		DesAlmacen VARCHAR(50),
		Lote INT,
		CostoUnitario DECIMAL(18,4),
		CantidadAjuste DECIMAL(18,2),
		Justificacion VARCHAR(250),
		AlmacenInventarioLoteID INT
	)

	declare @TamaniosLote AS TABLE
	(
		AlmacenInventarioLoteID INT,
		TamanioLote DECIMAL(18,2)
	)

	INSERT @Solicitudes(
		Folio,
		AutorizacionMateriaPrimaID,
		AlmacenMovimientoID,
		DesProducto,
		DesAlmacen,
		Lote,
		CostoUnitario,
		CantidadAjuste,
		Justificacion,
		AlmacenInventarioLoteID
	)
	SELECT 
	AMP.Folio,
	AMP.AutorizacionMateriaPrimaID,
	AM.AlmacenMovimientoID,
	P.Descripcion AS DesProducto, 
	A.Descripcion AS DesAlmacen, 
	AMP.Lote AS Lote,
	AMP.Precio AS CostoUnitario,
	CASE WHEN AM.TipoMovimientoID = @EntradaID 
		THEN AMP.Cantidad * -1
		ELSE AMP.Cantidad 
	END AS CantidadAjuste,
	AMP.Justificacion,
	ail.AlmacenInventarioLoteID
	FROM AutorizacionMateriaPrima (NOLOCK) AS AMP 
	INNER JOIN AlmacenMovimiento (NOLOCK) AS AM ON AM.FolioMovimiento = AMP.Folio AND AM.AlmacenID = AMP.AlmacenID 
													AND AM.TipoMovimientoID IN (@EntradaID, @SalidaID) AND AM.Status=@DifInvPendiente
	INNER JOIN AlmacenMovimientoDetalle amd (nolock) on am.AlmacenMovimientoID = amd.AlmacenMovimientoID and amd.ProductoID = amp.ProductoID
	left join AlmacenInventarioLote ail (nolock) on amd.AlmacenInventarioLoteID = ail.AlmacenInventarioLoteID and ail.Lote = amp.Lote
	INNER JOIN Producto (NOLOCK) AS P ON AMP.ProductoID = P.ProductoID
	INNER JOIN Almacen (NOLOCK) AS A  ON AMP.AlmacenID = A.AlmacenID
	WHERE AMP.TipoAutorizacionID = @TipoAutorizacionID 
	AND AMP.EstatusID = @EstatusID 
	AND AMP.Activo = @Activo 
	AND AMP.OrganizacionID = @OrganizacionID 

	insert into @TamaniosLote
	select 
	amd.AlmacenInventarioLoteID
	,sum(amd.Cantidad) TamanioLote
	from AlmacenMovimiento am (NOLOCK)
	inner join AlmacenMovimientoDetalle amd (nolock) on am.AlmacenMovimientoID = amd.AlmacenMovimientoID
	inner join @Solicitudes so on amd.AlmacenInventarioLoteID = so.AlmacenInventarioLoteID
	inner join TipoMovimiento tm on am.TipoMovimientoID = tm.TipoMovimientoID
	where tm.EsEntrada = 1
	and tm.TipoMovimientoID <> @EntradaID
	and am.Status = 21 --Movimiento Aplicados
	group by amd.AlmacenInventarioLoteID


	SELECT 
	SP.Folio,
	SP.AutorizacionMateriaPrimaID,
	SP.DesProducto,
	SP.AlmacenMovimientoID,
	SP.DesAlmacen,
	SP.Lote,
	SP.CostoUnitario,
	SP.CantidadAjuste,
	CAST(((AMD.Cantidad/ 
	CASE WHEN tl.TamanioLote = 0 
	THEN 1 
	when tl.TamanioLote is NULL
	then 1
	else tl.TamanioLote end) * 100) AS DECIMAL(18,3))AS PorcentajeAjuste,
	SP.Justificacion
	FROM @Solicitudes AS SP
	INNER JOIN 
	AlmacenMovimientoDetalle AS AMD 
	ON SP.AlmacenMovimientoID = AMD.AlmacenMovimientoID
	INNER JOIN AlmacenInventarioLote AS AIL
	ON AMD.AlmacenInventarioLoteID = AIL.AlmacenInventarioLoteID
	left join @TamaniosLote tl on sp.AlmacenInventarioLoteID = tl.AlmacenInventarioLoteID

	SET NOCOUNT OFF;
END
GO
