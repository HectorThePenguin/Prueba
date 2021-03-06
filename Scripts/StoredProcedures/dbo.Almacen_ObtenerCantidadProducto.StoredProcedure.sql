USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_ObtenerCantidadProducto]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Almacen_ObtenerCantidadProducto]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_ObtenerCantidadProducto]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    C�sar Valdez Figueroa
-- Create date: 18/02/2014
-- Description:  Obtiene la cantidad del producto
-- Almacen_ObtenerCantidadProducto 3,1
-- =============================================
CREATE PROCEDURE [dbo].[Almacen_ObtenerCantidadProducto]
	@AlmacenID INT,
    @ProductoID INT
AS
  BEGIN
      SET NOCOUNT ON;
		SELECT AlmacenInventarioID, 
		       AlmacenID, 
			   ProductoID, 
			   Minimo,
			   Maximo, 
			   PrecioPromedio, 
			   Cantidad, 
			   Importe
		  FROM AlmacenInventario
		 WHERE AlmacenID = @AlmacenID
		   AND ProductoID = @ProductoID
      SET NOCOUNT OFF;
  END

GO
