USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Vigilancia_Transportista_ObtenerPorID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Vigilancia_Transportista_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[Vigilancia_Transportista_ObtenerPorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Eduardo Cota
-- Create date: 19/Mayo/14
-- Description:  Obtener listado de Provvedores de materia prima
-- Vigilancia_Transportista_ObtenerPorID 375
-- =============================================
CREATE PROCEDURE [dbo].[Vigilancia_Transportista_ObtenerPorID]
@ID INT,
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
