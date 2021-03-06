USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[IndicadorProducto_ObtenerPorProductoId]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[IndicadorProducto_ObtenerPorProductoId]
GO
/****** Object:  StoredProcedure [dbo].[IndicadorProducto_ObtenerPorProductoId]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author:		Jesus Alvarez
-- Create date: 17/05/2014
-- Description:	Obtiene un indicador producto por producto id
-- IndicadorProducto_ObtenerPorProductoId 82, 1
--======================================================
CREATE PROCEDURE [dbo].[IndicadorProducto_ObtenerPorProductoId]
@ProductoID INT,
@Activo BIT = null 
AS
BEGIN

	SELECT  
		IP.IndicadorProductoID,
		IP.IndicadorID,
		IP.ProductoID,
		I.Descripcion,
		IP.Activo,
		IP.Minimo,
        IP.Maximo
	FROM IndicadorProducto (NOLOCK) IP 
	INNER JOIN Indicador (NOLOCK) I ON IP.IndicadorID = I.IndicadorID 
	WHERE IP.ProductoID = @ProductoID
	AND (IP.Activo = @Activo OR @Activo is null)
	AND (I.Activo = @Activo OR @Activo is null)
END

GO
