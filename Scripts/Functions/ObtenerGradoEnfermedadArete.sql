IF EXISTS(SELECT *
      FROM   sys.objects
      WHERE  [object_id] = Object_id(N'[dbo].[ObtenerGradoEnfermedadArete]'))
		DROP FUNCTION [dbo].[ObtenerGradoEnfermedadArete]
GO
CREATE FUNCTION ObtenerGradoEnfermedadArete(@Arete varchar(15))
Returns VARCHAR(1000)
AS
BEGIN
	DECLARE @Grado BIGINT
	
	SET @Grado = (SELECT top 1 GradoID FROM Deteccion WHERE Arete = @Arete order by deteccionid desc)
	
	RETURN ISNULL(@Grado,0)
END
GO