USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Proveedor_ObtenerPorID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Proveedor_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[Proveedor_ObtenerPorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Jos� Gilberto Quintero L�pez
-- Create date: 15/10/2013
-- Description:  Obtener listado de Proveedores
-- Proveedor_ObtenerPorID 1
-- =============================================
CREATE PROCEDURE [dbo].[Proveedor_ObtenerPorID]
	@ProveedorID INT
AS
  BEGIN
      SET NOCOUNT ON;
      SELECT P.ProveedorID,
			P.CodigoSAP,
      		P.Descripcion,
			P.TipoProveedorID,
			p.ImporteComision,
			P.Activo
			, TP.Descripcion AS TipoProveedor,
		    P.CorreoElectronico
      FROM Proveedor P
	  INNER JOIN TipoProveedor TP
		ON (P.TipoProveedorID = TP.TipoProveedorID)
      WHERE ProveedorID = @ProveedorID
      SET NOCOUNT OFF;
  END

GO
