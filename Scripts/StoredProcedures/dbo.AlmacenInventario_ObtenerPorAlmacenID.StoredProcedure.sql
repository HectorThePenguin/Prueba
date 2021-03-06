USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventario_ObtenerPorAlmacenID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenInventario_ObtenerPorAlmacenID]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventario_ObtenerPorAlmacenID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jesus Alvarez
-- Create date: 23/05/2014
-- Description: Obtiene almacen inventario por almacenid
-- SpName     : AlmacenInventario_ObtenerPorAlmacenID 1
--======================================================
CREATE PROCEDURE [dbo].[AlmacenInventario_ObtenerPorAlmacenID]
@AlmacenID INT
AS
BEGIN
	SELECT 
		AlmacenInventarioID,
		AlmacenID,
		ProductoID,
		Minimo,
		Maximo,
		PrecioPromedio,
		Cantidad,
		Importe,
		FechaCreacion,
		UsuarioCreacionID
	FROM AlmacenInventario (NOLOCK) AI
	WHERE AI.AlmacenID = @AlmacenID
END

GO
