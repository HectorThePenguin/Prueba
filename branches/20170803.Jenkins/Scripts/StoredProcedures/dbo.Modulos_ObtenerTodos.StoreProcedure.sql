
--=============================================
-- Author     : Franco Jesus Inzunza Martinez
-- Create date: 16/03/2016
-- Description: Obtiene todos los modulos
-- Modulo_ObtenerTodos
--=============================================
CREATE PROCEDURE [dbo].[Modulo_ObtenerTodos] 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		  ModuloID,
			Descripcion,
			Orden,
			Control
		FROM Modulo WHERE Activo=1
	SET NOCOUNT OFF;
END


