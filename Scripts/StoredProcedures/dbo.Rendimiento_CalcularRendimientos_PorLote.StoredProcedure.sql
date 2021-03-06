USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Rendimiento_CalcularRendimientos_PorLote]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Rendimiento_CalcularRendimientos_PorLote]
GO
/****** Object:  StoredProcedure [dbo].[Rendimiento_CalcularRendimientos_PorLote]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Jesus Garcia Reyes
-- Create date: <18 Ago 2014>
-- Description: Calcula los rendimientos del cierre
-- Rendimiento_CalcularRendimientos '2014-09-05', 1
-- =============================================
-- =============================================
-- Author: Cesar Fernando Vega Vazquez
-- Create date: <25 Mar 2015>
-- Description: Optimización de Proceso
-- Rendimiento_CalcularRendimientos '2015-03-29', 1
-- =============================================
-- =============================================
-- Author: Cesar Fernando Vega Vazquez
-- Create date: <01 Abr 2015>
-- Description: Se quita la eliminacion de las tablas transaccionales
-- Rendimiento_CalcularRendimientos '2015-03-29', 1
-- Rendimiento_CalcularRendimientos '2015-04-22', 1, 2
-- =============================================

CREATE PROCEDURE [dbo].[Rendimiento_CalcularRendimientos_PorLote]
	@fechaSacrificio datetime
	, @lote int
	, @ejecutar int = 2
AS
BEGIN
	SET NOCOUNT ON;

	declare @organizacion int

	select @organizacion = OrganizacionID from Lote where LoteID = @lote

	--DECLARE @fechaSacrificio datetime
	--DECLARE @organizacion int
	--DECLARE @ejecutar int = 0

	--SET @fechaSacrificio = '2015-04-11' --COLOCAR LA FECHA DEL SACRIFICIO
	--SET @organizacion = 5				--COLOCAR EL NUMERO DE ORGANIZACION  1 = CULIACAN, 2 = MEXICALI

	IF @ejecutar IN (0, 1, 2, 3)
	BEGIN

		DECLARE @Clave VARCHAR(MAX), @ClaveSalidaSacrificio VARCHAR(MAX), @TipoMovimientoSalidaSacrificio int
		SET @Clave = 'AjustePesoSalida'
		SET @ClaveSalidaSacrificio = 'Salida por sacrificio'

		SELECT @TipoMovimientoSalidaSacrificio = TipoMovimientoID FROM TipoMovimiento (NOLOCK) WHERE Descripcion = @ClaveSalidaSacrificio AND Activo = 1

		CREATE TABLE #Rendimientos (
			OrganizacionID int not null
			, AnimalID bigint not null
			, Corral char(10) not null
			, Lote varchar(10) not null
			, Arete varchar(15) not null
			, FechaSalida char(10) not null
			, PesoSalida int not null
			, PesoNoqueo int not null
			, PesoCanal int not null
			, PesoPiel int not null
			, LoteOrigenID int not null
			, AnimalMovimientoID int not null
			, TipoMovimientoID int
			, FechaSacrificio datetime
		)
	
		update LayoutSubidaRendimientos set Arete = Cast(Arete as bigint) 

		IF @organizacion = 5 
		BEGIN
			SELECT 
				ls.LoteSacrificioID
				, l.LoteID
				, lsd.AnimalID 
			INTO 
				#animales_luc
			FROM 
				LoteSacrificioLucero ls
				INNER JOIN LoteSacrificioLuceroDetalle lsd on
					ls.LoteSacrificioID = lsd.LoteSacrificioID
				INNER JOIN Lote l on
					ls.LoteID = l.LoteID
				INNER JOIN InterfaceSalidaTraspasoDetalle istd on
					ls.InterfaceSalidaTraspasoDetalleID = istd.InterfaceSalidaTraspasoDetalleID
				INNER JOIN InterfaceSalidaTraspaso ist on
					istd.InterfaceSalidaTraspasoID = ist.InterfaceSalidaTraspasoID
			WHERE
				DATEADD(d,0,DATEDIFF(d,0,ls.Fecha)) = @fechaSacrificio
				AND ls.LoteID = @lote

			INSERT INTO #Rendimientos
			SELECT 
				los.OrganizacionID
				, a.AnimalID
				, c.Codigo AS Corral
				, l.Lote AS Lote
				, CAST(los.Arete AS bigint) AS Arete
				, los.FechaSalida
				, CAST(ROUND(los.PesoNoqueo * (1 + (CAST(ISNULL(po.Valor, '0') AS decimal) / 100)), 0) AS int) AS PesoSalida
				, CAST(los.PesoNoqueo AS int) AS PesoNoqueo
				, CAST(ROUND(los.PesoCanal, 0) AS int) AS PesoCanal
				, CAST(ROUND(los.PesoPiel, 0) AS int) AS PesoPiel
				, 0 AS LoteOrigenID
				, 0 AS AnimalMovimientoID
				, @TipoMovimientoSalidaSacrificio
				, @fechaSacrificio
			FROM
				#animales_luc t
				INNER JOIN Animal (nolock) a 
					ON t.AnimalID = a.AnimalID
				INNER JOIN AnimalMovimiento (nolock) am 
					ON am.AnimalID = a.AnimalID AND am.Activo = 1 
				INNER JOIN Corral (nolock) c 
					ON c.CorralID = am.CorralID
				INNER JOIN Lote (nolock) l
					ON l.LoteID = am.LoteID 
				INNER JOIN LayoutSubidaRendimientos (nolock) los 
					ON a.Arete = los.Arete AND am.OrganizacionID = los.OrganizacionID
				LEFT JOIN Parametro (nolock) p
					ON p.Clave = @Clave
				LEFT JOIN ParametroOrganizacion (nolock) po
					ON po.ParametroID = p.ParametroID AND po.OrganizacionID = am.OrganizacionID
			WHERE 
				los.FechaSalida = @fechaSacrificio
		END
		ELSE
		BEGIN

			SELECT 
				ls.LoteSacrificioID
				, l.LoteID
				, lsd.AnimalID 
			INTO 
				#animales
			FROM 
				LoteSacrificio ls
				INNER JOIN LoteSacrificioDetalle lsd on
					ls.LoteSacrificioID = lsd.LoteSacrificioID
				INNER JOIN Lote l on
					ls.LoteID = l.LoteID
			WHERE
				l.OrganizacionID = @organizacion
				AND ls.LoteID = @lote
				AND DATEADD(d,0,DATEDIFF(d,0,ls.Fecha)) = @fechaSacrificio

			INSERT INTO #Rendimientos
			SELECT 
				los.OrganizacionID
				, a.AnimalID
				, c.Codigo AS Corral
				, l.Lote AS Lote
				, CAST(los.Arete AS bigint) AS Arete
				, los.FechaSalida
				, CAST(ROUND(los.PesoNoqueo * (1 + (CAST(ISNULL(po.Valor, '0') AS decimal) / 100)), 0) AS int) AS PesoSalida
				, CAST(los.PesoNoqueo AS int) AS PesoNoqueo
				, CAST(ROUND(los.PesoCanal, 0) AS int) AS PesoCanal
				, CAST(ROUND(los.PesoPiel, 0) AS int) AS PesoPiel
				, 0 AS LoteOrigenID
				, 0 AS AnimalMovimientoID
				, @TipoMovimientoSalidaSacrificio
				, @fechaSacrificio
			FROM
				#animales t
				INNER JOIN Animal (nolock) a 
					ON t.AnimalID = a.AnimalID
				INNER JOIN AnimalMovimiento (nolock) am 
					ON am.AnimalID = a.AnimalID AND am.Activo = 1 
				INNER JOIN Corral (nolock) c 
					ON c.CorralID = am.CorralID
				INNER JOIN Lote (nolock) l
					ON l.LoteID = am.LoteID 
				INNER JOIN LayoutSubidaRendimientos (nolock) los 
					ON a.Arete = los.Arete AND am.OrganizacionID = los.OrganizacionID
				LEFT JOIN Parametro (nolock) p
					ON p.Clave = @Clave
				LEFT JOIN ParametroOrganizacion (nolock) po
					ON po.ParametroID = p.ParametroID AND po.OrganizacionID = am.OrganizacionID
			WHERE 
				los.FechaSalida = @fechaSacrificio
		END
	END

	IF @ejecutar = 0
	BEGIN
		UPDATE r SET AnimalMovimientoID = am.AnimalMovimientoID, LoteOrigenID = am.LoteID
		FROM #Rendimientos r 
		INNER JOIN AnimalMovimiento am 
			ON am.AnimalID = r.AnimalID AND am.Activo = 1

		UPDATE am SET Activo = 0
		FROM #Rendimientos r 
		INNER JOIN AnimalMovimiento am 
			ON am.AnimalID = r.AnimalID AND am.Activo = 1 and am.TipoMovimientoID != r.TipoMovimientoID

		INSERT INTO AnimalMovimiento (
			AnimalID, OrganizacionID, CorralID, LoteID, FechaMovimiento, Peso, Temperatura, TipoMovimientoID,
			TrampaID, OperadorID, Observaciones, Activo, FechaCreacion, UsuarioCreacionID, LoteIDOrigen, AnimalMovimientoIDAnterior)
		SELECT	r.AnimalID, r.OrganizacionID, c.CorralID, r.LoteOrigenID, FechaSacrificio, r.PesoSalida, 0, r.TipoMovimientoID, 
				1, 1, 'Salida por Sacrificio del ' + convert(varchar(10), FechaSacrificio, 112), 1, GETDATE(), 1, r.LoteOrigenID, r.AnimalMovimientoID
		FROM #Rendimientos r 
		INNER JOIN Corral (nolock) c 
			ON c.OrganizacionID = r.OrganizacionID AND c.Codigo = r.Corral
		LEFT OUTER JOIN AnimalMovimiento am
			ON r.AnimalID = am.AnimalID and r.TipoMovimientoID = am.TipoMovimientoID
		WHERE
			am.AnimalID is null

		BEGIN TRY

			INSERT INTO AnimalHistorico (AnimalID, Arete, AreteMetalico, FechaCompra, TipoGanadoID, CalidadGanadoID, ClasificacionGanadoID, PesoCompra, OrganizacionIDEntrada,
			 FolioEntrada, PesoLlegada, Paletas, CausaRechadoID, Venta, Cronico, PesoNoqueo, PesoCanal, PesoPiel, Activo, FechaCreacion, UsuarioCreacionID,
			 FechaModificacion, UsuarioModificacionID)
			SELECT a.AnimalID, a.Arete, a.AreteMetalico, a.FechaCompra, a.TipoGanadoID, a.CalidadGanadoID, a.ClasificacionGanadoID, a.PesoCompra, a.OrganizacionIDEntrada,
			 a.FolioEntrada, a.PesoLlegada, a.Paletas, a.CausaRechadoID, a.Venta, a.Cronico, r.PesoNoqueo, r.PesoCanal, r.PesoPiel, a.Activo, a.FechaCreacion,
			 a.UsuarioCreacionID, a.FechaModificacion, a.UsuarioModificacionID
			FROM #Rendimientos r 
			INNER JOIN Animal (nolock) a
				ON r.AnimalID = a.AnimalID
			LEFT OUTER JOIN AnimalHistorico (nolock) h
				ON a.AnimalID = h.AnimalID
			WHERE
				h.AnimalID IS NULL
		
			INSERT INTO AnimalMovimientoHistorico (AnimalID, AnimalMovimientoID, OrganizacionID, CorralID, LoteID, FechaMovimiento, Peso, Temperatura, TipoMovimientoID,
			 TrampaID, OperadorID, Observaciones, Activo, FechaCreacion, UsuarioCreacionID, FechaModificacion, UsuarioModificacionID, LoteIDOrigen, AnimalMovimientoIDAnterior)
			SELECT am.AnimalID, am.AnimalMovimientoID, am.OrganizacionID, am.CorralID, am.LoteID, am.FechaMovimiento, am.Peso, am.Temperatura, am.TipoMovimientoID,
			 am.TrampaID, am.OperadorID, am.Observaciones, am.Activo, am.FechaCreacion, am.UsuarioCreacionID, am.FechaModificacion, am.UsuarioModificacionID,
			 am.LoteIDOrigen, am.AnimalMovimientoIDAnterior
			FROM #Rendimientos r 
			INNER JOIN AnimalMovimiento (nolock) am
				ON am.AnimalID = r.AnimalID
			LEFT OUTER JOIN AnimalMovimientoHistorico (nolock) h
				ON am.AnimalMovimientoID = h.AnimalMovimientoID
			WHERE
				h.AnimalMovimientoID IS NULL

			INSERT INTO AnimalCostoHistorico (AnimalCostoID, AnimalID, FechaCosto, CostoID, FolioReferencia, Importe, FechaCreacion, UsuarioCreacionID, FechaModificacion,
			UsuarioModificacionID,TipoReferencia)
			SELECT ac.AnimalCostoID, ac.AnimalID, ac.FechaCosto, ac.CostoID, ac.FolioReferencia, ac.Importe, ac.FechaCreacion, ac.UsuarioCreacionID, ac.FechaModificacion,
			ac.UsuarioModificacionID, ac.TipoReferencia
			FROM #Rendimientos r 
			INNER JOIN AnimalCosto (nolock) ac
				ON ac.AnimalID = r.AnimalID
			LEFT OUTER JOIN AnimalCostoHistorico (nolock) h
				ON ac.AnimalCostoID = h.AnimalCostoID
			WHERE
				h.AnimalCostoID IS NULL

			DECLARE @Lotes Table (
				Corral VARCHAR(10)
				, Lote VARCHAR(10)
				, OrganizacionId INT
				, FechaSacrificio DateTime
				, Cabezas INT
			)
	
			INSERT INTO @Lotes
				SELECT 
					Corral
					, Lote
					, OrganizacionId
					, FechaSacrificio
					, COUNT('') Cabezas
				FROM
					InterfazSPI (nolock) 
				WHERE
					Procesado = 1
					AND OrganizacionId = @organizacion
					AND FechaSacrificio = @fechaSacrificio
				GROUP BY
					Corral
					, Lote
					, OrganizacionId
					, FechaSacrificio

			UPDATE
				l
			SET
				Cabezas = l.Cabezas - i.Cabezas
			FROM
				Lote l
				INNER JOIN @Lotes i ON
					l.Lote = i.Lote
					AND l.OrganizacionID = i.OrganizacionId
	
			UPDATE
				l
			SET
				Activo = 0
			FROM
				Lote l
				INNER JOIN @Lotes i ON
					l.Lote = i.Lote
					AND l.OrganizacionID = i.OrganizacionId
			WHERE
				l.Cabezas <= 0

			UPDATE
				a 
			SET 
				Activo = 0 
			FROM #Rendimientos r 
				INNER JOIN Animal a ON r.AnimalID = a.AnimalID

			SELECT 'OK'

		END TRY
		BEGIN CATCH

			DECLARE @MeSaGe VARCHAR(MAX)

			SELECT @MeSaGe = ERROR_MESSAGE() 

			RAISERROR (@MeSaGe, 16, 1)

		END CATCH

		DROP TABLE #Rendimientos
	END

	IF @ejecutar = 1
	BEGIN
		BEGIN TRY
			INSERT INTO AnimalConsumoHistorico (AnimalConsumoID, AnimalID, RepartoID, FormulaIDServida, Cantidad, TipoServicioID, Fecha
			, Activo, FechaCreacion	, UsuarioCreacionID, FechaModificacion, UsuarioModificacionID)
			SELECT ac.AnimalConsumoID, ac.AnimalID, ac.RepartoID, ac.FormulaIDServida, ac.Cantidad, ac.TipoServicioID, ac.Fecha, ac.Activo
			, ac.FechaCreacion, ac.UsuarioCreacionID, ac.FechaModificacion, ac.UsuarioModificacionID
			FROM #Rendimientos r 
			INNER JOIN AnimalConsumo (nolock) ac
				ON ac.AnimalID = r.AnimalID
			LEFT OUTER JOIN AnimalConsumoHistorico (nolock) h
				ON ac.AnimalConsumoID = h.AnimalConsumoID
			WHERE
				h.AnimalConsumoID IS NULL

			SELECT 'OK'

		END TRY
		BEGIN CATCH

			DECLARE @MeSaGe1 VARCHAR(MAX)

			SELECT @MeSaGe1 = ERROR_MESSAGE() 

			RAISERROR (@MeSaGe1, 16, 1)

		END CATCH

		DROP TABLE #Rendimientos
	END

	IF @ejecutar = 2
	BEGIN
		Select * from #Rendimientos
	END

	IF @ejecutar = 3
	BEGIN

		Delete a
		from AnimalConsumoHistorico a
			inner join #Rendimientos r on a.AnimalID = r.AnimalID

		Delete a
		from AnimalCostoHistorico a
			inner join #Rendimientos r on a.AnimalID = r.AnimalID

		Delete a
		from AnimalMovimientoHistorico a
			inner join #Rendimientos r on a.AnimalID = r.AnimalID

		Delete a
		from AnimalHistorico a
			inner join #Rendimientos r on a.AnimalID = r.AnimalID
	END
END






GO
