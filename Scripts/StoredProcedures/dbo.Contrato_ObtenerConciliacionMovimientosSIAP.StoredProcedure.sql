USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Contrato_ObtenerConciliacionMovimientosSIAP]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Contrato_ObtenerConciliacionMovimientosSIAP]
GO
/****** Object:  StoredProcedure [dbo].[Contrato_ObtenerConciliacionMovimientosSIAP]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Gilberto Carranza
-- Create date: 10/10/2014
-- Description:  Obtiene un proveedor por producto
-- Contrato_ObtenerConciliacionMovimientosSIAP '20151009', '20151009', 2
-- =============================================
CREATE PROCEDURE [dbo].[Contrato_ObtenerConciliacionMovimientosSIAP]
@FechaInicial DATE
, @FechaFinal DATE
, @OrganizacionID INT
AS
BEGIN
	SET NOCOUNT ON;
		DECLARE @ContratoActivo INT
		SET @ContratoActivo = 45
		CREATE TABLE #tContrato
		(
			ContratoID			INT
			,  OrganizacionID	INT
			,  Folio			INT
			,  ProductoID		INT
			,  TipoContratoID	INT
			,  TipoFleteID		INT
			,  ProveedorID		INT
			,  Precio			DECIMAL(18,4)
			,  TipoCambioID		INT
			,  Cantidad			DECIMAL
			,  Merma			DECIMAL
			,  PesoNegociar		VARCHAR(50)
			,  Fecha			SMALLDATETIME
			,  Tolerancia		DECIMAL
			,  Parcial			BIT
			,  CuentaSAPID		INT
			,  Producto			VARCHAR(100)
			,  TipoCambio		VARCHAR(100)
			,  TipoContrato		VARCHAR(100)
			,  Cambio			DECIMAL
			,  CodigoSAP		VARCHAR(100)
			,  CuentaSAP		VARCHAR(100)
			,  Cuenta			VARCHAR(100)
		)
		INSERT INTO #tContrato
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
			,  C.Tolerancia
			,  C.Parcial
			,  C.CuentaSAPID
			,  P.Descripcion		AS Producto
			,  TC.Descripcion		AS TipoCambio
			,  TCon.Descripcion		AS TipoContrato
			,  TC.Cambio
			,  Prov.CodigoSAP
			,  CS.CuentaSAP
			,  CS.Descripcion		AS Cuenta
		FROM Contrato C
		INNER JOIN Producto P
			ON (C.ProductoID = P.ProductoID)
		INNER JOIN TipoCambio TC
			ON (C.TipoCambioID = TC.TipoCambioID)
		INNER JOIN TipoContrato TCon
			ON (C.TipoContratoID = TCon.TipoContratoID)
		INNER JOIN Proveedor Prov
			ON (C.ProveedorID = Prov.ProveedorID)
		LEFT JOIN CuentaSAP CS
			ON (C.CuentaSAPID = CS.CuentaSAPID)
		WHERE CAST(C.Fecha AS DATE) BETWEEN @FechaInicial AND @FechaFinal
			AND C.Activo = 1
			AND C.EstatusID = @ContratoActivo
			AND C.OrganizacionID = @OrganizacionID
		SELECT ContratoID
			,  OrganizacionID
			,  Folio
			,  ProductoID
			,  TipoContratoID
			,  TipoFleteID
			,  ProveedorID
			,  Precio
			,  TipoCambioID
			,  Cantidad
			,  Merma
			,  PesoNegociar
			,  Fecha
			,  Tolerancia
			,  Parcial
			,  CuentaSAPID
			,  Producto
			,  TipoCambio
			,  TipoContrato
			,  Cambio
			,  CodigoSAP
			,  CuentaSAP
			,  Cuenta
		FROM #tContrato
		SELECT CP.ContratoID
			,  CP.ContratoParcialID
			,  CP.Cantidad
			,  CP.Importe
			,  CP.TipoCambioID
			,  CP.FechaCreacion
			,  TCambio.Cambio
			,  TCambio.Descripcion	AS TipoCambio
		FROM #tContrato tC
		INNER JOIN ContratoParcial CP
			ON (tC.ContratoID = CP.ContratoID
				AND tC.Parcial = 1
				AND CAST(CP.FechaCreacion AS DATE) BETWEEN @FechaInicial AND @FechaFinal)
		INNER JOIN TipoCambio TCambio
			ON (CP.TipoCambioID = TCambio.TipoCambioID)
		DROP TABLE #tContrato
	SET NOCOUNT OFF;
END

GO
