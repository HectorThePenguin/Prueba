IF EXISTS(SELECT *
      FROM   sys.objects
      WHERE  [object_id] = Object_id(N'[dbo].[ObtenerCorralAnteriorAEnfermeria]'))
		DROP FUNCTION [dbo].[ObtenerCorralAnteriorAEnfermeria]
GO
CREATE FUNCTION ObtenerCorralAnteriorAEnfermeria(@AnimalID INT)
Returns INT
AS
BEGIN
	DECLARE @CorralID INT
	
	SET @CorralID = 
		(SELECT TOP 1 AM.CorralID FROM AnimalMovimiento AM INNER JOIN Corral C ON (AM.CorralID = C.CorralID AND C.TipoCorralID != 3) 
		WHERE AM.AnimalID = @AnimalID ORDER BY AnimalMovimientoID DESC)

	Return isnull(@CorralID, 0)
END

GO