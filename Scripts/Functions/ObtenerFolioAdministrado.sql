IF EXISTS(SELECT *
      FROM   sys.objects
      WHERE  [object_id] = Object_id(N'[dbo].[ObtenerFolioAdministrado]'))
		DROP FUNCTION [dbo].[ObtenerFolioAdministrado]
GO
CREATE FUNCTION ObtenerFolioAdministrado(@OrganizacionId INT, @TipoFolioID INT)
Returns INT
AS
BEGIN
	DECLARE @Folio INT
	
	SET @Folio = 
		(SELECT Valor 
		FROM Folio
		WHERE OrganizacionId = @OrganizacionId 	
		AND TipoFolioID = @TipoFolioID)

	Return isnull(@Folio, 0)
END

GO