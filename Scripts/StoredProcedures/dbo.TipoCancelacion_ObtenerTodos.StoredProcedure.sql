USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoCancelacion_ObtenerTodos]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoCancelacion_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[TipoCancelacion_ObtenerTodos]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : 
-- Create date: 02/12/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TipoCancelacion_ObtenerTodos
--======================================================
CREATE PROCEDURE [dbo].[TipoCancelacion_ObtenerTodos] @Activo BIT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT TipoCancelacionID
		,Descripcion
		,DiasPermitidos
		,Activo
	FROM TipoCancelacion
	WHERE Activo = @Activo
		OR @Activo IS NULL
	SET NOCOUNT OFF;
END

GO
