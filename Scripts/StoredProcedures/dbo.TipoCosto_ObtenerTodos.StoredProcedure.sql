USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoCosto_ObtenerTodos]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoCosto_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[TipoCosto_ObtenerTodos]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 2013/11/12
-- Description: Sp para obtener todos los Tipos de Cost
-- TipoCosto_ObtenerTodos 0
--=============================================
CREATE PROCEDURE [dbo].[TipoCosto_ObtenerTodos]
@Activo BIT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		TipoCostoID,
		Descripcion,
		Activo
	FROM TipoCosto
	WHERE Activo = @Activo OR @Activo IS NULL
	SET NOCOUNT OFF;
END

GO
