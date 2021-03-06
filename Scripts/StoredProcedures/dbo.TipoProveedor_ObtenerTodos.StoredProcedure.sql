USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoProveedor_ObtenerTodos]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoProveedor_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[TipoProveedor_ObtenerTodos]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Jos� Gilberto Quintero L�pez
-- Create date: 15/10/2013
-- Description:  Obtener listado de TipoProveedores
-- TipoProveedor_ObtenerTodos
-- =============================================
CREATE PROCEDURE [dbo].[TipoProveedor_ObtenerTodos]
@Activo BIT	= NULL
AS
  BEGIN
      SET NOCOUNT ON;
      SELECT TipoProveedorID,
			Descripcion,
			CodigoGrupoSAP,
			Activo
      FROM TipoProveedor
      WHERE (Activo = @Activo OR @Activo is null)
      SET NOCOUNT OFF;
  END

GO
