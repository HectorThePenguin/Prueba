 IF EXISTS(SELECT *
      FROM   sys.objects
      WHERE  [object_id] = Object_id(N'[dbo].[ObtenerViscerasFactura]'))
		DROP FUNCTION [dbo].[ObtenerViscerasFactura]
GO
--=============================================
-- Author     : Ramses Santos
-- Create date: 2014/08/18
-- Description: Obtiene el peso canal del animal id
-- select dbo.ObtenerViscerasFactura (1)
--=============================================
CREATE FUNCTION ObtenerViscerasFactura(@LoteSacrificioId INT)
	RETURNS INT
AS
BEGIN
	DECLARE @Visceras INT

	 SELECT @Visceras = COUNT(*)
	  FROM LoteSacrificioDetalle (NOLOCK) AS LSD
	  WHERE  LSD.LoteSacrificioId = @LoteSacrificioId 

	 Return ISNULL(@Visceras, 0)
END
GO