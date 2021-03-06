USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Contrato_ObtenerPorProveedorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Contrato_ObtenerPorProveedorID]
GO
/****** Object:  StoredProcedure [dbo].[Contrato_ObtenerPorProveedorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Pedro Delgado
-- Create date: 19/05/2014
-- Description: Obtiene todos los contratos de un proveedor
-- SpName     : Contrato_ObtenerPorProveedorID 1
--======================================================
CREATE PROCEDURE [dbo].[Contrato_ObtenerPorProveedorID]
@ProveedorID INT,
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
		C.TipoCambioID,
		C.Cantidad,
		C.Merma,
		C.PesoNegociar,
		C.Fecha,
		C.FechaVigencia,
		C.Activo,
		C.FechaCreacion,
		C.UsuarioCreacionID,
		C.FechaModificacion,
		C.UsuarioModificacionID
	FROM Contrato C (NOLOCK)
	WHERE C.Activo = 1 AND C.ProveedorID = @ProveedorID AND C.OrganizacionID = @OrganizacionID
END

GO
