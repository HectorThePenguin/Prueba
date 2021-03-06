USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerPorSubFamiliaAyuda]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Producto_ObtenerPorSubFamiliaAyuda]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerPorSubFamiliaAyuda]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Vel�zquez Araujo
-- Create date: 18/06/2014
-- Description: Obtiene los Productos filtrados por la Sub Familia
-- SpName     : Producto_ObtenerPorSubFamiliaAyuda 10
--======================================================
CREATE PROCEDURE [dbo].[Producto_ObtenerPorSubFamiliaAyuda] @ProductoID INT
	,@Descripcion VARCHAR(50)
	,@SubFamiliaID INT
AS
if @Descripcion is null
begin
	set @Descripcion = ''
end
SELECT pro.ProductoID
	,pro.Descripcion
	,pro.SubFamiliaID
	,pro.UnidadID
	,pro.ManejaLote
	,pro.Activo
	,pro.FechaCreacion
	,pro.UsuarioCreacionID
	,pro.FechaModificacion
	,pro.UsuarioModificacionID
FROM Producto pro
INNER JOIN SubFamilia sub ON pro.SubFamiliaID = sub.SubFamiliaID
INNER JOIN Familia fa ON sub.FamiliaId = fa.FamiliaID
WHERE pro.Activo = 1
	AND sub.SubFamiliaID = @SubFamiliaID
	AND (
		pro.Descripcion LIKE '%' + @Descripcion + '%'
		OR @Descripcion = ''
		)
	AND @ProductoID IN (
		pro.ProductoID
		,0
		)

GO
