IF EXISTS (
		SELECT *
		FROM sys.objects
		WHERE [object_id] = Object_id(N'[dbo].[ObtenerTipoGanadoPrimeroEnLote]')
		)
	DROP FUNCTION [dbo].[ObtenerTipoGanadoPrimeroEnLote]
GO
--=============================================
-- Author     : Alejandro Quiroz
-- Create date: 2014/09/09
-- Description: Función para obtener el sexo del primer animal en un loteID
-- select dbo.ObtenerTipoGanadoPrimeroEnLote(100)
--=============================================
CREATE FUNCTION ObtenerTipoGanadoPrimeroEnLote(
	@LoteID INT)
RETURNS VARCHAR(50)
AS
BEGIN
	DECLARE @TipoGanado AS VARCHAR(50)
	SELECT TOP 1 @TipoGanado = TG.Descripcion 
	FROM Animal (NOLOCK) A
	INNER JOIN AnimalMovimiento (NOLOCK) AM ON (A.AnimalID = AM.AnimalID)
	INNER JOIN TipoGanado (NOLOCK) TG ON (A.TipoGanadoID = TG.TipoGanadoID)
	WHERE AM.LoteID = @LoteID
	RETURN @TipoGanado
END
GO
