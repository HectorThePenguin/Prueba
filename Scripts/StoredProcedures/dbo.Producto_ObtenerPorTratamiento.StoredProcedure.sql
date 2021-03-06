USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerPorTratamiento]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Producto_ObtenerPorTratamiento]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerPorTratamiento]    Script Date: 15/10/2015 09:31:45 a.m. ******/
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
CREATE PROCEDURE [dbo].[Producto_ObtenerPorTratamiento]
@OrganizacionId INT,
@CodigoTratamiento INT,
@Sexo Char,
@TipoTratamiento INT
AS
  BEGIN
      SET NOCOUNT ON;
		SELECT P.ProductoID, P.Descripcion, P.SubFamiliaID, P.UnidadID, P.Activo, TP.Dosis , TP.TratamientoID
		FROM Tratamiento T
		INNER JOIN TratamientoProducto TP on T.TratamientoID = TP.TratamientoID
		INNER JOIN Producto P ON TP.ProductoID = P.ProductoID
		WHERE T.CodigoTratamiento = @CodigoTratamiento 
		AND T.TipoTratamientoID = @TipoTratamiento
		AND T.OrganizacionID = @OrganizacionId AND P.Activo = 1
		AND T.Sexo = @Sexo
		AND P.Activo = 1
		AND T.Activo = 1
		AND TP.Activo = 1
      SET NOCOUNT OFF;
  END

GO
