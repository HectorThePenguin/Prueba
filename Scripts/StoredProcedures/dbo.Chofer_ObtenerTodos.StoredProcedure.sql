USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Chofer_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Chofer_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[Chofer_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jos� Gilberto Quintero L�pez
-- Create date: 15/10/2013
-- Description:	Obtener listado de choferes
-- Chofer_ObtenerTodos 1
-- =============================================
CREATE PROCEDURE [dbo].[Chofer_ObtenerTodos]	
@Activo BIT = NULL	
AS
BEGIN
	SET NOCOUNT ON;
	Select ChoferID,
		Nombre,
		ApellidoPaterno,
		ApellidoMaterno,
		Activo
	FROM Chofer
		WHERE (
			Activo = @Activo
			OR @Activo IS NULL
			)
	SET NOCOUNT OFF;
END

GO
