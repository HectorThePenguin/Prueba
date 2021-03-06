USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Contrato_ObtenerPorFolioContenedor]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Contrato_ObtenerPorFolioContenedor]
GO
/****** Object:  StoredProcedure [dbo].[Contrato_ObtenerPorFolioContenedor]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Carranza
-- Create date: 28/07/2014
-- Description: Obtiene las entrada por folio
-- SpName     : Contrato_ObtenerPorFolioContenedor 1, 1
--======================================================
CREATE PROCEDURE [dbo].[Contrato_ObtenerPorFolioContenedor]
@Folio INT
, @OrganizacionId INT
AS 
BEGIN
	SET NOCOUNT ON
		SELECT 
			EP.EntradaProductoID,
			EP.OrganizacionID,
			EP.RegistroVigilanciaID,
			EP.Folio,
			EP.Fecha,
			EP.Observaciones,
			EP.PesoOrigen,
			EP.PesoBruto,
			EP.PesoTara,
			EP.Piezas,
			EP.TipoContratoID,
			EP.AlmacenInventarioLoteID,
			EP.AlmacenMovimientoID
			, EP.ProductoID
			, C.ContratoID
			, C.Cantidad
			, C.Merma
			, C.PesoNegociar
			, C.Precio
			, Pro.ProveedorID
			, Pro.CodigoSAP
			, Pro.Descripcion		AS Proveedor
		FROM Contrato C
		INNER JOIN EntradaProducto EP
			ON (EP.Folio = @Folio
				AND C.OrganizacionID = @OrganizacionId
				AND C.ContratoID = EP.ContratoID
				AND C.ProductoID = EP.ProductoID)		
		INNER JOIN Proveedor Pro
			ON (C.ProveedorID = Pro.ProveedorID)
	SET NOCOUNT OFF
END

GO
