USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[VentaGanado_ObtenerPorFolioTicket]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[VentaGanado_ObtenerPorFolioTicket]
GO
/****** Object:  StoredProcedure [dbo].[VentaGanado_ObtenerPorFolioTicket]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Gilberto Carranza
-- Create date: 14-07-2014
-- Description:	Obtiene los datos para generar la poliza de salida
-- VentaGanado_ObtenerPorFolioTicket 71, 5
-- =============================================
CREATE PROCEDURE [dbo].[VentaGanado_ObtenerPorFolioTicket]
@FolioTicket INT
, @OrganizacionID INT
AS
BEGIN

	SET NOCOUNT ON

		SELECT VG.FolioTicket
			,  C.CodigoSAP
			,  C.Descripcion
			,  VG.VentaGanadoID
			,  VG.FechaVenta
		FROM VentaGanado VG
		INNER JOIN Cliente C
			ON (VG.ClienteID = C.ClienteID)
		INNER JOIN Lote L
			ON (VG.LoteID = L.LoteID)
		WHERE VG.FolioTicket = @FolioTicket
			AND VG.OrganizacionID = @OrganizacionID
			AND VG.Activo = 0

	SET NOCOUNT OFF

END

GO
