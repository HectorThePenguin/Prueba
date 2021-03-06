USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Operador_ObtenerTodos]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Operador_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[Operador_ObtenerTodos]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 06/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Operador_ObtenerTodos
--======================================================
CREATE PROCEDURE [dbo].[Operador_ObtenerTodos]
@Activo BIT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		OperadorID
		,OrganizacionID
		,Nombre
		,ApellidoPaterno
		,ApellidoMaterno
		,CodigoSAP
		,RolID
		,UsuarioID
		,Activo
	FROM Operador
	WHERE Activo = @Activo OR @Activo IS NULL
	SET NOCOUNT OFF;
END

GO
