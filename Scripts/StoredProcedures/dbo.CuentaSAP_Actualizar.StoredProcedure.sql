IF EXISTS(SELECT *
      FROM   sys.objects
      WHERE  [object_id] = Object_id(N'[dbo].[CuentaSAP_Actualizar]'))
			DROP PROCEDURE [dbo].[CuentaSAP_Actualizar]
go
--=============================================
-- Author     : José Gilberto Quintero López
-- Create date: 2013/12/10
-- Description: 
-- 
--=============================================
CREATE PROCEDURE [dbo].[CuentaSAP_Actualizar]
	@CuentaSAPID INT,
	@CuentaSAP VARCHAR(10),
	@Descripcion VARCHAR(50),
	@TipoCuentaID INT,
	@Activo BIT,
	@UsuarioModificacionID INT	
AS
BEGIN
	SET NOCOUNT ON;
		UPDATE CuentaSAP SET 
			CuentaSAP = @CuentaSAP,
			Descripcion = @Descripcion,
			TipoCuentaID = @TipoCuentaID,
			Activo = @Activo,
			FechaModificacion = GETDATE(),
			UsuarioModificacionID = @UsuarioModificacionID	
		WHERE CuentaSAPID = @CuentaSAPID
	SET NOCOUNT OFF;
END
go