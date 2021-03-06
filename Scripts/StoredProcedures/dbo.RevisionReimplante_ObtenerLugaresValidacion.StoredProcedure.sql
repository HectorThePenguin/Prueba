USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[RevisionReimplante_ObtenerLugaresValidacion]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[RevisionReimplante_ObtenerLugaresValidacion]
GO
/****** Object:  StoredProcedure [dbo].[RevisionReimplante_ObtenerLugaresValidacion]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Roque Solis
-- Create date: 2014/02/12
-- Description: Obtiene los lugares de validacion
-- RevisionReimplante_ObtenerLugaresValidacion 1
--=============================================
CREATE PROCEDURE [dbo].[RevisionReimplante_ObtenerLugaresValidacion] 
	@Activo BIT
AS
BEGIN
	SET NOCOUNT ON
		SELECT AreaRevisionID,
			Descripcion,
			Activo,
			FechaCreacion,
			UsuarioCreacionID,
			FechaModificacion,
			UsuarioModificacionID
		FROM AreaRevision
		WHERE Activo = @Activo
	SET NOCOUNT OFF
END

GO
