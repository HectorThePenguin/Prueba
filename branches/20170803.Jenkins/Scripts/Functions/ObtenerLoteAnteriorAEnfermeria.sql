IF EXISTS(SELECT *
      FROM   sys.objects
      WHERE  [object_id] = Object_id(N'[dbo].[ObtenerLoteAnteriorAEnfermeria]'))
		DROP FUNCTION [dbo].[ObtenerLoteAnteriorAEnfermeria]
GO
CREATE FUNCTION ObtenerLoteAnteriorAEnfermeria (@AnimalID INT)
Returns INT
AS
BEGIN
	DECLARE @LoteID INT
	
	SET @LoteID = 
		(SELECT TOP 1 AM.LoteID 
		   FROM AnimalMovimiento AM 
		  INNER JOIN Corral C ON (AM.CorralID = C.CorralID AND C.TipoCorralID != 3)
		  WHERE AM.AnimalID = @AnimalID 
		  ORDER BY AnimalMovimientoID DESC)

	Return isnull(@LoteID, 0)
END

GO