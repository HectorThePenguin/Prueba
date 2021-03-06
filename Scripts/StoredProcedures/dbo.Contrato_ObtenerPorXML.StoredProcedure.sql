USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Contrato_ObtenerPorXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Contrato_ObtenerPorXML]
GO
/****** Object:  StoredProcedure [dbo].[Contrato_ObtenerPorXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Gilberto Carranza
-- Create date: 10/10/2014
-- Description:  Obtiene un proveedor por producto
-- Contrato_ObtenerPorXML
-- =============================================
CREATE PROCEDURE [dbo].[Contrato_ObtenerPorXML]
@XmlContrato XML
AS
BEGIN
	SET NOCOUNT ON;
		SELECT C.ContratoID
			,  C.OrganizacionID
			,  C.Folio
			,  C.ProductoID
			,  C.TipoContratoID
			,  C.TipoFleteID
			,  C.ProveedorID
			,  C.Precio
			,  C.TipoCambioID
			,  C.Cantidad
			,  C.Merma
			,  C.PesoNegociar
			,  C.Fecha
			,  C.FechaVigencia
			,  C.Tolerancia
			,  C.Parcial
			,  C.CuentaSAPID
			,  C.EstatusID
			,  P.CodigoSAP
			,  P.Descripcion		AS Proveedor
			,  Pro.Descripcion		AS Producto
			,  CS.CuentaSAP
			,  CS.Descripcion		AS DescripcionCuentaSAP
			,  TCam.Descripcion		AS TipoCambio
			,  O.Descripcion		AS Organizacion
			,  TC.Descripcion		AS TipoContrato
		FROM Contrato C
		INNER JOIN
		(
			SELECT T.N.value('./ContratoID[1]','INT') AS ContratoID
			FROM @XmlContrato.nodes('/ROOT/Contrato') as T(N)
		) x	ON (C.ContratoID = x.ContratoID)
		INNER JOIN Proveedor P
			ON (C.ProveedorID = P.ProveedorID)
		INNER JOIN Producto Pro
			ON (C.ProductoID = Pro.ProductoID)
		INNER JOIN TipoContrato TC
			ON (C.TipoContratoID = TC.TipoContratoID)
		INNER JOIN CuentaSAP CS
			ON (C.CuentaSAPID = CS.CuentaSAPID)
		INNER JOIN TipoCambio TCam
			ON (C.TipoCambioID = TCam.TipoCambioID)
		INNER JOIN TipoFlete TF
			ON (C.TipoFleteID = TF.TipoFleteID)
		INNER JOIN Organizacion O
			ON (C.OrganizacionID = O.OrganizacionID)
	SET NOCOUNT OFF;
END

GO
