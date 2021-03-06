USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Contrato_ObtenerPorProveedorAlmacenID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Contrato_ObtenerPorProveedorAlmacenID]
GO
/****** Object:  StoredProcedure [dbo].[Contrato_ObtenerPorProveedorAlmacenID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Pedro Delgado
-- Create date: 19/05/2014
-- Description: Obtiene todos los contratos de un proveedor
-- SpName     : Contrato_ObtenerPorProveedorAlmacenID 6119,1,72 
--======================================================
CREATE PROCEDURE [dbo].[Contrato_ObtenerPorProveedorAlmacenID]
@ProveedorID INT,
@OrganizacionID INT,
@AlmacenID INT
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
		C.UsuarioModificacionID,
		C.CalidadOrigen
	FROM Contrato C (NOLOCK)
	INNER JOIN ProveedorAlmacen pa on c.ProveedorID = pa.ProveedorID
	WHERE C.Activo = 1 AND C.ProveedorID = @ProveedorID AND C.OrganizacionID = @OrganizacionID
	and pa.AlmacenID = @AlmacenID
END

GO
