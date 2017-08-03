USE [SIAP]
GO

IF EXISTS(SELECT ''
FROM SYS.OBJECTS
WHERE [OBJECT_ID] = OBJECT_ID(N'[dbo].[Animal_ObtenerAnimalPorAretes]'))
 DROP PROCEDURE [dbo].[Animal_ObtenerAnimalPorAretes]; 
GO

--======================================================
-- Author:		César Valdez
-- Create date: 04/12/2015
-- Description:	Obtiene un animal para verificar su existencia en el inventario historico
-- [Animal_ObtenerAnimalPorAretes] ''
--======================================================
CREATE PROCEDURE [dbo].[Animal_ObtenerAnimalPorAretes]
	@Arete VARCHAR(15),
	@AreteTestigo VARCHAR(15),
	@OrganizacionID INT
AS
BEGIN

	SELECT a.AnimalID,a.Arete,a.AreteMetalico,a.FechaCompra,a.TipoGanadoID,a.CalidadGanadoID,a.ClasificacionGanadoID,	a.PesoCompra,
		   a.OrganizacionIDEntrada,a.FolioEntrada,a.PesoLlegada,a.Paletas,a.CausaRechadoID,a.Venta,a.Cronico,a.Activo,a.FechaCreacion,
           a.UsuarioCreacionID, 0 AS Historico, am.AnimalMovimientoID, am.FechaMovimiento, tp.TipoMovimientoID, tp.Descripcion,
		   eg.FechaEntrada, org.Descripcion AS Origen,
		   /* Si es compra directa se va a la entrada por el proveedor */
		   COALESCE(
			CASE WHEN o.TipoOrganizacionID = 3
			THEN
				( SELECT TOP 1  
					CASE WHEN egcto.TieneCuenta = 1
						 THEN
							(SELECT Descripcion FROM CuentaSAP (NOLOCK) WHERE CuentaSAP = egcto.CuentaProvision)
						 ELSE 
							(SELECT Descripcion FROM Proveedor (NOLOCK) WHERE ProveedorID = egcto.ProveedorID)
						 END AS Proveedor
				   FROM EntradaGanadoCosteo egc (NOLOCK)
				   INNER JOIN EntradaGanadoCosto egcto (NOLOCK) ON egcto.EntradaGanadoCosteoID = egc.EntradaGanadoCosteoID
				   INNER JOIN Costo co ON co.CostoID = egcto.CostoID
				   WHERE 1 = 1
				     AND egc.EntradaGanadoID = eg.EntradaGanadoID
				   ORDER BY egcto.CostoID ASC
				) COLLATE DATABASE_DEFAULT
			ELSE
			/* De lo contrario se va a centros por el proveedor */
			    ( SELECT cp.Descripcion -- , cp.Nombre, cp.ApellidoPaterno, cp. ApellidoMaterno, a.Arete, a.AreteMetalico
				    FROM SuKarne.dbo.CatAnimal ca (NOLOCK)
				   INNER JOIN SuKarne.dbo.CatProveedor cp (NOLOCK) ON cp.OrganizacionId = ca.OrganizacionId AND cp.ProveedorID = ca.ProveedorId
				   LEFT JOIN siap.dbo.InterfaceSalidaAnimal isa (NOLOCK) ON ca.OrganizacionId = isa.OrganizacionID AND isa.AreteMetalico = ca.AreteMetalico COLLATE DATABASE_DEFAULT
				   WHERE tmp.AnimalID = isa.AnimalID) COLLATE DATABASE_DEFAULT
			END , '')  AS Proveedor
	  FROM(
			SELECT a.AnimalID, MAX(am.AnimalMovimientoID) AnimalMovimientoID
			  FROM Animal a (NOLOCK)
			 INNER JOIN AnimalMovimiento am (NOLOCK) ON am.AnimalID = a.AnimalID
			 WHERE ((Arete = @Arete AND @Arete != '') OR 
				   (AreteMetalico = @AreteTestigo AND @AreteTestigo != '')) 
			   AND OrganizacionIDEntrada = @OrganizacionID
			 GROUP BY a.AnimalID
		) AS tmp
	 INNER JOIN Animal a (NOLOCK) ON tmp.AnimalID = a.AnimalID
     INNER JOIN AnimalMovimiento am (NOLOCK) ON am.AnimalID = a.AnimalID AND am.AnimalMovimientoID = tmp.AnimalMovimientoID
	 INNER JOIN TipoMovimiento tp (NOLOCK) ON tp.TipoMovimientoID = am.TipoMovimientoID
	 INNER JOIN EntradaGanado eg (NOLOCK) ON eg.FolioEntrada = a.FolioEntrada AND eg.OrganizacionID = a.OrganizacionIDEntrada
	 LEFT JOIN InterfaceSalidaAnimal isa (NOLOCK) ON a.AnimalID = isa.AnimalID
	 INNER JOIN Organizacion o (NOLOCK) ON o.OrganizacionID = eg.OrganizacionOrigenID
	LEFT JOIN Organizacion org (NOLOCK) ON org.OrganizacionID = isa.OrganizacionID
	UNION ALL

	SELECT a.AnimalID,a.Arete,a.AreteMetalico,a.FechaCompra,a.TipoGanadoID,a.CalidadGanadoID,a.ClasificacionGanadoID,	a.PesoCompra,
		   a.OrganizacionIDEntrada,a.FolioEntrada,a.PesoLlegada,a.Paletas,a.CausaRechadoID,a.Venta,a.Cronico,a.Activo,a.FechaCreacion,
           a.UsuarioCreacionID, 1 AS Historico, am.AnimalMovimientoID, am.FechaMovimiento, tp.TipoMovimientoID, tp.Descripcion,
		   eg.FechaEntrada,  org.Descripcion AS Origen,
		   /* Si es compra directa se va a la entrada por el proveedor */
		   COALESCE(
			CASE WHEN o.TipoOrganizacionID = 3
			THEN
				( SELECT TOP 1  
					CASE WHEN egcto.TieneCuenta = 1
						 THEN
							(SELECT Descripcion FROM CuentaSAP (NOLOCK) WHERE CuentaSAP = egcto.CuentaProvision)
						 ELSE 
							(SELECT Descripcion FROM Proveedor (NOLOCK) WHERE ProveedorID = egcto.ProveedorID)
						 END AS Proveedor
				    FROM EntradaGanadoCosteo egc (NOLOCK)
				   INNER JOIN EntradaGanadoCosto egcto (NOLOCK) ON egcto.EntradaGanadoCosteoID = egc.EntradaGanadoCosteoID
				   INNER JOIN Costo co ON co.CostoID = egcto.CostoID
				   WHERE 1 = 1
				     AND egc.EntradaGanadoID = eg.EntradaGanadoID
				   ORDER BY egcto.CostoID ASC
				) COLLATE DATABASE_DEFAULT
			ELSE
			/* De lo contrario se va a centros por el proveedor */		    

(SELECT cp.Descripcion -- , cp.Nombre, cp.ApellidoPaterno, cp. ApellidoMaterno, a.Arete, a.AreteMetalico
				    FROM SuKarne.dbo.CatAnimal ca (NOLOCK)
				   INNER JOIN SuKarne.dbo.CatProveedor cp (NOLOCK) ON cp.OrganizacionId = ca.OrganizacionId AND cp.ProveedorID = ca.ProveedorId
				   LEFT JOIN siap.dbo.InterfaceSalidaAnimal isa (NOLOCK) ON ca.OrganizacionId = isa.OrganizacionID AND isa.AreteMetalico = ca.AreteMetalico COLLATE DATABASE_DEFAULT
				   WHERE tmp.AnimalID = isa.AnimalID
) COLLATE DATABASE_DEFAULT
			END , '')  AS Proveedor
	  FROM(
			SELECT a.AnimalID, MAX(am.AnimalMovimientoID) AnimalMovimientoID
			  FROM AnimalHistorico a (NOLOCK)
			 INNER JOIN AnimalMovimientoHistorico am (NOLOCK) ON am.AnimalID = a.AnimalID
			 WHERE ((Arete = @Arete AND @Arete != '') OR 
				   (AreteMetalico = @AreteTestigo AND @AreteTestigo != '')) 
			   AND OrganizacionIDEntrada = @OrganizacionID
			 GROUP BY a.AnimalID
		) AS tmp
	 INNER JOIN AnimalHistorico a (NOLOCK) ON tmp.AnimalID = a.AnimalID
     LEFT JOIN AnimalMovimientoHistorico am (NOLOCK) ON am.AnimalID = a.AnimalID AND am.AnimalMovimientoID = tmp.AnimalMovimientoID
	 INNER JOIN TipoMovimiento tp (NOLOCK) ON tp.TipoMovimientoID = am.TipoMovimientoID
	 INNER JOIN EntradaGanado eg (NOLOCK) ON eg.FolioEntrada = a.FolioEntrada AND eg.OrganizacionID = a.OrganizacionIDEntrada
	LEFT JOIN InterfaceSalidaAnimal isa (NOLOCK) ON a.AnimalID = isa.AnimalID
	 LEFT JOIN Organizacion o (NOLOCK) ON o.OrganizacionID = eg.OrganizacionOrigenID 
	LEFT JOIN Organizacion org (NOLOCK) ON org.OrganizacionID = isa.OrganizacionID
END