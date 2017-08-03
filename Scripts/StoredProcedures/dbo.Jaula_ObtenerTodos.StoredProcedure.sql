USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Jaula_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Jaula_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[Jaula_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Jos� Gilberto Quintero L�pez
-- Create date: 15/10/2013
-- Description:  Obtener listado de Jaulas
-- Jaula_ObtenerTodos 1
-- =============================================
CREATE PROCEDURE [dbo].[Jaula_ObtenerTodos]
@Activo BIT = null
AS
  BEGIN
    SET NOCOUNT ON;
	SELECT JaulaID,
	 Placajaula,
	 ProveedorID,
	 Capacidad,
	 Secciones,
	 Activo
	FROM Jaula
	WHERE (Activo = @Activo OR @Activo is null)
	  ORDER BY Placajaula
      SET NOCOUNT OFF;
END

GO
