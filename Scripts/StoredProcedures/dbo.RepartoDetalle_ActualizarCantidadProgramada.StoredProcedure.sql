USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[RepartoDetalle_ActualizarCantidadProgramada]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[RepartoDetalle_ActualizarCantidadProgramada]
GO
/****** Object:  StoredProcedure [dbo].[RepartoDetalle_ActualizarCantidadProgramada]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Jorge Luis Velazquez Araujo
-- Create date: 2014/10/29
-- Description: Procedimiento para actualizar la cantidad programada del Reparto Detalle
-- EXEC [dbo].[RepartoDetalle_ActualizarAlmacenMovimiento] 1,16,123,200.0,1
-- =============================================
CREATE PROCEDURE [dbo].[RepartoDetalle_ActualizarCantidadProgramada]
	@RepartoDetalleXML XML
AS
BEGIN
	CREATE table #RepartoDetalle (
		RepartoDetalleID BIGINT,
		CantidadProgramada INT,		
		UsuarioCreacionID INT				
	)
	INSERT #RepartoDetalle (
		RepartoDetalleID,
		CantidadProgramada,
		UsuarioCreacionID)
	SELECT RepartoDetalleID = t.item.value('./RepartoDetalleID[1]', 'BIGINT'),
		AlmacenMovimientoID = t.item.value('./CantidadProgramada[1]', 'INT'),		
		UsuarioCreacionID = t.item.value('./UsuarioCreacionID[1]', 'INT')
	FROM @RepartoDetalleXML.nodes('ROOT/RepartoDetalle') AS T(item)
	update rd
	set rd.CantidadProgramada = rdtemp.CantidadProgramada, rd.FechaModificacion = GETDATE(), rd.UsuarioModificacionID = rdtemp.UsuarioCreacionID
	from RepartoDetalle rd
	inner join #RepartoDetalle rdtemp on rd.RepartoDetalleID = rdtemp.RepartoDetalleID
	where rdtemp.RepartoDetalleID > 0
END

GO
