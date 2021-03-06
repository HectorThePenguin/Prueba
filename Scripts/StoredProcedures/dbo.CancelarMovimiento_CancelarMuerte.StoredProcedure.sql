USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CancelarMovimiento_CancelarMuerte]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CancelarMovimiento_CancelarMuerte]
GO
/****** Object:  StoredProcedure [dbo].[CancelarMovimiento_CancelarMuerte]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: C�sar Valdez
-- Create date: 14/02/2013
-- Description: Cancela el movimiento muerte
-- Empresa: Apinterfaces
-- EXEC CancelarMovimiento_CancelarMuerte 1,'Motivo',1,1,7
-- =============================================
CREATE PROCEDURE [dbo].[CancelarMovimiento_CancelarMuerte]
	@MuerteID INT,
	@MotivoCancelacion VARCHAR(255),
	@OperadorCancelacion INT,
	@UsuarioModificacionID INT,
	@EstatusID INT
AS
BEGIN
	UPDATE Muertes 	
	SET OperadorCancelacion = @OperadorCancelacion,
		FechaCancelacion = GETDATE(),
		MotivoCancelacion = @MotivoCancelacion,
		EstatusID = @EstatusID,
		Activo = 0, 
		FechaModificacion = GETDATE(),
		UsuarioModificacionID = @UsuarioModificacionID
	WHERE MuerteID = @MuerteID
END

GO
