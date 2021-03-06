USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoCuenta_Actualizar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoCuenta_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[TipoCuenta_Actualizar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
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
CREATE PROCEDURE [dbo].[TipoCuenta_Actualizar]
	@TipoCuentaID INT,
	@Descripcion VARCHAR(50),
	@Activo BIT,
	@FechaModificacion SMALLDATETIME,
	@UsuarioModificacionID INT	
AS
BEGIN
	SET NOCOUNT ON;
		UPDATE TipoCuenta SET 
			Descripcion = @Descripcion,
			Activo = @Activo,
			FechaModificacion = GETDATE(),
			UsuarioModificacionID = @UsuarioModificacionID	
		WHERE TipoCuentaID = @TipoCuentaID
	SET NOCOUNT OFF;
END

GO
