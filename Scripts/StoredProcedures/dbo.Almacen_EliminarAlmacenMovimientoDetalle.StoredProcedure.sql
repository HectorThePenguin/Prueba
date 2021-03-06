USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_EliminarAlmacenMovimientoDetalle]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Almacen_EliminarAlmacenMovimientoDetalle]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_EliminarAlmacenMovimientoDetalle]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Jesus Alvarez
-- Fecha: 2013-12-19
-- Origen: APInterfaces
-- Descripci�n:	Elimina registros de AlmacenMovimientoDetalle por ID
-- EXEC Almacen_EliminarAlmacenMovimientoDetalle 15,64,6
-- =============================================
CREATE PROCEDURE [dbo].[Almacen_EliminarAlmacenMovimientoDetalle]
@AlmacenMovimientoDetalleID BIGINT
AS
BEGIN
	DELETE FROM AlmacenMovimientoDetalle WHERE AlmacenMovimientoDetalleID = @AlmacenMovimientoDetalleID ;
END

GO
