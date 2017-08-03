USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoCuenta_ObtenerPorID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoCuenta_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[TipoCuenta_ObtenerPorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jos� Gilberto Quintero L�pez
-- Create date: 2013/11/28
-- Description: 
-- 
--=============================================
CREATE PROCEDURE [dbo].[TipoCuenta_ObtenerPorID]
@TipoCuentaID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		TipoCuentaID,
		Descripcion,
		Activo
	FROM TipoCuenta
	WHERE TipoCuentaID = @TipoCuentaID
	SET NOCOUNT OFF;
END

GO
