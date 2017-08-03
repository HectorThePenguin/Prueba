IF EXISTS (
		SELECT *
		FROM sys.objects
		WHERE [object_id] = Object_id(N'[dbo].[ObtenerTipoGanado]')
		)
	DROP FUNCTION [dbo].[ObtenerTipoGanado]
GO

-- =============================================
-- Author		: José Gilberto Quintero López
-- Create date	: 02/04/2014
-- Description	: Obtiene el tipo de ganado
-- Origen		: Select * From dbo.ObtenerTipoGanado(1, 0, 5, '')
--				  Select * From dbo.ObtenerTipoGanado(1, 1, 5, 'M') 	
--=============================================
CREATE FUNCTION ObtenerTipoGanado (
	 @OrganizacionId INT
	,@LoteId INT
	,@TipoMovimientoID INT
	,@Sexo CHAR(1)
	)
RETURNS TABLE
AS
RETURN (
		SELECT tg.TipoGanadoID
			,tg.Descripcion
			,a.LoteID
			,a.Cabezas
			,a.PesoLote
			,a.PesoPromedio
			,tg.PesoMinimo
			,tg.PesoMaximo
			,tg.Sexo
		FROM (
			SELECT 
				 l.loteId
				,l.Cabezas
				,a.PesoLote
				,CASE 
					WHEN l.Cabezas = 0
						THEN 0
					ELSE (a.PesoLote / l.Cabezas)
					END AS [PesoPromedio]
			FROM (
				SELECT am.LoteId
					,am.TipoMovimientoID
					,SUM(a.PesoCompra) AS [PesoLote]
				FROM AnimalMovimiento am
				INNER JOIN Lote lo on am.LoteID = lo.LoteID
				INNER JOIN Animal A ON am.AnimalID = a.AnimalID
				WHERE am.OrganizacionID = @OrganizacionId
					AND @LoteId in (am.LoteID, 0)
					AND TipoMovimientoID = @TipoMovimientoID
					AND	lo.Activo = 1				
				GROUP BY am.OrganizacionID
					,am.LoteId
					,TipoMovimientoID
				) a
			INNER JOIN Lote l ON l.LoteID = a.LoteID
			) a
		INNER JOIN TipoGanado tg ON a.PesoPromedio BETWEEN tg.PesoMinimo
				AND tg.PesoMaximo
		WHERE @Sexo in (tg.Sexo,'')
		)
GO


