
GO
/****** Object:  StoredProcedure [dbo].[TipoCostoCentro_ObtenerTodos]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoCostoCentro_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[TipoCostoCentro_ObtenerTodos]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Edgar Villarreal
-- Create date: 2015/10/19
-- Description: Sp para obtener todos los Tipos de Costos de centro
-- EXEC TipoCostoCentro_ObtenerTodos 1
--=============================================
CREATE PROCEDURE [dbo].[TipoCostoCentro_ObtenerTodos]
@Activo BIT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		TipoCostoCentroID,
		Descripcion,
		Activo
	FROM TipoCostoCentro
	WHERE Activo = @Activo OR @Activo IS NULL
	SET NOCOUNT OFF;
END

GO
