 IF EXISTS(SELECT *
      FROM   sys.objects
      WHERE  [object_id] = Object_id(N'[dbo].[ObtenerTipoGanadoFactura]'))
		DROP FUNCTION [dbo].[ObtenerTipoGanadoFactura]
GO
--=============================================
-- Author     : Ramses Santos
-- Create date: 2014/08/18
-- Description: Obtiene el tipo de ganado de los cuales son mayoria en la lista.
-- select dbo.ObtenerTipoGanadoFactura (1)
--=============================================
CREATE FUNCTION ObtenerTipoGanadoFactura(@LoteSacrificioId INT)
	RETURNS VARCHAR(50)
AS
BEGIN
	DECLARE @TipoGanado VARCHAR(50)

	 SELECT TOP 1 @TipoGanado = TG.Descripcion from LoteSacrificioDetalle (NOLOCK) AS LSD
	 INNER JOIN AnimalHistorico (NOLOCK) AS AH ON (LSD.AnimalID = AH.AnimalID)
	 INNER JOIN TipoGanado (NOLOCK) AS TG ON (TG.TipoGanadoID = AH.TipoGanadoID)
	 WHERE LSD.LoteSacrificioId = @LoteSacrificioId 
	 GROUP BY TG.Descripcion
	 ORDER BY COUNT(TG.Descripcion) DESC

	 Return ISNULL(@TipoGanado, '')
END
GO