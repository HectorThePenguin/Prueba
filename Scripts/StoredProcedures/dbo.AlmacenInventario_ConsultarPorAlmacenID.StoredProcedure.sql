USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventario_ConsultarPorAlmacenID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenInventario_ConsultarPorAlmacenID]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventario_ConsultarPorAlmacenID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Pedro Delgado
-- Create date: 05/04/2014
-- Description: Obtiene todos los productos asignado almacenID
-- SpName     : EXEC AlmacenInventario_ConsultarPorAlmacenID 1,4
--======================================================
CREATE PROCEDURE [dbo].[AlmacenInventario_ConsultarPorAlmacenID]
@AlmacenID INT,
@OrganizacionID INT
AS
BEGIN
	SELECT 
		AI.AlmacenInventarioID, 
		AI.AlmacenID,
		AI.ProductoID,
		AI.Minimo,
		AI.Maximo,
		AI.PrecioPromedio,
		AI.Cantidad,
		AI.Importe
	FROM AlmacenInventario (NOLOCK) AI
	INNER JOIN Formula (NOLOCK) F ON (AI.ProductoID = F.ProductoID)
	INNER JOIN Almacen (NOLOCK) A ON (AI.AlmacenID = A.AlmacenID)
	WHERE AI.AlmacenID = @AlmacenID AND A.OrganizacionID = @OrganizacionID AND A.Activo = 1
END

GO
