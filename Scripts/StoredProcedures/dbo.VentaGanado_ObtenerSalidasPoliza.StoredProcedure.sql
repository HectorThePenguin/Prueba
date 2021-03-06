USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[VentaGanado_ObtenerSalidasPoliza]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[VentaGanado_ObtenerSalidasPoliza]
GO
/****** Object:  StoredProcedure [dbo].[VentaGanado_ObtenerSalidasPoliza]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Gilberto Carranza
-- Create date: 09-07-2014
-- Description:	Obtiene los datos para generar la poliza de salida
-- VentaGanado_ObtenerSalidasPoliza 54, 2
-- =============================================
CREATE PROCEDURE [dbo].[VentaGanado_ObtenerSalidasPoliza]
@FolioTicket INT
, @OrganizacionID INT
AS
BEGIN

	SET NOCOUNT ON

		SELECT DISTINCT VG.FolioTicket
			,  VG.FolioFactura
			,  VG.PesoBruto
			,  VG.PesoTara
			,  VG.FechaVenta
			,  VG.ClienteID
			,  C.CodigoSAP
			,  VGD.Arete
			,  VGD.AnimalID
			,  '' AS FotoVenta --VGD.FotoVenta
			,  CP.Precio			
			,  CP.CausaPrecioID
			,  A.OrganizacionIDEntrada
			,  TG.TipoGanadoID
			,  TG.Descripcion AS TipoGanado
		FROM VentaGanado VG
		INNER JOIN Cliente C
			ON (VG.ClienteID = C.ClienteID)
		INNER JOIN VentaGanadoDetalle VGD
			ON (VG.VentaGanadoID = VGD.VentaGanadoID)
		INNER JOIN AnimalHistorico A(NOLOCK)
			ON (VGD.AnimalID = A.AnimalID)
		INNER JOIN TipoGanado TG
			ON (A.TipoGanadoID = TG.TipoGanadoID)
		INNER JOIN CausaPrecio CP
			ON (VGD.CausaPrecioID = CP.CausaPrecioID)
		WHERE VG.FolioTicket = @FolioTicket
			AND VG.OrganizacionID = @OrganizacionID

	SET NOCOUNT OFF

END

GO
