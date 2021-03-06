USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Contrato_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Contrato_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[Contrato_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Pedro Delgado
-- Create date: 15/05/2014
-- Description: Obtiene un contrato por ContratoID
-- SpName     : Contrato_ObtenerPorID 1108,1
--======================================================
CREATE PROCEDURE [dbo].[Contrato_ObtenerPorID] 
@ContratoID INT,
@OrganizacionID INT
AS
BEGIN
	SELECT 
		C.ContratoID,
		C.OrganizacionID,
		C.Folio,
		C.ProductoID,
		C.TipoContratoID,
		C.TipoFleteID,
		C.ProveedorID,
		C.Precio,
		C.Precio / TC.Cambio AS PrecioConvertido,
		C.TipoCambioID,
		TC.Descripcion,
		C.Cantidad,
		C.Merma,
		C.PesoNegociar,
		C.Fecha,
		C.FechaVigencia,
		C.Tolerancia,
		C.Parcial,
		C.CuentaSAPID,
		C.EstatusID,
		C.Activo,
		C.FechaCreacion,
		C.UsuarioCreacionID,
		C.CalidadOrigen,
		C.AplicaDescuento
	FROM Contrato (NOLOCK) C
	INNER JOIN TipoCambio TC ON(TC.TipoCambioID = C.TipoCambioID)
	WHERE C.ContratoID = @ContratoID 
	AND @OrganizacionID in (C.OrganizacionID,0)
END
GO
