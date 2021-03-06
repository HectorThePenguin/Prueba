USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_ObtenerProductosPorAlmacenID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Almacen_ObtenerProductosPorAlmacenID]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_ObtenerProductosPorAlmacenID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Edgar Villarreal
-- Create date: 25/03/2014 12:00:00 a.m.
-- Description: Obtiene todos los productos asignado almacenID
-- SpName     : EXEC Almacen_ObtenerProductosPorAlmacenID 1,4,1
--======================================================
CREATE PROCEDURE [dbo].[Almacen_ObtenerProductosPorAlmacenID]
@Activo INT,
@OrganizacionID INT,
@AlmacenID INT
AS
BEGIN
	SET NOCOUNT ON;
			SELECT A.AlmacenID, P.ProductoID, P.Descripcion, UM.ClaveUnidad,AI.PrecioPromedio
			FROM Almacen A
			INNER JOIN AlmacenInventario AS AI ON AI.AlmacenID=A.AlmacenID
			INNER JOIN Producto AS P ON P.ProductoID=AI.ProductoID
			INNER JOIN UnidadMedicion AS UM ON UM.UnidadID=P.UnidadID 
			WHERE A.OrganizacionID=@OrganizacionID
			AND A.AlmacenID=@AlmacenID
			AND P.Activo=@Activo
			AND A.Activo=@Activo;
	SET NOCOUNT OFF;
END

GO
