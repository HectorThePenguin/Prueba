USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Contrato_ActualizarEstado]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Contrato_ActualizarEstado]
GO
/****** Object:  StoredProcedure [dbo].[Contrato_ActualizarEstado]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Jesus Alvarez
-- Create date: 21/05/2014
-- Description: Actualiza el estatus del contrato
-- Contrato_ActualizarEstado 
-- =============================================
CREATE PROCEDURE [dbo].[Contrato_ActualizarEstado]		
	@ContratoID INT,
	@EstatusID INT,
	@Activo INT,
	@UsuarioModificacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE Contrato
			SET EstatusID = @EstatusID,
			Activo = @Activo,
			FechaModificacion = GETDATE(),
			UsuarioModificacionID = @UsuarioModificacionID
			WHERE ContratoID = @ContratoID
	SET NOCOUNT OFF;
END

GO
