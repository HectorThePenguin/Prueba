USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_ObtenerAlmacenMovimiento]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Almacen_ObtenerAlmacenMovimiento]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_ObtenerAlmacenMovimiento]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jesus Alvarez
-- Create date: 24/03/2014
-- Description: 
-- SpName     : Almacen_ObtenerAlmacenMovimiento
--======================================================
CREATE PROCEDURE [dbo].[Almacen_ObtenerAlmacenMovimiento]
@AlmacenID INT,
@AlmacenMovimientoID BIGINT
AS
BEGIN
	SELECT 
		   AM.AlmacenID,
		   AM.AlmacenMovimientoID,
		   AM.TipoMovimientoID,
		   AM.FolioMovimiento,
		   AM.FechaMovimiento,
		   AM.Observaciones,
		   AM.AnimalMovimientoID,
		   AM.Status,
	       AM.FechaCreacion,
		   AM.UsuarioCreacionID,
		   AM.FechaModificacion,
		   AM.UsuarioModificacionID
	FROM AlmacenMovimiento AM
	WHERE (AM.AlmacenID = @AlmacenID OR @AlmacenID = 0)
	AND AM.AlmacenMovimientoID = @AlmacenMovimientoID
END

GO
