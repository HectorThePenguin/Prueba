USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[VentaGanado_ObtenerSalidasPolizaConciliacion]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[VentaGanado_ObtenerSalidasPolizaConciliacion]
GO
/****** Object:  StoredProcedure [dbo].[VentaGanado_ObtenerSalidasPolizaConciliacion]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Gilberto Carranza
-- Create date: 09-07-2014
-- Description:	Obtiene los datos para generar la poliza de salida
-- VentaGanado_ObtenerSalidasPolizaConciliacion '20150326', '20150326', 2
-- =============================================
CREATE PROCEDURE [dbo].[VentaGanado_ObtenerSalidasPolizaConciliacion]
@FechaInicial DATE
, @FechaFinal DATE
, @OrganizacionID INT
AS
BEGIN

	SET NOCOUNT ON

		CREATE TABLE #tVentaGanado
		(
			VentaGanadoID	INT
			, FolioTicket	INT
			, FolioFactura	VARCHAR(100)
			, PesoBruto		DECIMAL
			, PesoTara		DECIMAL
			, FechaVenta	DATETIME
			, ClienteID		INT
			, CodigoSAP		VARCHAR(100)
		)

		INSERT INTO #tVentaGanado
		SELECT DISTINCT VG.VentaGanadoID
			,  VG.FolioTicket
			,  VG.FolioFactura
			,  VG.PesoBruto
			,  VG.PesoTara
			,  VG.FechaVenta
			,  VG.ClienteID
			,  C.CodigoSAP
		FROM VentaGanado VG
		INNER JOIN Cliente C
			ON (VG.ClienteID = C.ClienteID)
		WHERE CAST(VG.FechaVenta AS DATE) BETWEEN @FechaInicial AND @FechaFinal
			AND VG.OrganizacionID = @OrganizacionID
			AND VG.Activo = 0

		SELECT DISTINCT VG.VentaGanadoID
			,  FolioTicket
			,  FolioFactura
			,  PesoBruto
			,  PesoTara
			,  FechaVenta
			,  ClienteID
			,  CodigoSAP
			,  VGD.Arete
			,  VGD.AnimalID
			,  CP.Precio
			,  CP.CausaPrecioID
			,  A.OrganizacionIDEntrada
			,  TG.TipoGanadoID
			,  TG.Descripcion AS TipoGanado
		FROM #tVentaGanado VG
		INNER JOIN VentaGanadoDetalle VGD
			ON (VG.VentaGanadoID = VGD.VentaGanadoID)
		INNER JOIN AnimalHistorico A(NOLOCK)
			ON (VGD.AnimalID = A.AnimalID)
		INNER JOIN TipoGanado TG
			ON (A.TipoGanadoID = TG.TipoGanadoID)
		INNER JOIN CausaPrecio CP
			ON (VGD.CausaPrecioID = CP.CausaPrecioID)

		DROP TABLE #tVentaGanado

	SET NOCOUNT OFF

END

GO
