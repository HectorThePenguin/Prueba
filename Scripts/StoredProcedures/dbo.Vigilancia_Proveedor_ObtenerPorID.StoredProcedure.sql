USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Vigilancia_Proveedor_ObtenerPorID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Vigilancia_Proveedor_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[Vigilancia_Proveedor_ObtenerPorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Eduardo Cota
-- Create date: 18/Mayo/14
-- Description:  Obtener listado de Provvedores de materia prima
-- Vigilancia_Proveedor_ObtenerPorID 4842
-- =============================================
CREATE PROCEDURE [dbo].[Vigilancia_Proveedor_ObtenerPorID]
@ID int,
 @TipoProveedorID INT
AS
  BEGIN
      SET NOCOUNT ON;
      SELECT 
			O.ProveedorID,
 			O.Descripcion,
			O.Activo
      FROM Proveedor O
      WHERE (O.ProveedorID = @ID) AND (O.TipoProveedorID = @TipoProveedorID)
      SET NOCOUNT OFF;
  END

GO
