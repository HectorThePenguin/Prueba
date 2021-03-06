USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Cuenta_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Cuenta_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[Cuenta_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
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
CREATE PROCEDURE [dbo].[Cuenta_Actualizar]
	@CuentaID INT,
	@Descripcion VARCHAR(50),
	@TipoCuentaID INT,
	@ClaveCuenta VARCHAR(50),
	@Activo BIT,
	@UsuarioModificacionID INT	
AS
BEGIN
	SET NOCOUNT ON;
		UPDATE Cuenta SET 
			Descripcion = @Descripcion,
			TipoCuentaID = @TipoCuentaID,
			ClaveCuenta = @ClaveCuenta,
			Activo = @Activo,
			FechaModificacion = GETDATE(),
			UsuarioModificacionID = @UsuarioModificacionID	
		WHERE CuentaID = @CuentaID
	SET NOCOUNT OFF;
END

GO
