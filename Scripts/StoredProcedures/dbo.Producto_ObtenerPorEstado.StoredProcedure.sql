USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerPorEstado]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Producto_ObtenerPorEstado]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerPorEstado]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Leonel Ayala
-- Create date: 17/12/2013
-- Description:  Obtener listado de productos activos
-- Producto_ObtenerPorEstado 1 
-- =============================================
CREATE PROCEDURE [dbo].[Producto_ObtenerPorEstado]
@Activo INT
AS
  BEGIN
      SET NOCOUNT ON;
		SELECT 
			P.ProductoID, 
			P.Descripcion AS ProductoDescripcion,
			P.SubFamiliaID,
			Fa.FamiliaID,
			Fa.Descripcion AS FamiliaDescripcion,
			P.UnidadID,
   			Um.Descripcion AS UnidadDescripcion,
			P.Activo,
			Sub.Descripcion AS SubFamilia,
			P.ManejaLote
		FROM Producto P
		INNER JOIN SubFamilia Sub ON P.SubFamiliaID = Sub.SubFamiliaID
		INNER JOIN Familia Fa ON Fa.FamiliaID = Sub.FamiliaID
  		INNER JOIN UnidadMedicion Um ON Um.UnidadID = P.UnidadID  
		WHERE P.Activo = @Activo
      SET NOCOUNT OFF;
  END

GO
