 IF EXISTS(SELECT *
      FROM   sys.objects
      WHERE  [object_id] = Object_id(N'[dbo].[ObtenerPesoNoqueoFactura]'))
		DROP FUNCTION [dbo].[ObtenerPesoNoqueoFactura]
GO
--=============================================
-- Author     : Ramses Santos
-- Create date: 2014/08/18
-- Description: Obtiene el peso noqueo del animalid
-- select dbo.ObtenerPesoNoqueoFactura (1)
--=============================================
CREATE FUNCTION ObtenerPesoNoqueoFactura(@LoteSacrificioId INT)
	RETURNS INT
AS
BEGIN
	DECLARE @PesoNoqueo INT

	 SELECT @PesoNoqueo = ISNULL(SUM(AH.PesoNoqueo), 0)
	  FROM LoteSacrificioDetalle (NOLOCK) AS LSD
	  INNER JOIN AnimalHistorico (NOLOCK) AS AH ON (LSD.AnimalID = AH.AnimalID)
	  WHERE LSD.LoteSacrificioId = @LoteSacrificioId 

	 Return ISNULL(@PesoNoqueo, 0)
END
GO