USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenMovimiento_ObtenerPorId]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenMovimiento_ObtenerPorId]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenMovimiento_ObtenerPorId]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jesus Alvarez
-- Create date: 24/05/2014
-- Description: Obtiene un almacen movimiento
-- SpName     : AlmacenMovimiento_ObtenerPorId 1
--======================================================
CREATE PROCEDURE [dbo].[AlmacenMovimiento_ObtenerPorId]
@AlmacenMovimientoID BIGINT
AS
BEGIN
	SELECT 
		AlmacenMovimientoID,
		AlmacenID,
		TipoMovimientoID,
		ProveedorID,
		FolioMovimiento,
		Observaciones,
		FechaMovimiento,
		Status,
		AnimalMovimientoID,
		FechaCreacion,
		UsuarioCreacionID
	FROM AlmacenMovimiento (NOLOCK) AM
	WHERE AM.AlmacenMovimientoID = @AlmacenMovimientoID
END

GO
