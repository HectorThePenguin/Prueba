USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoProrrateo_ObtenerTodos]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoProrrateo_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[TipoProrrateo_ObtenerTodos]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 2013/11/12
-- Description: Sp para obtener todos los Tipos de Prorrateo
-- TipoProrrateo_ObtenerTodos
--=============================================
CREATE PROCEDURE [dbo].[TipoProrrateo_ObtenerTodos]
@Activo BIT	= NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		TipoProrrateoID,
		Descripcion,
		Activo,
		FechaCreacion,
		UsuarioCreacionID,
		FechaModificacion,
		UsuarioModificacionID	
	FROM TipoProrrateo
	WHERE (Activo = @Activo OR @Activo is null)
	SET NOCOUNT OFF;
END

GO
