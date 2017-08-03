IF EXISTS (
		SELECT *
		FROM sys.objects
		WHERE [object_id] = Object_id(N'[dbo].[ObtenerFechaEntradaPromedioParaLote]')
		)
	DROP FUNCTION [dbo].[ObtenerFechaEntradaPromedioParaLote]
GO
-- =============================================
-- Author:    César Valdez
-- Create date: 20/05/2014
-- Description:  Obtener listado de partidas separadas por comas
-- select dbo.ObtenerFechaEntradaPromedioParaLote(12885) 
-- =============================================
CREATE FUNCTION [dbo].[ObtenerFechaEntradaPromedioParaLote] (
	@LoteID INT)
RETURNS DATE
AS
BEGIN
    DECLARE @DiasEngordaPromedio AS smalldatetime;
    	
    -- Se obtienen los dias engorda promedio para el lote en base a la fecha entrada de cada animal
    SELECT @DiasEngordaPromedio = CAST(AVG(CAST(EG.FechaEntrada AS float)) AS smalldatetime)
    FROM Lote L 
    INNER JOIN AnimalMovimiento AM on L.LoteID = AM.LoteID
    INNER JOIN Animal A ON A.AnimalID = AM.AnimalID
    INNER JOIN EntradaGanado EG ON EG.OrganizacionID = A.OrganizacionIDEntrada AND A.FolioEntrada = EG.FolioEntrada
    WHERE AM.Activo = 1
	   AND A.Activo = 1
        AND L.LoteID = @LoteID
    	
    RETURN @DiasEngordaPromedio
END
