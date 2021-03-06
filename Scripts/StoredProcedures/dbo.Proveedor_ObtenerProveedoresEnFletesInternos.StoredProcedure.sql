USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Proveedor_ObtenerProveedoresEnFletesInternos]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Proveedor_ObtenerProveedoresEnFletesInternos]
GO
/****** Object:  StoredProcedure [dbo].[Proveedor_ObtenerProveedoresEnFletesInternos]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Ramses Santos
-- Create date: 2014/07/31
-- Description: 
-- EXEC Proveedor_ObtenerProveedoresEnFletesInternos 1, 1
--=============================================
CREATE PROCEDURE [dbo].[Proveedor_ObtenerProveedoresEnFletesInternos]
@ProductoId INT,
@OrganizacionId INT
AS
BEGIN
	SET NOCOUNT ON;
		SELECT P.ProveedorID, P.Descripcion, P.TipoProveedorID, P.CodigoSAP, P.Activo
		FROM FleteInterno (NOLOCK) AS FI
		INNER JOIN FleteInternoDetalle (NOLOCK) AS FID ON (FI.FleteInternoID = FID.FleteInternoID)
		INNER JOIN Proveedor (NOLOCK) AS P ON (P.ProveedorID = FID.ProveedorID)
		WHERE FI.OrganizacionID = @OrganizacionId AND FI.ProductoID = @ProductoId
		AND FI.Activo = 1 AND FID.Activo = 1 AND P.Activo = 1
		GROUP BY P.ProveedorID, P.Descripcion, P.TipoProveedorID, P.CodigoSAP, P.Activo
	SET NOCOUNT OFF;
END

GO
