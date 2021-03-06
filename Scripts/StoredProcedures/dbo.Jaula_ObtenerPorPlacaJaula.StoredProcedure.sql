USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Jaula_ObtenerPorPlacaJaula]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Jaula_ObtenerPorPlacaJaula]
GO
/****** Object:  StoredProcedure [dbo].[Jaula_ObtenerPorPlacaJaula]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Jos� Gilberto Quintero L�pez
-- Create date: 15/10/2013
-- Description:  Obtiene un registro de jaula por su Id
-- Jaula_ObtenerPorPlacaJaula
-- =============================================
CREATE PROCEDURE [dbo].[Jaula_ObtenerPorPlacaJaula]
(
	@PlacaJaula VARCHAR(10)
	, @ProveedorID INT
)
AS
  BEGIN
      SET NOCOUNT ON;
      SELECT JaulaID,
             PlacaJaula,
             ProveedorID,
             Capacidad,
             Secciones,
             Activo
      FROM Jaula
      Where PlacaJaula = @PlacaJaula
		AND @ProveedorID IN (ProveedorID, 0)
      SET NOCOUNT OFF;
  END

GO
