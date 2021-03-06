USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Jaula_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Jaula_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[Jaula_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Jos� Gilberto Quintero L�pez
-- Create date: 15/10/2013
-- Description:  Obtiene un registro de jaula por su Id
-- Jaula_ObtenerPorID 1
-- =============================================
CREATE PROCEDURE [dbo].[Jaula_ObtenerPorID]
(
	@JaulaID int
)
AS
  BEGIN
      SET NOCOUNT ON;
      SELECT j.JaulaID,
             j.PlacaJaula,
             j.ProveedorID,
             p.Descripcion as [Proveedor],
             p.CodigoSAP,
             j.Capacidad,
             j.Secciones,
             j.Activo
      FROM Jaula j
      inner join Proveedor p on p.ProveedorID = j.ProveedorID
      Where JaulaID = @JaulaID
      SET NOCOUNT OFF;
  END

GO
