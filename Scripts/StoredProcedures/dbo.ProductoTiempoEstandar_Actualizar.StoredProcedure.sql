IF EXISTS(SELECT * FROM   sys.objects WHERE  [object_id] = Object_id(N'[dbo].[ProductoTiempoEstandar_Actualizar]'))
BEGIN
 DROP PROCEDURE [dbo].[ProductoTiempoEstandar_Actualizar]
END
GO

--=============================================
-- Author     : Daniel Benitez
-- Create date: 2017/02/21
-- Description: 
-- [ProductoTiempoEstandar_Actualizar] 0, 1
--=============================================
CREATE PROCEDURE [dbo].[ProductoTiempoEstandar_Actualizar]
@ProductoTiempoEstandarID INT,
@ProductoId INT,
@Estatus INT,
@Tiempo VARCHAR(8),
@UsuarioCreacion INT
AS
BEGIN
	SET NOCOUNT ON;
	
	UPDATE ProductoTiempoEstandar SET Tiempo = @Tiempo, UsuarioModificacionID =  @UsuarioCreacion, FechaModificacion = GETDATE(), Activo = @Estatus
	WHERE ProductoTiempoEstandarID = @ProductoTiempoEstandarID;
	
	SELECT @@ROWCOUNT Resultado;
	
	SET NOCOUNT OFF;
END