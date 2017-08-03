USE [SIAP]
GO

IF EXISTS(SELECT ''
FROM SYS.OBJECTS
WHERE [OBJECT_ID] = OBJECT_ID(N'[dbo].[AnimalMovimiento_ObtenerTrazabilidadAnimalMovimiento]'))
 DROP PROCEDURE [dbo].[AnimalMovimiento_ObtenerTrazabilidadAnimalMovimiento]; 
GO



-- =============================================
-- Autor:		César Valdez
-- Create date: 2015/12/04
-- Modification date: 2016/12/10
-- Description: SP para Obtener la trazabilidad de un animal
-- AnimalMovimiento_ObtenerTrazabilidadAnimalMovimiento
-- =============================================
CREATE PROCEDURE [dbo].[AnimalMovimiento_ObtenerTrazabilidadAnimalMovimiento] 
	@AnimalID BIGINT,
	@Historico BIT
AS
BEGIN
	/*Total de Animales en Corral*/
	DECLARE @Total INT = (
	SELECT COUNT(AnimalID) FROM SuKarne.dbo.CatAnimal
	WHERE CorralID = (SELECT TOP 1 CorralID 
						FROM SuKarne.dbo.CatAnimal ca INNER JOIN InterfaceSalidaAnimal isa ON AnimalIDAbasto=ca.AnimalID
						WHERE isa.AnimalID=@AnimalID)
	AND Activo = 1 
	GROUP BY CorralID )
	
		/* Si es 0 se busca en inventario normal de lo contrario se busca en historico*/
	IF ( @Historico = 0 )
		BEGIN
			/* Informacion De los movimientos del animal */
			SELECT am.AnimalMovimientoID, 
					am.AnimalID, 
					tm.TipoMovimientoID, 
					tm.Descripcion, 
					am.FechaMovimiento, 
					am.Peso,
					CASE COALESCE(co.CorralID,0) WHEN 0 THEN ceo.CorralID ELSE co.CorralID END AS 'CorralIDOrigen',
					CASE COALESCE(co.Codigo,'') WHEN '' THEN ceo.Codigo ELSE co.Codigo END AS 'CodigoOrigen',
					CASE COALESCE(am.LoteIDOrigen,0) WHEN 0 THEN leo.LoteID ELSE am.LoteIDOrigen END AS 'LoteIDOrigen',
					CASE COALESCE(lo.Lote,'') WHEN '' THEN leo.Lote ELSE lo.Lote END AS 'LoteOrigen',
					am.CorralID AS 'CorralIDDestino',
					cd.Codigo AS 'CodigoDestino',
					am.LoteID AS 'LoteIDDestino',
					ld.Lote AS 'LoteDestino',
					t.TrampaID,
					t.Descripcion AS DescripcionTrampa,
					t.HostName,
					u.UsuarioID,
					u.Nombre
			 FROM AnimalMovimiento am (NOLOCK)
			INNER JOIN Animal a (NOLOCK) ON a.AnimalID = am.AnimalID
			INNER JOIN EntradaGanado eg (NOLOCK) ON eg.FolioEntrada = a.FolioEntrada AND eg.OrganizacionID = a.OrganizacionIDEntrada
			INNER JOIN Lote leo (NOLOCK) ON leo.LoteID = eg.LoteID
			INNER JOIN Corral ceo (NOLOCK) ON ceo.CorralID = eg.CorralID
			INNER JOIN TipoMovimiento tm (NOLOCK) ON tm.TipoMovimientoID = am.TipoMovimientoID
			INNER JOIN Corral cd (NOLOCK) ON cd.CorralID = am.CorralID
			INNER JOIN Lote ld (NOLOCK) ON ld.LoteID = am.LoteID
			INNER JOIN Trampa t (NOLOCK) ON t.TrampaID = am.TrampaID
			INNER JOIN Usuario u (NOLOCK) ON u.UsuarioID = am.UsuarioCreacionID
			 LEFT JOIN Lote lo (NOLOCK) ON lo.LoteID = am.LoteIDOrigen
			 LEFT JOIN Corral co (NOLOCK) ON co.CorralID = lo.CorralID
			WHERE am.AnimalID = @AnimalID
			ORDER BY am.AnimalMovimientoID ASC;

			/* Se obtienen los costos del animal*/			
			SELECT ac.AnimalID, ac.CostoID, c.Descripcion, SUM(ac.Importe) AS Importe
			FROM AnimalCosto ac (NOLOCK) 
			INNER JOIN Costo c (NOLOCK) ON c.CostoID = ac.CostoID
			WHERE AnimalID = @AnimalID
			GROUP BY ac.AnimalID, ac.CostoID, c.Descripcion;
			
			/* Obtener el consumo de los animales */
			SELECT FormulaIDServida, f.Descripcion, COUNT(1) AS Dias, ROUND(SUM(Cantidad),0) As Kilos, ROUND((SUM(Cantidad)/COUNT(1)),2) AS Promedio
			  FROM(
				SELECT Aco.FormulaIDServida, Aco.Fecha, SUM(Aco.Cantidad) Cantidad
				  FROM AnimalConsumo Aco (NOLOCK) 
				 WHERE Aco.AnimalID =  @AnimalID
				 GROUP BY Aco.FormulaIDServida, Aco.Fecha
			  ) AS Consumos
			 INNER JOIN Formula f ON FormulaIDServida = f.FormulaID
			 GROUP BY FormulaIDServida, f.Descripcion
			 ORDER BY f.Descripcion ASC;
			 
		END
	ELSE
		BEGIN
			/* Informacion De los movimientos del animal */
			SELECT am.AnimalMovimientoID, 
					am.AnimalID, 
					tm.TipoMovimientoID, 
					tm.Descripcion, 
					am.FechaMovimiento, 
					am.Peso,
					CASE COALESCE(co.CorralID,0) WHEN 0 THEN ceo.CorralID ELSE co.CorralID END AS 'CorralIDOrigen',
					CASE COALESCE(co.Codigo,'') WHEN '' THEN ceo.Codigo ELSE co.Codigo END AS 'CodigoOrigen',
					CASE COALESCE(am.LoteIDOrigen,0) WHEN 0 THEN leo.LoteID ELSE am.LoteIDOrigen END AS 'LoteIDOrigen',
					CASE COALESCE(lo.Lote,'') WHEN '' THEN leo.Lote ELSE lo.Lote END AS 'LoteOrigen',
					am.CorralID AS 'CorralIDDestino',
					cd.Codigo AS 'CodigoDestino',
					am.LoteID AS 'LoteIDDestino',
					ld.Lote AS 'LoteDestino',
					t.TrampaID,
					t.Descripcion AS DescripcionTrampa,
					t.HostName,
					u.UsuarioID,
					u.Nombre
			 FROM AnimalMovimientoHistorico am (NOLOCK) 
			INNER JOIN AnimalHistorico a (NOLOCK) ON a.AnimalID = am.AnimalID
			INNER JOIN EntradaGanado eg (NOLOCK) ON eg.FolioEntrada = a.FolioEntrada AND eg.OrganizacionID = a.OrganizacionIDEntrada
			INNER JOIN Lote leo (NOLOCK) ON leo.LoteID = eg.LoteID
			INNER JOIN Corral ceo (NOLOCK) ON ceo.CorralID = eg.CorralID
			INNER JOIN TipoMovimiento tm (NOLOCK) ON tm.TipoMovimientoID = am.TipoMovimientoID
			INNER JOIN Corral cd (NOLOCK) ON cd.CorralID = am.CorralID
			INNER JOIN Lote ld (NOLOCK) ON ld.LoteID = am.LoteID
			INNER JOIN Trampa t (NOLOCK) ON t.TrampaID = am.TrampaID
			INNER JOIN Usuario u (NOLOCK) ON u.UsuarioID = am.UsuarioCreacionID
			 LEFT JOIN Lote lo (NOLOCK) ON lo.LoteID = am.LoteIDOrigen
			 LEFT JOIN Corral co (NOLOCK) ON co.CorralID = lo.CorralID
			WHERE am.AnimalID = @AnimalID
			ORDER BY am.AnimalMovimientoID ASC;
			
			/* Se obtienen los costos del animal*/			
			SELECT ac.AnimalID, ac.CostoID, c.Descripcion, SUM(ac.Importe) AS Importe
			FROM AnimalCostoHistorico ac (NOLOCK) 
			INNER JOIN Costo c (NOLOCK) ON c.CostoID = ac.CostoID
			WHERE AnimalID = @AnimalID
			GROUP BY ac.AnimalID, ac.CostoID, c.Descripcion;
			
			/* Obtener el consumo de los animales */
			SELECT FormulaIDServida, f.Descripcion, COUNT(1) AS Dias, ROUND(SUM(Cantidad),0) As Kilos, ROUND((SUM(Cantidad)/COUNT(1)),2) AS Promedio
			  FROM(
				SELECT Aco.FormulaIDServida, Aco.Fecha, SUM(Aco.Cantidad) Cantidad
				  FROM AnimalConsumoHistorico Aco (NOLOCK) 
				 WHERE Aco.AnimalID =  @AnimalID
				 GROUP BY Aco.FormulaIDServida, Aco.Fecha
			  ) AS Consumos
			 INNER JOIN Formula f ON FormulaIDServida = f.FormulaID
			 GROUP BY FormulaIDServida, f.Descripcion
			 ORDER BY f.Descripcion ASC;
			 
		END
			 
		/* Se obtienen los productos aplicados en cada movimiento */
		
	IF ( @Historico = 0 )
  	BEGIN 
		SELECT AMov.AnimalMovimientoID, p.ProductoID, p.Descripcion, AMD.Cantidad, um.ClaveUnidad, AMD.Importe
		  FROM AlmacenMovimiento AMov (NOLOCK) 
		 INNER JOIN AlmacenMovimientoDetalle AMD (NOLOCK) ON AMD.AlmacenMovimientoID = AMov.AlmacenMovimientoID
		 INNER JOIN Producto p (NOLOCK) ON p.ProductoID = AMD.ProductoID
		 INNER JOIN UnidadMedicion um (NOLOCK) ON um.UnidadID = p.UnidadID
		 WHERE AMov.AnimalMovimientoID IN (SELECT AnimalMovimientoID FROM AnimalMovimiento (NOLOCK) WHERE AnimalID = @AnimalID )
		 ORDER By AMov.AnimalMovimientoID ASC;
	END 
	ELSE BEGIN 
		SELECT AMov.AnimalMovimientoID, p.ProductoID, p.Descripcion, AMD.Cantidad, um.ClaveUnidad, AMD.Importe
		  FROM AlmacenMovimiento AMov (NOLOCK) 
		 INNER JOIN AlmacenMovimientoDetalle AMD (NOLOCK) ON AMD.AlmacenMovimientoID = AMov.AlmacenMovimientoID
		 INNER JOIN Producto p (NOLOCK) ON p.ProductoID = AMD.ProductoID
		 INNER JOIN UnidadMedicion um (NOLOCK) ON um.UnidadID = p.UnidadID
		 WHERE AMov.AnimalMovimientoID IN (SELECT AnimalMovimientoID FROM AnimalMovimientoHistorico (NOLOCK) WHERE AnimalID = @AnimalID )
		 ORDER By AMov.AnimalMovimientoID ASC;
	END
		 
		 /* Obtener los costos de abasto del animal*/
		SELECT  cac.AnimalID, cac.CostoID ,c.Descripcion, ROUND(SUM(cac.Importe),2) AS ImporteAbasto 
		FROM SuKarne.dbo.CacAnimalCosto cac (NOLOCK)
		INNER JOIN SIAP.dbo.Costo c (NOLOCK) ON (c.CostoID = cac.CostoID)
		INNER JOIN SIAP.dbo.InterfaceSalidaAnimal isa (NOLOCK) ON (cac.AnimalID = isa.AnimalIDAbasto)
		WHERE isa.AnimalID = @AnimalID
		GROUP BY cac.AnimalID, cac.CostoID,c.Descripcion;

		/* Obtener los consumo de abasto del animal*/
		SELECT (SUM(cac.Cantidad)/@Total) AS 'Cantidad', cp.Descripcion, DATEDIFF(DAY, cac.Fecha, GETDATE()) as 'Dias'
		FROM SuKarne.dbo.CacAlimentarCorral (NOLOCK) cac
		INNER JOIN SuKarne.dbo.CacAnimalCosto (NOLOCK) caco ON caco.FolioReferencia = cac.IdMovimiento
		INNER JOIN SuKarne.dbo.CatAnimal (NOLOCK) cta ON cac.CorralId = cta.CorralId
		INNER JOIN SuKarne.dbo.CatProducto (NOLOCK) cp ON cp.ProductoID = cac.ProductoID
		INNER JOIN SIAP.dbo.InterfaceSalidaAnimal isa (NOLOCK) ON (caco.AnimalID = isa.AnimalIDAbasto)
		WHERE caco.CostoID = 14 AND cta.Activo = 1 AND isa.AnimalID = @AnimalID 
		GROUP BY cp.ProductoID, cp.Descripcion, cac.Fecha
		 
END



