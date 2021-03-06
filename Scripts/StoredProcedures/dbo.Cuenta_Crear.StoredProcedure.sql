USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Cuenta_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Cuenta_Crear]
GO
/****** Object:  StoredProcedure [dbo].[Cuenta_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
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
CREATE PROCEDURE [dbo].[Cuenta_Crear]
@Descripcion VARCHAR(50),
	@TipoCuentaID INT,
	@ClaveCuenta VARCHAR(50),
	@Activo BIT,	
	@UsuarioCreacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	INSERT Cuenta(
		Descripcion,
		TipoCuentaID,
		ClaveCuenta,
		Activo,
		FechaCreacion,
		UsuarioCreacionID		
	)
	VALUES(
		@Descripcion,
		@TipoCuentaID,
		@ClaveCuenta,
		@Activo,
		GETDATE(),
		@UsuarioCreacionID
	)
	SET NOCOUNT OFF;
END

GO
