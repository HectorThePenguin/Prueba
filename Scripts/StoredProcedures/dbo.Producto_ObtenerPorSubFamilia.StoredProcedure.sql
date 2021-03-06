USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerPorSubFamilia]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Producto_ObtenerPorSubFamilia]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerPorSubFamilia]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Leonel Ayala
-- Create date: 17/12/2013
-- Description:  Obtener listado de Corrales Cerrados para programacion de reimplante de ganado
-- OrdenReimplante_ObtenerCorralesProgramar
-- =============================================
CREATE PROCEDURE [dbo].[Producto_ObtenerPorSubFamilia]
@SubFamiliaId INT
AS
  BEGIN
      SET NOCOUNT ON;
		SELECT P.ProductoID, P.Descripcion, P.SubFamiliaID, P.UnidadID, Um.Descripcion AS UnidadDescripcion, P.Activo 
		FROM Producto P
	    INNER JOIN UnidadMedicion Um ON Um.UnidadID = P.UnidadID  
		WHERE P.SubFamiliaID = @SubFamiliaId
      SET NOCOUNT OFF;
  END

GO
