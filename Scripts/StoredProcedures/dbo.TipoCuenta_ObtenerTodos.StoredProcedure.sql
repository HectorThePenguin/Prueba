USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoCuenta_ObtenerTodos]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoCuenta_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[TipoCuenta_ObtenerTodos]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jos� Gilberto Quintero L�pez
-- Create date: 2013/11/28
-- Description: 
-- TipoCuenta_ObtenerTodos 0
--=============================================
CREATE PROCEDURE [dbo].[TipoCuenta_ObtenerTodos]
@Activo BIT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		TipoCuentaID,
		Descripcion,
		Activo
	FROM TipoCuenta
	WHERE Activo = @Activo OR @Activo IS NULL
	SET NOCOUNT OFF;
END

GO
