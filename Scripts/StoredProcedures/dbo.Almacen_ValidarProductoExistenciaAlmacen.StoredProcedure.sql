USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_ValidarProductoExistenciaAlmacen]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Almacen_ValidarProductoExistenciaAlmacen]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_ValidarProductoExistenciaAlmacen]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 26/07/2014 12:00:00 a.m.
-- Description: Valida si el producto esta en algun almacen con existencias
-- SpName     : Almacen_ValidarProductoExistenciaAlmacen 1
--======================================================
CREATE PROCEDURE [dbo].[Almacen_ValidarProductoExistenciaAlmacen] @ProductoID INT
AS
SELECT TOP 1 ai.AlmacenInventarioID
FROM Almacen a
INNER JOIN AlmacenInventario ai ON a.AlmacenID = ai.AlmacenID
WHERE ai.ProductoID = @ProductoID
	AND ai.Cantidad > 0

GO
