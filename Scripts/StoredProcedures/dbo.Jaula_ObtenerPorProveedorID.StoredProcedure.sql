USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Jaula_ObtenerPorProveedorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Jaula_ObtenerPorProveedorID]
GO
/****** Object:  StoredProcedure [dbo].[Jaula_ObtenerPorProveedorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Jos� Gilberto Quintero L�pez
-- Create date: 15/10/2013
-- Description:  Obtiene un registro de jaula por su ProveedorID
-- Jaula_ObtenerPorProveedorID 4857
-- =============================================
CREATE PROCEDURE [dbo].[Jaula_ObtenerPorProveedorID]
(
	@ProveedorID INT
)
AS
  BEGIN
      SET NOCOUNT ON;
      SELECT JaulaID,
             PlacaJaula,
             ProveedorID,
             Capacidad,
             Secciones,
			 NumEconomico,
             Activo
      FROM Jaula
		  WHERE ProveedorID = @ProveedorID
		  AND Activo = 1
		  ORDER BY PlacaJaula
      SET NOCOUNT OFF;
  END

GO
