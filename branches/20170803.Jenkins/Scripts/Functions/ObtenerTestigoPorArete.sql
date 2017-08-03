IF EXISTS(SELECT *
      FROM   sys.objects
      WHERE  [object_id] = Object_id(N'[dbo].[ObtenerTestigoPorArete]'))
		DROP FUNCTION [dbo].[ObtenerTestigoPorArete]
GO
CREATE FUNCTION ObtenerTestigoPorArete(@Arete varchar(15))
Returns varchar(15)
AS
BEGIN
	DECLARE @AreteTestigo varchar(15)
	
	SET @AreteTestigo = 
		(SELECT AreteMetalico 
		FROM Animal
		WHERE Arete = @Arete 	
		AND Activo = 1)

	Return isnull(@AreteTestigo, '')
END

GO