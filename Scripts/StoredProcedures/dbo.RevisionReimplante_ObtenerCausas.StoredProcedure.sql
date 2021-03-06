USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[RevisionReimplante_ObtenerCausas]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[RevisionReimplante_ObtenerCausas]
GO
/****** Object:  StoredProcedure [dbo].[RevisionReimplante_ObtenerCausas]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Roque Solis
-- Create date: 2014/03/12
-- Description: Obtiene las causas para la revison de implante
-- RevisionReimplante_ObtenerCausas 1
--=============================================
CREATE PROCEDURE [dbo].[RevisionReimplante_ObtenerCausas] 
	@Activo BIT
AS
BEGIN
	SET NOCOUNT ON
		SELECT EstadoImplanteID,
			Descripcion,
			Activo,
			FechaCreacion,
			UsuarioCreacionID,
			FechaModificacion,
			UsuarioModificacionID,
			Correcto
		FROM EstadoImplante
		WHERE Activo = @Activo
	SET NOCOUNT OFF
END

GO
