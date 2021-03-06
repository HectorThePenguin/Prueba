USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ContratoDetalle_ActualizarEstado]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ContratoDetalle_ActualizarEstado]
GO
/****** Object:  StoredProcedure [dbo].[ContratoDetalle_ActualizarEstado]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Jesus Alvarez
-- Create date: 21/05/2014
-- Description: Actualiza el estatus del contrato detalle
-- ContratoDetalle_ActualizarEstado
-- =============================================
CREATE PROCEDURE [dbo].[ContratoDetalle_ActualizarEstado]		
	@ContratoDetalleID INT,
	@Activo INT,
	@UsuarioModificacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE ContratoDetalle
			SET Activo = @Activo,
			FechaModificacion = GETDATE(),
			UsuarioModificacionID = @UsuarioModificacionID
			WHERE ContratoDetalleID = @ContratoDetalleID
	SET NOCOUNT OFF;
END

GO
