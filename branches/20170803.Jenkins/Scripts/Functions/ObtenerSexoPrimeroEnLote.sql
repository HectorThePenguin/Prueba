IF EXISTS (
		SELECT *
		FROM sys.objects
		WHERE [object_id] = Object_id(N'[dbo].[ObtenerSexoPrimeroEnLote]')
		)
	DROP FUNCTION [dbo].[ObtenerSexoPrimeroEnLote]
GO
--=============================================
-- Author     : Alejandro Quiroz
-- Create date: 2014/09/09
-- Description: Función para obtener el sexo del primer animal en un loteID
-- select dbo.ObtenerSexoPrimeroEnLote(100)
--=============================================
CREATE FUNCTION ObtenerSexoPrimeroEnLote(
	@LoteID INT)
RETURNS CHAR (1)
AS
BEGIN
	DECLARE @Sexo AS CHAR(1)
	SET @Sexo = 'H'
	SELECT TOP 1 @Sexo = TG.Sexo 
	FROM Animal (NOLOCK) A
	INNER JOIN AnimalMovimiento (NOLOCK) AM ON (A.AnimalID = AM.AnimalID)
	INNER JOIN TipoGanado (NOLOCK) TG ON (A.TipoGanadoID = TG.TipoGanadoID)
	WHERE AM.LoteID = @LoteID
	RETURN @Sexo
END
GO
