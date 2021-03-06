USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Proveedor_ObtenerTodos]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Proveedor_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[Proveedor_ObtenerTodos]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Jos� Gilberto Quintero L�pez
-- Create date: 15/10/2013
-- Description:  Obtener listado de Proveedores
-- Proveedor_ObtenerTodos 1
-- =============================================
CREATE PROCEDURE [dbo].[Proveedor_ObtenerTodos]
@Activo BIT = NULL
AS
  BEGIN
      SET NOCOUNT ON;
      SELECT ProveedorID,
			CodigoSAP,
			Descripcion,
			TipoProveedorID,
			Activo
      FROM Proveedor
       WHERE Activo = @Activo OR @Activo IS NULL
      SET NOCOUNT OFF;
  END

GO
