USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CalidadGanado_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CalidadGanado_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[CalidadGanado_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jos� Gilberto Quintero L�pez
-- Create date: 2013/11/28
-- Description: 
-- CalidadGanado_ObtenerTodos
--=============================================
CREATE PROCEDURE [dbo].[CalidadGanado_ObtenerTodos] @Activo BIT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT CalidadGanadoID
		,Calidad
		,Descripcion
		,Sexo
		,Activo
	FROM CalidadGanado
	WHERE (
			Activo = @Activo
			OR @Activo IS NULL
			)
	SET NOCOUNT OFF;
END

GO
