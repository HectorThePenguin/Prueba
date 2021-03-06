USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenMovimiento_ActualizarEstatusNuevo]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenMovimiento_ActualizarEstatusNuevo]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenMovimiento_ActualizarEstatusNuevo]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Jorge Luis Vel�zquez Araujo
-- Create date: 07/07/2014
-- Description: Actualiza el status del movimiento
-- AlmacenMovimiento_ActualizarEstatusNuevo 
-- =============================================
CREATE PROCEDURE [dbo].[AlmacenMovimiento_ActualizarEstatusNuevo]		
@AlmacenID INT,
@FolioMovimiento BIGINT,
@EstatusAnterior INT,
@EstatusNuevo INT,
@UsuarioModificacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE AlmacenMovimiento
			SET Status = @EstatusNuevo,			
			FechaModificacion = GETDATE(),
			UsuarioModificacionID = @UsuarioModificacionID
			WHERE 
			AlmacenID = @AlmacenID
			AND FolioMovimiento = @FolioMovimiento
			AND Status = @EstatusAnterior
	SET NOCOUNT OFF;
END

GO
