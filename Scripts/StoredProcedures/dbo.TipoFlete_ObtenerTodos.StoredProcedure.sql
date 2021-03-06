USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoFlete_ObtenerTodos]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoFlete_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[TipoFlete_ObtenerTodos]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Jesus Alvarez
-- Create date: 14/05/2014
-- Description: Obtiene el total de tipos de flete
-- TipoFlete_ObtenerTodos
-- =============================================
CREATE PROCEDURE [dbo].[TipoFlete_ObtenerTodos]
@Activo BIT = null
AS
  BEGIN
      SET NOCOUNT ON;
      SELECT TipoFleteID,
			 Descripcion,
			 Activo,
			 FechaCreacion,
			 UsuarioCreacionID,
			 FechaModificacion,
			 UsuarioModificacionID
      FROM TipoFlete
      WHERE (Activo = @Activo OR @Activo is null)
	  ORDER BY TipoFleteID
      SET NOCOUNT OFF;
  END

GO
