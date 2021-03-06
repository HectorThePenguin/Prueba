USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoCuenta_Crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoCuenta_Crear]
GO
/****** Object:  StoredProcedure [dbo].[TipoCuenta_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
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
CREATE PROCEDURE [dbo].[TipoCuenta_Crear]
	@Descripcion VARCHAR(50),
	@Activo BIT,
	@UsuarioCreacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	INSERT TipoCuenta(
		Descripcion,
		Activo,
		FechaCreacion,
		UsuarioCreacionID
	)
	VALUES(
		@Descripcion,
		@Activo,
		GETDATE(),
		@UsuarioCreacionID
	)
	SET NOCOUNT OFF;
END

GO
