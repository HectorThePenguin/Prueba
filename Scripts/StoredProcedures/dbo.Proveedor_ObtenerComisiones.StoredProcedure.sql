USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Proveedor_ObtenerComisiones]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Proveedor_ObtenerComisiones]
GO
/****** Object:  StoredProcedure [dbo].[Proveedor_ObtenerComisiones]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Daniel Benitez
-- Create date: 04/08/2015
-- Description:	Otiene un listado de Proveedores paginado con filtro de tipo proveedor
-- Proveedor_ObtenerComisiones 2431
-- =============================================
CREATE PROCEDURE [dbo].[Proveedor_ObtenerComisiones]
	@ProveedorID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT pcom.ProveedorComisionID, pcom.ProveedorID, pcom.TipoComisionID, pcom.Tarifa, tcom.descripcion
	FROM ProveedorComision pcom INNER JOIN TipoComision tcom ON (pcom.TipoComisionID = tcom.TipoComisionID ) 
	WHERE pcom.Activo = 1 AND tcom.Activo = 1 and pcom.ProveedorID = @ProveedorID
END

GO
