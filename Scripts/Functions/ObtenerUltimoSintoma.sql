IF EXISTS (
		SELECT *
		FROM sys.objects
		WHERE [object_id] = Object_id(N'[dbo].[ObtenerUltimoSintoma]')
		)
	DROP FUNCTION [dbo].[ObtenerUltimoSintoma]
GO
-- =============================================  
-- Author:    Gilberto Carranza
-- Create date: 28-04-2014  
-- Description:  Obtiene el ultimo sintoma del animal
-- Select * From ObtenerUltimoSintoma(1)
-- =============================================  
CREATE FUNCTION dbo.ObtenerUltimoSintoma(@DeteccionAnimalID INT)
RETURNS @Corrales TABLE 
(
	SintomaID INT
	, DeteccionAnimalID INT
)
AS
BEGIN

	INSERT INTO @Corrales
	SELECT MAX(SintomaID)
		,  @DeteccionAnimalID
	FROM DeteccionSintomaAnimal 
	WHERE DeteccionAnimalID = @DeteccionAnimalID
	GROUP BY CONVERT(CHAR(8), FechaCreacion, 112)

	RETURN

END
