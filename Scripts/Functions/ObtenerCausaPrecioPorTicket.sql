IF EXISTS (
		SELECT *
		FROM sys.objects
		WHERE [object_id] = Object_id(N'[dbo].[ObtenerCausaPrecioPorTicket]')
		)
	DROP FUNCTION [dbo].[ObtenerCausaPrecioPorTicket]
GO

-- =============================================
-- Author		: Ramses Santos
-- Create date	: 12/08/2014
-- Description	: Obtiene la causa precio del ticket ingresado
-- Origen		: Select dbo.ObtenerCausaPrecioPorTicket(1) AS [DescripcionTipoGanado]	
--=============================================
CREATE FUNCTION dbo.ObtenerCausaPrecioPorTicket (
	@Ticket INT
)
RETURNS [decimal](10, 2)
AS
BEGIN
	DECLARE @Precio [decimal](10, 2)

	SELECT @Precio = CP.Precio FROM VentaGanado (NOLOCK) AS VG
		INNER  JOIN  VentaGanadoDetalle (NOLOCK) AS VGD ON (VG.VentaGanadoID = VGD.VentaganadoID)
		INNER JOIN CausaPrecio(NOLOCK) AS CP ON (CP.CausaPrecioID = VGD.CausaPrecioID)
		WHERE VG.FolioTicket = @Ticket
		GROUP BY CP.Precio

	RETURN ISNULL(@Precio, 0)
END
GO


