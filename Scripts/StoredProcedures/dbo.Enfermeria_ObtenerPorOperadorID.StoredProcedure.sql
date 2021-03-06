USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Enfermeria_ObtenerPorOperadorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Enfermeria_ObtenerPorOperadorID]
GO
/****** Object:  StoredProcedure [dbo].[Enfermeria_ObtenerPorOperadorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Enfermeria_ObtenerPorOperadorID]
	@OperadorID INT
AS
BEGIN	
	SET NOCOUNT ON;	
		SELECT E.EnfermeriaID, ISNULL(E.Descripcion, '') AS Descripcion FROM SupervisorEnfermeria (NOLOCK) AS SE
		INNER JOIN Enfermeria(NOLOCK) AS E ON (E.EnfermeriaID = SE.EnfermeriaID)
		WHERE SE.OperadorID = @OperadorID AND SE.Activo = 1 AND E.Activo = 1
	SET NOCOUNT OFF;
END

GO
