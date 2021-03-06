USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ContratoParcial_ActualizarEstado]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ContratoParcial_ActualizarEstado]
GO
/****** Object:  StoredProcedure [dbo].[ContratoParcial_ActualizarEstado]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Jesus Alvarez
-- Create date: 22/08/2014
-- Description: Actualiza el estatus del contrato parcial
-- ContratoParcial_ActualizarEstado
-- =============================================
CREATE PROCEDURE [dbo].[ContratoParcial_ActualizarEstado]		
	@ContratoParcialID INT,
	@Activo INT,
	@UsuarioModificacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE ContratoParcial
			SET Activo = @Activo,
			FechaModificacion = GETDATE(),
			UsuarioModificacionID = @UsuarioModificacionID
			WHERE ContratoParcialID = @ContratoParcialID
	SET NOCOUNT OFF;
END

GO
