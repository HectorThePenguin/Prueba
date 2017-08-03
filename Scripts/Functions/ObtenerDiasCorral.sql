IF EXISTS (
		SELECT *
		FROM sys.objects
		WHERE [object_id] = Object_id(N'[dbo].[ObtenerDiasCorral]')
		)
	DROP FUNCTION [dbo].[ObtenerDiasCorral]
GO

-- =============================================
-- Author		: José Gilberto Quintero López
-- Create date	: 02/04/2014
-- Description	: Obtiene el tipo de ganado
-- Origen		: Select dbo.ObtenerDiasCorral(1, 1) AS [DiasCorral]
--				  Select dbo.ObtenerDiasCorral(4, 1) AS [DiasCorral] 	
--=============================================
CREATE FUNCTION dbo.ObtenerDiasCorral (
	@OrganizacionID INT
	,@LoteID INT
	)
RETURNS INT
AS
BEGIN
	DECLARE @DiasCorral INT

	SELECT @DiasCorral = DATEDIFF(dd, FechaInicio, GETDATE())
	FROM Lote
	WHERE OrganizacionID = @OrganizacionID
		AND LoteID = @LoteID

	RETURN ISNULL(@DiasCorral, 0)
END
GO


