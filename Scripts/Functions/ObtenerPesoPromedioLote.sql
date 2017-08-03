IF EXISTS (
		SELECT *
		FROM sys.objects
		WHERE [object_id] = Object_id(N'[dbo].[ObtenerPesoPromedioLote]')
		)
	DROP FUNCTION [dbo].[ObtenerPesoPromedioLote]
GO
--=============================================
-- Author     : Alejandro Quiroz
-- Create date: 2014/09/09
-- Description: Función para obtener el peso promedio de un loteID
-- select dbo.ObtenerPesoPromedioLote(100)
--=============================================
CREATE FUNCTION ObtenerPesoPromedioLote(
	@LoteID INT)
RETURNS INT
AS
BEGIN
	DECLARE @Promedio AS INT
	SET @Promedio = 0
	SELECT @Promedio = AVG(AM.Peso) 
	FROM AnimalMovimiento (NOLOCK) AM 
	WHERE AM.LoteID = @LoteID
	RETURN @Promedio
END
GO
