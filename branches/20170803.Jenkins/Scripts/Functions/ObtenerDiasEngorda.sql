IF EXISTS (
		SELECT *
		FROM sys.objects
		WHERE [object_id] = Object_id(N'[dbo].[ObtenerDiasEngorda]')
		)
	DROP FUNCTION [dbo].[ObtenerDiasEngorda]
GO

-- =============================================
-- Author		: José Gilberto Quintero López
-- Create date	: 02/04/2014
-- Description	: Obtiene el tipo de ganado
-- Origen		: Select dbo.ObtenerDiasEngorda(1, 1) AS [DiasEngorda]
--				  Select dbo.ObtenerDiasEngorda(4, 1) AS [DiasEngorda] 	
--=============================================
CREATE FUNCTION dbo.ObtenerDiasEngorda (
	@OrganizacionID INT
	,@LoteID INT
	)
RETURNS INT
AS
BEGIN
	DECLARE @DiasEngorda INT

	SELECT @DiasEngorda = DATEDIFF(dd, FechaDisponibilidad, GETDATE())
	FROM Lote
	WHERE OrganizacionID = @OrganizacionID
		AND LoteID = @LoteID

	RETURN ISNULL(@DiasEngorda, 0)
END
GO


