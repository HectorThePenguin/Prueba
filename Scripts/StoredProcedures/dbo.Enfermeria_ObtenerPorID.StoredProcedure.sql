USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Enfermeria_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Enfermeria_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[Enfermeria_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Jos� Gilberto Quintero L�pez
-- Create date: 15/10/2013
-- Description:  Obtener listado de Enfermeriaes 
-- Enfermeria_ObtenerPorID 1, 4
-- =============================================
CREATE PROCEDURE [dbo].[Enfermeria_ObtenerPorID]
(
	@EnfermeriaID int
	,@OrganizacionID int
)
AS
  BEGIN
      SET NOCOUNT ON;
      SELECT EnfermeriaID
		,OrganizacionID
		,Descripcion
		,Activo
      FROM Enfermeria
      WHERE EnfermeriaID = @EnfermeriaID
      And @OrganizacionID in (OrganizacionID, 0)
      SET NOCOUNT OFF;
  END

GO
