USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenMovimiento_ActualizarEstatus]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenMovimiento_ActualizarEstatus]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenMovimiento_ActualizarEstatus]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Jesus Alvarez
-- Create date: 28/06/2014
-- Description: Actualiza el status del movimiento
-- AlmacenMovimiento_ActualizarEstatus 
-- =============================================
CREATE PROCEDURE [dbo].[AlmacenMovimiento_ActualizarEstatus]		
@AlmacenMovimientoID INT,
@Status INT,
@Observaciones VARCHAR(255),
@UsuarioModificacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE AlmacenMovimiento
			SET Status = @Status,
			Observaciones = @Observaciones,
			FechaModificacion = GETDATE(),
			UsuarioModificacionID = @UsuarioModificacionID
			WHERE AlmacenMovimientoID = @AlmacenMovimientoID
	SET NOCOUNT OFF;
END

GO
