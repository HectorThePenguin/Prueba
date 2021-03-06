USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaIndividualVenta_ConsultaVentaGanadoPorTicket]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SalidaIndividualVenta_ConsultaVentaGanadoPorTicket]
GO
/****** Object:  StoredProcedure [dbo].[SalidaIndividualVenta_ConsultaVentaGanadoPorTicket]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Ramses Santos
-- Create date: 2014/02/28
-- Description: 
--  exec SalidaIndividualVenta_ConsultaVentaGanadoPorTicket 23, 1
--=============================================
CREATE PROCEDURE [dbo].[SalidaIndividualVenta_ConsultaVentaGanadoPorTicket] @FolioTicket INT
	,@OrganizacionID INT
	,@TipoVenta INT
AS
BEGIN
	SET NOCOUNT ON;
	
	IF @TipoVenta = 1 --Tipo Propio
		BEGIN

			SELECT VG.VentaGanadoID
				,VG.FolioTicket
				,VG.FolioFactura
				,VG.ClienteID
				,Cte.CodigoSAP
				,VG.FechaVenta
				,VG.PesoTara
				,VG.PesoBruto
				,VG.LoteID
				,C.Codigo
				,VG.Activo
				,VG.UsuarioCreacionID
				,VG.UsuarioModificacionID
				,0 as TotalCabezas
			FROM VentaGanado AS VG
			LEFT JOIN Lote AS L ON (VG.LoteID = L.LoteID)
			LEFT JOIN Corral AS C ON (L.CorralID = C.CorralID)
			LEFT JOIN Cliente AS Cte ON (Cte.ClienteID = VG.ClienteID)
			WHERE VG.FolioTicket = @FolioTicket
				AND VG.OrganizacionID = @OrganizacionID
				AND VG.Activo = 1
				AND CAST(VG.FechaVenta AS DATE) = CAST(GETDATE() AS DATE)
		END
	ELSE --Tipo Externo
		BEGIN
			SELECT CAST(SGI.SalidaGanadoIntensivoID AS INT) VentaGanadoID
				,SGI.FolioTicket
				,SGI.FolioFactura
				,SGI.ClienteID
				,Cte.CodigoSAP
				,SGIP.FechaPesoTara FechaVenta
				,SGIP.PesoTara
				,SGIP.PesoBruto
				,SGI.LoteID
				,C.Codigo
				,SGI.Activo
				,SGI.UsuarioCreacionID
				,SGI.UsuarioModificacionID
				,cast(SGI.Cabezas as int) TotalCabezas
			FROM SalidaGanadoIntensivo AS SGI
			INNER JOIN SalidaGanadoIntensivoPesaje AS SGIP ON (SGI.SalidaGanadoIntensivoID = SGIP.SalidaGanadoIntensivoID)
			LEFT JOIN Lote AS L ON (SGI.LoteID = L.LoteID)
			LEFT JOIN Corral AS C ON (L.CorralID = C.CorralID)
			LEFT JOIN Cliente AS Cte ON (Cte.ClienteID = SGI.ClienteID)
			WHERE SGI.FolioTicket = @FolioTicket
				AND SGI.OrganizacionID = @OrganizacionID
				AND SGI.Activo = 1
				AND CAST(SGIP.FechaPesoTara AS DATE) = CAST(GETDATE() AS DATE)
		END
	
		
	SET NOCOUNT OFF;
END

GO

