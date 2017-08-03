IF EXISTS(SELECT *
      FROM   sys.objects
      WHERE  [object_id] = Object_id(N'[dbo].[ObtenerAretePorTestigo]'))
		DROP FUNCTION [dbo].[ObtenerAretePorTestigo]
GO
CREATE FUNCTION ObtenerAretePorTestigo(@AreteTestigo varchar(15))
Returns varchar(15)
AS
BEGIN
	DECLARE @Arete varchar(15)
	
	SET @Arete = 
		(SELECT Arete 
		FROM Animal
		WHERE AreteMetalico = @AreteTestigo 	
		AND Activo = 1)

	Return isnull(@Arete, '')
END

GO