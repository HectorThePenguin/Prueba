USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventarioLote_ObtenerPorAlmacenInventarioID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenInventarioLote_ObtenerPorAlmacenInventarioID]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenInventarioLote_ObtenerPorAlmacenInventarioID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Pedro Delgado
-- Create date: 17/06/2014 12:00:00 a.m.
-- Description: 
-- SpName     : AlmacenInventarioLote_ObtenerPorAlmacenInventarioID
--======================================================
CREATE PROCEDURE [dbo].[AlmacenInventarioLote_ObtenerPorAlmacenInventarioID]
@AlmacenInventarioID INT
AS
BEGIN
	SELECT 
		AlmacenInventarioLoteID,
		AlmacenInventarioID,
		Lote,
		Cantidad,
		PrecioPromedio,
		Piezas,
		Importe,
		FechaInicio,
		FechaFin,
		Activo,
		FechaCreacion,
		UsuarioCreacionID,
		FechaModificacion,
		UsuarioModificacionID
	FROM AlmacenInventarioLote (NOLOCK)
	WHERE AlmacenInventarioID = @AlmacenInventarioID AND Activo = 1
	ORDER BY Lote
END

GO
