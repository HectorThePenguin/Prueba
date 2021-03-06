IF EXISTS (SELECT
	''
FROM sys.objects
WHERE [object_id] = OBJECT_ID(N'[dbo].[ReporteDetalleCorte]')) DROP PROCEDURE [dbo].[ReporteDetalleCorte];
GO
-- =============================================
-- Author: Jose Luis Urias Roman
-- Create date: 23/07/2014
-- Description: Obtiene los datos para reporte de Detalle del Corte
-- ReporteDetalleCorte  '20140901', '20141010', 1, 5, 13
--001 Jorge Luis Velazquez Araujo 22/04/2015 *se agrega el Tipo Movimiento 13 (Corte por Transferencia)
-- =============================================
CREATE PROCEDURE ReporteDetalleCorte
(
	@RangoFechasInicialSeleccionado DATE,
	@RangoFechasFinalSeleccionado DATE,
	@OrganizacionSeleccionada INT,
	@IdUsuario INT,
	@TipoMovimientoID INT
)
AS

declare @TipoMovimientoCorteTransferencia int = 13

SELECT
	a.Arete,
	tp.Descripcion,
	eg.FolioEntrada,
	c2.Codigo AS CorralOrigen,
	c.Codigo AS CorralDestino,
	a.pesocompra AS PesoOrigen,
	am.Peso AS PesoCorte,
	COALESCE(CAST(((a.PesoCompra - am.Peso) / CAST(NULLIF(a.PesoCompra, 0) AS DECIMAL) * 100) AS DECIMAL(10, 2)), 0) AS Merma,
	am.Temperatura,
	UPPER(Org.Division) AS Nomenclatura,
	CONVERT(VARCHAR(10), @RangoFechasInicialSeleccionado, 103) AS FecInicial,
	CONVERT(VARCHAR(10), @RangoFechasFinalSeleccionado, 103) AS FecFinal,
	UPPER(Org.Division) AS Nomenclatura
FROM animalmovimiento am
INNER JOIN animal a
	ON a.animalid = am.animalid
INNER JOIN TipoGanado tp
	ON tp.tipoganadoid = a.tipoganadoid
INNER JOIN Corral c
	ON am.corralid = c.corralid
INNER JOIN EntradaGanado eg
	ON eg.organizacionid = a.OrganizacionIDEntrada
	AND eg.folioentrada = a.folioentrada
INNER JOIN lote l
	ON l.loteid = eg.loteid
INNER JOIN corral c2
	ON c2.corralid = l.corralid
INNER JOIN usuario Us
	ON Us.UsuarioID = @IdUsuario
INNER JOIN Organizacion Org
	ON Org.OrganizacionID = Us.OrganizacionID
WHERE am.tipomovimientoid in (@TipoMovimientoID,@TipoMovimientoCorteTransferencia)
AND c2.Codigo <> 'ZZZ'
AND c.Codigo <> 'ZZZ'
AND CONVERT(CHAR(8), am.FechaMovimiento, 112)
BETWEEN CONVERT(CHAR(8), @RangoFechasInicialSeleccionado, 112) AND
CONVERT(CHAR(8), @RangoFechasFinalSeleccionado, 112)
AND eg.Organizacionid = @OrganizacionSeleccionada