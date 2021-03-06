USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaIndividual_ObtenerDatosFactura]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SalidaIndividual_ObtenerDatosFactura]
GO
/****** Object:  StoredProcedure [dbo].[SalidaIndividual_ObtenerDatosFactura]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Ramses Santos
-- Create date: 12/08/2014
-- Description:  Obtiene los datos para generar el xml de la factura.
-- SalidaIndividual_ObtenerDatosFactura 60, 4
-- =============================================
CREATE PROCEDURE [dbo].[SalidaIndividual_ObtenerDatosFactura]
	@FolioTicket INT
	, @OrganizacionID INT
AS
BEGIN

	SET NOCOUNT ON

		 SELECT VG.FolioTicket
				, VG.FechaVenta
				, Cte.CodigoSAP
				, Cte.Descripcion AS NombreCliente
				, Cte.RFC
				, Cte.Calle
				, Cte.CodigoPostal
				, Cte.Poblacion
				, Cte.Estado
				, Cte.Pais
				, MP.MetodoPagoID AS MetodoPago
				, Cte.CondicionPago
				, Cte.DiasPago
				, TG.Descripcion AS DescripcionProducto	 
				, CP.Precio AS CausaPrecio
				, CP.CausaPrecioID
				, VGD.AnimalID
				, VG.PesoBruto 
				, VG.PesoTara
				, VG.FolioFactura
		FROM VentaGanado (NOLOCK) AS VG
		INNER JOIN Cliente (NOLOCK) AS Cte 
			ON (Cte.ClienteID = VG.ClienteID)
		INNER JOIN MetodoPago (NOLOCK) AS MP 
			ON (Cte.MetodoPagoID = MP.MetodoPagoID)
		INNER JOIN VentaGanadoDetalle VGD
			ON (VG.VentaGanadoID = VGD.VentaGanadoID)
		INNER JOIN 
		(
			SELECT AnimalID
				,  TipoGanadoID
			FROM Animal A(NOLOCK)
			UNION 
			SELECT AnimalID
				,  TipoGanadoID
			FROM AnimalHistorico A(NOLOCK)
		) A
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
