USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_ActualizarAlmacenMovimiento]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Almacen_ActualizarAlmacenMovimiento]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_ActualizarAlmacenMovimiento]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jesus Alvarez
-- Create date: 28/03/2014
-- Description: Actualizar un registro en AlmacenMovimiento por ID
-- SpName     : Almacen_ActualizarAlmacenMovimiento
--======================================================
CREATE PROCEDURE [dbo].[Almacen_ActualizarAlmacenMovimiento]
@AlmacenMovimientoID BIGINT,
@Observaciones VARCHAR(255),
@Estatus INT,
@UsuarioModificacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE AlmacenMovimiento SET
		Observaciones = @Observaciones,
		Status = @Estatus,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE AlmacenMovimientoID = @AlmacenMovimientoID
	SET NOCOUNT OFF;
END

GO
