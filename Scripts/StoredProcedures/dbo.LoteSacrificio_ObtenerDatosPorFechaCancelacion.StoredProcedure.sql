USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[LoteSacrificio_ObtenerDatosPorFechaCancelacion]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[LoteSacrificio_ObtenerDatosPorFechaCancelacion]
GO
/****** Object:  StoredProcedure [dbo].[LoteSacrificio_ObtenerDatosPorFechaCancelacion]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--======================================================
-- Author     : Ramses Santos
-- Create date: 16/08/2014
-- Description: Obtiene los datos de sacrificio del dia.
-- SpName     : LoteSacrificio_ObtenerDatosPorFechaCancelacion '2015-04-20'
--======================================================
CREATE PROCEDURE [dbo].[LoteSacrificio_ObtenerDatosPorFechaCancelacion]
@Fecha DATE
AS
BEGIN
	SET NOCOUNT ON

		SELECT *
		FROM
		( 
			SELECT LS.LoteID
				,  LS.Fecha
				,  LS.ImporteCanal		AS	ImporteCanal
				,  LS.ImportePiel		AS	ImportePiel
				,  LS.ImporteVisera		AS	ImporteViscera
				,  LS.Serie
				,  LS.Folio
				,  L.Lote
				,  L.OrganizacionID
				,  0					AS	InterfaceSalidaTraspasoDetalleID
				,  COUNT(LSD.AnimalID)	AS	Canales
				,  LS.PesoCanal			AS	Peso
				,  LS.PesoPiel			AS	PesoPiel
				,  LS.SerieFolio
				,  LS.PolizaGenerada
				,  ''					AS  Corral
			FROM LoteSacrificio LS
			INNER JOIN LoteSacrificioDetalle LSD
				ON (LS.LoteSacrificioID = LSD.LoteSacrificioID)
			INNER JOIN Lote L
				ON (LS.LoteID = L.LoteID)
			WHERE --CAST(LS.Fecha AS DATE) = @Fecha
				--AND 
				LS.ImporteCanal > 0
				AND LS.Serie IS NULL
				AND LS.Folio IS NULL
				AND LS.PolizaGenerada = 1
			GROUP BY LS.LoteID
				,  LS.Fecha
				,  LS.ImporteCanal
				,  LS.ImportePiel
				,  LS.ImporteVisera
				,  LS.Serie
				,  LS.Folio
				,  L.Lote
				,  L.OrganizacionID
				,  LS.PesoCanal
				,  LS.PesoPiel
				,  LS.SerieFolio
				,  LS.PolizaGenerada
			UNION
			SELECT LS.LoteID
				,  LS.Fecha
				,  LS.ImporteCanal		AS ImporteCanal
				,  LS.ImportePiel		AS ImportePiel
				,  LS.ImporteVisera		AS ImporteViscera
				,  LS.Serie
				,  LS.Folio
				,  L.Lote
				,  L.OrganizacionID
				,  LS.InterfaceSalidaTraspasoDetalleID
				,  COUNT(LSD.AnimalID)	AS Canales
				,  LS.PesoCanal			AS Peso
				,  LS.PesoPiel			AS PesoPiel
				,  LS.SerieFolio
				,  LS.PolizaGenerada
				,  ''					AS  Corral
			FROM LoteSacrificioLucero LS
			INNER JOIN Lote L
				ON (LS.LoteID = L.LoteID)
			INNER JOIN LoteSacrificioLuceroDetalle LSD
				ON (LS.LoteSacrificioID = LSD.LoteSacrificioID)
			WHERE --CAST(LS.Fecha AS DATE) = @Fecha
				--AND 
				LS.ImporteCanal > 0
				AND LS.Serie IS NULL
				AND LS.Folio IS NULL
				AND LS.PolizaGenerada = 1
			GROUP BY LS.LoteID
				,  LS.Fecha
				,  LS.ImporteCanal
				,  LS.ImportePiel
				,  LS.ImporteVisera
				,  LS.Serie
				,  LS.Folio
				,  L.Lote
				,  L.OrganizacionID
				,  LS.InterfaceSalidaTraspasoDetalleID
				,  LS.PesoCanal
				,  LS.PesoPiel
				,  LS.SerieFolio
				,  LS.PolizaGenerada
		) A
		ORDER BY Folio

	SET NOCOUNT OFF
END

GO
