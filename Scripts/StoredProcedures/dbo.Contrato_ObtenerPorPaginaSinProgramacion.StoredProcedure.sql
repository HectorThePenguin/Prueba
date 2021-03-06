USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Contrato_ObtenerPorPaginaSinProgramacion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Contrato_ObtenerPorPaginaSinProgramacion]
GO
/****** Object:  StoredProcedure [dbo].[Contrato_ObtenerPorPaginaSinProgramacion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--=============================================
-- Author     : Pedro Delgado
-- Create date: 07/08/2014
-- Description: Obtiene contratos por pagina
-- Contrato_ObtenerPorPaginaSinProgramacion 0, 0, 0,'', 0, 0, 0, 0, 0, '', 1, 1, 15
--=============================================
CREATE PROCEDURE [dbo].[Contrato_ObtenerPorPaginaSinProgramacion] 
@ContratoID INT,
@OrganizacionID INT,
@Folio INT,
@ProductoDescripcion VARCHAR(50),
@ProductoID INT,
@TipoContratoID INT,
@TipoFleteID INT,
@ProveedorID INT,
@CodigoSAP VARCHAR(10),
@EstatusID INT,
@TipoCambioID INT,
@PesoNegociar VARCHAR(10),
@Activo BIT,
@Inicio INT,
@Limite INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT ROW_NUMBER() OVER (
			ORDER BY C.Folio ASC
			) AS RowNum,
		C.ContratoID,
		C.OrganizacionID,
	    O.Descripcion AS OrganizacionDescripcion,
		O.TipoOrganizacionID,
		C.Folio,
		C.ProductoID,
		P.Descripcion AS ProductoDescripcion,
		P.SubFamiliaID,
		C.TipoContratoID,
		TC.Descripcion AS TipoContratoDescripcion,
		ISNULL(C.TipoFleteID, 0) AS TipoFleteID,
		ISNULL(TF.Descripcion, '') AS TipoFleteDescripcion,
		C.ProveedorID,
		PR.Descripcion AS ProveedorDescripcion,
		PR.CodigoSAP,
		PR.TipoProveedorID,
		C.Precio,
		ISNULL(C.TipoCambioID, 0) AS TipoCambioID,
		TCB.Descripcion AS TipoCambioDescripcion,
		TCB.Cambio,
		C.EstatusID,
		E.TipoEstatus,
		E.Descripcion AS EstatusDescripcion,
		C.CuentaSAPID,
		CS.CuentaSAP,
		CS.Descripcion AS CuentaDescripcion,
		C.Cantidad,
		C.Tolerancia,
		C.Parcial,
		C.Merma,
		C.PesoNegociar,
		C.Fecha,
		C.FechaVigencia,
		C.CalidadOrigen,
		C.FolioAserca,
		C.FolioCobertura,
		C.CostoSecado,
		C.AplicaDescuento,
		C.Activo,
		C.FechaCreacion,
		C.UsuarioCreacionID
	INTO #Datos
	FROM Contrato C
	INNER JOIN Organizacion O
		ON (O.OrganizacionID = C.OrganizacionID)
	INNER JOIN Producto P
		ON (C.ProductoID = P.ProductoID)
	INNER JOIN TipoContrato TC
		ON (C.TipoContratoID = TC.TipoContratoID)
	INNER JOIN TipoFlete TF
		ON (C.TipoFleteID = TF.TipoFleteID)
	INNER JOIN Proveedor PR
		ON (C.ProveedorID = PR.ProveedorID)
	INNER JOIN TipoCambio TCB
		ON (C.TipoCambioID = TCB.TipoCambioID)
	LEFT JOIN Flete F
		ON (F.ContratoID = C.ContratoID AND F.OrganizacionID = C.OrganizacionID and f.Activo = 1)
	LEFT JOIN CuentaSAP CS
		ON (C.CuentaSAPID = CS.CuentaSAPID)
	INNER JOIN Estatus E
		ON (C.EstatusID = E.EstatusID)

	WHERE @Folio IN (C.Folio, 0)
	    AND @ContratoID IN (C.ContratoID, 0)
		AND @OrganizacionID IN (C.OrganizacionID, 0)
		AND @ProductoID IN (C.ProductoID, 0)
		AND (P.Descripcion LIKE '%'+@ProductoDescripcion+'%' OR @ProductoDescripcion = '')
		AND @TipoContratoID IN (C.TipoContratoID, 0)
		AND @TipoFleteID IN (C.TipoFleteID, 0)
		AND @ProveedorID IN (C.ProveedorID, 0)
		AND @TipoCambioID IN (C.TipoCambioID, 0)
		AND @PesoNegociar IN (C.PesoNegociar, '')
		AND (PR.CodigoSAP LIKE '%'+@CodigoSAP+'%' OR @CodigoSAP = '')
		AND C.Activo = @Activo
		AND F.ContratoID IS NULL

	SELECT ContratoID,
		   OrganizacionID,
		   OrganizacionDescripcion,
		   TipoOrganizacionID,
		   Folio,
		   ProductoID,
		   ProductoDescripcion,
		   SubFamiliaID,
		   TipoContratoID,
		   TipoContratoDescripcion,
		   TipoFleteID,
		   TipoFleteDescripcion,
		   ProveedorID,
		   ProveedorDescripcion,
		   CodigoSAP,
		   TipoProveedorID,
		   Precio,
		   TipoCambioID,
		   TipoCambioDescripcion,
		   Cambio,
		   EstatusID,
		   TipoEstatus,
		   EstatusDescripcion,
		   CuentaSAPID,
		   CuentaSAP,
		   CuentaDescripcion,
		   Cantidad,
		   Tolerancia,
		   Parcial,
		   Merma,
		   PesoNegociar,
		   Fecha,
		   FechaVigencia,
		   CalidadOrigen,
		   FolioAserca,
		   FolioCobertura,
		   CostoSecado,
		   AplicaDescuento,
		   Activo,
		   FechaCreacion,
		   UsuarioCreacionID
		   , 0 AS CantidadSurtida
	FROM #Datos
	WHERE RowNum BETWEEN @Inicio
			AND @Limite

	SELECT COUNT(ContratoID) AS TotalReg
	FROM #Datos

	DROP TABLE #Datos

	SET NOCOUNT OFF;
END
GO
