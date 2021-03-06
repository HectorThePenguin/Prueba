IF EXISTS(SELECT *
      FROM   sys.objects
      WHERE  [object_id] = Object_id(N'[dbo].[CuentaSAP_Crear]'))
			DROP PROCEDURE [dbo].[CuentaSAP_Crear]
GO
--=============================================
-- Author     : José Gilberto Quintero López
-- Create date: 2013/12/10
-- Description: 
-- 
--=============================================
CREATE PROCEDURE [dbo].[CuentaSAP_Crear]
@CuentaSAP VARCHAR(10),
	@Descripcion VARCHAR(50),
	@TipoCuentaID INT,
	@Activo BIT,
	@UsuarioCreacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	INSERT CuentaSAP(
		CuentaSAP,
		Descripcion,
		TipoCuentaID,
		Activo,
		FechaCreacion,
		UsuarioCreacionID		
	)
	VALUES(
		@CuentaSAP,
		@Descripcion,
		@TipoCuentaID,
		@Activo,
		GETDATE(),
		@UsuarioCreacionID		
	)
	SET NOCOUNT OFF;
END
go
