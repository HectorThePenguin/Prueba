USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerPorFolioSalida]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Producto_ObtenerPorFolioSalida]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerPorFolioSalida]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Roque Solis
-- Origen: Apinterfaces
-- Create date: 20/06/2014
-- Description:  Obtener el Producto de una salida
-- EXEC Producto_ObtenerPorFolioSalida 123
-- =============================================
CREATE PROCEDURE [dbo].[Producto_ObtenerPorFolioSalida] 
@FolioSalida INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT pr.ProductoID,
	       pr.Descripcion,
		   sf.SubFamiliaID,
		   sf.Descripcion as [SubFamilia],
		   f.FamiliaID,
		   f.Descripcion as [Familia],
		   pr.UnidadID,
		   um.Descripcion as [Unidad],
		   pr.ManejaLote,
		   pr.Activo
	FROM Producto pr
	INNER JOIN SubFamilia sf ON pr.SubFamiliaID = sf.SubFamiliaID
	INNER JOIN Familia f ON sf.FamiliaID = f.FamiliaID
	INNER JOIN UnidadMedicion um ON um.UnidadID = pr.UnidadID
	INNER JOIN SalidaProducto sp ON sp.FolioSalida = @FolioSalida
	INNER JOIN AlmacenInventarioLote ail ON ail.AlmacenInventarioLoteID = sp.AlmacenInventarioLoteID
	INNER JOIN AlmacenInventario ai ON ai.AlmacenInventarioID = ail.AlmacenInventarioID
	WHERE pr.ProductoID = ai.ProductoID
	  AND pr.Activo = 1
	SET NOCOUNT OFF;
END

GO
