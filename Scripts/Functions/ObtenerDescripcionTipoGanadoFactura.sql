IF EXISTS (
		SELECT *
		FROM sys.objects
		WHERE [object_id] = Object_id(N'[dbo].[ObtenerDescripcionTipoGanadoFactura]')
		)
	DROP FUNCTION [dbo].[ObtenerDescripcionTipoGanadoFactura]
GO

-- =============================================
-- Author		: Ramses Santos
-- Create date	: 12/08/2014
-- Description	: Obtiene tipo de ganado del ticket de la venta de ganado
-- Origen		: Select dbo.ObtenerDescripcionTipoGanadoFactura(1) AS [DescripcionTipoGanado]	
--=============================================
CREATE FUNCTION dbo.ObtenerDescripcionTipoGanadoFactura (
	@Ticket INT
)
RETURNS VARCHAR(50)
AS
BEGIN
	DECLARE @Descripcion VARCHAR(50)

	SELECT @Descripcion = TG.Descripcion 
		FROM VentaGanado VG
		INNER  JOIN  VentaGanadoDetalle (NOLOCK) AS VGD ON (VG.VentaGanadoID = VGD.VentaganadoID)
		INNER JOIN Animal (NOLOCK) A on VGD.AnimalID = A.AnimalID
		INNER JOIN TipoGanado (NOLOCK) TG on TG.TipoGanadoID = A.TipoGanadoID
		WHERE VG.FolioTicket = @Ticket
		GROUP BY TG.TipoGanadoID, TG.Descripcion
		ORDER BY COUNT(TG.TipoGanadoID) ASC

	RETURN ISNULL(@Descripcion, '')
END
GO


