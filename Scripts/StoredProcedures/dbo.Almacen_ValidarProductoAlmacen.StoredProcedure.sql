USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_ValidarProductoAlmacen]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Almacen_ValidarProductoAlmacen]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_ValidarProductoAlmacen]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 26/07/2014 12:00:00 a.m.
-- Description: Valida si el almacen tiene por lo menos un producto con cantidad
-- SpName     : Almacen_ValidarProductoAlmacen 1
--======================================================
CREATE PROCEDURE [dbo].[Almacen_ValidarProductoAlmacen] @AlmacenID INT
AS
SELECT TOP 1 ai.AlmacenInventarioID
FROM Almacen a
INNER JOIN AlmacenInventario ai ON a.AlmacenID = ai.AlmacenID
WHERE a.AlmacenID = @AlmacenID
	AND ai.Cantidad > 0

GO
