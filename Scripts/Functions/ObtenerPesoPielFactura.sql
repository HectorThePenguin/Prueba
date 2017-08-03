 IF EXISTS(SELECT *
      FROM   sys.objects
      WHERE  [object_id] = Object_id(N'[dbo].[ObtenerPesoPielFactura]'))
		DROP FUNCTION [dbo].[ObtenerPesoPielFactura]
GO
--=============================================
-- Author     : Ramses Santos
-- Create date: 2014/08/18
-- Description: Obtiene el peso piel del animalid
-- select dbo.ObtenerPesoPielFactura (1)
--=============================================
CREATE FUNCTION ObtenerPesoPielFactura(@LoteSacrificioId INT)
	RETURNS INT
AS
BEGIN
	DECLARE @PesoPiel INT

	 SELECT @PesoPiel = ISNULL(SUM(AH.PesoPiel), 0)
	  FROM LoteSacrificioDetalle (NOLOCK) AS LSD
	  INNER JOIN AnimalHistorico (NOLOCK) AS AH ON (LSD.AnimalID = AH.AnimalID)
	  WHERE LSD.LoteSacrificioId = @LoteSacrificioId 

	 Return ISNULL(@PesoPiel, 0)
END
GO